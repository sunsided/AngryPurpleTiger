using System.Security.Cryptography;
using System.Text;

namespace AngryPurpleTiger;

/// <summary>
///     An Animal Name instance.
/// </summary>
/// <param name="Adjective">The adjective to use.</param>
/// <param name="Color">The color to use.</param>
/// <param name="Animal">The animal to use.</param>
public sealed record AnimalName(string Adjective, string Color, string Animal) : IEquatable<string>
{
    private AnimalName(ReadOnlySpan<byte> digest)
        : this(Adjectives.Default[digest[0]], Colors.Default[digest[1]], Animals.Default[digest[2]])
    {
    }

    /// <summary>
    ///     Gets the adjective.
    /// </summary>
    public string Adjective { get; } = EnsureValidWord(Adjective);

    /// <summary>
    ///     Gets the color.
    /// </summary>
    public string Color { get; } = EnsureValidWord(Color);

    /// <summary>
    ///     Gets the animal.
    /// </summary>
    public string Animal { get; } = EnsureValidWord(Animal);

    /// <summary>
    ///     Gets whether this name is canonical, i.e. uses only default
    ///     <see cref="Adjectives"/>, <see cref="Colors"/> and <see cref="Animals"/>.
    /// </summary>
    public bool IsCanonical =>
        Adjectives.Default.Contains(Adjective, StringComparer.OrdinalIgnoreCase) &&
        Colors.Default.Contains(Color, StringComparer.OrdinalIgnoreCase) &&
        Animals.Default.Contains(Animal, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    ///     Constructs a new <see cref="AnimalName"/> struct from the specified input string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>The <see cref="AnimalName"/>.</returns>
    public static AnimalName FromString(string input) => new(HexDigest(input));

    /// <summary>
    ///     Constructs a new <see cref="AnimalName"/> struct from the specified input bytes.
    /// </summary>
    /// <param name="input">The bytes to hash.</param>
    /// <returns>The <see cref="AnimalName"/>.</returns>
    public static AnimalName FromBytes(ReadOnlySpan<byte> input) => new(HexDigest(input));

    /// <summary>
    ///     Returns the generated name.
    /// </summary>
    public override string ToString() => ToString(StringStyle.Titlecase, ' ');

    /// <summary>
    ///     Returns the generated name.
    /// </summary>
    /// <param name="style">The style to use.</param>
    /// <param name="separator">The separator character to use.</param>
    public string ToString(StringStyle style, char separator)
    {
        var sb = new StringBuilder(Adjective.Length + Color.Length + Animal.Length + 2);
        switch (style)
        {
            case StringStyle.Titlecase:
                sb.Append(char.ToUpper(Adjective[0]));
                sb.Append(Adjective[1..]);
                sb.Append(separator);
                sb.Append(char.ToUpper(Color[0]));
                sb.Append(Color[1..]);
                sb.Append(separator);
                sb.Append(char.ToUpper(Animal[0]));
                sb.Append(Animal[1..]);
                break;
            case StringStyle.Lowercase:
                sb.Append(Adjective.ToLower());
                sb.Append(separator);
                sb.Append(Color.ToLower());
                sb.Append(separator);
                sb.Append(Animal.ToLower());
                break;
            case StringStyle.Uppercase:
                sb.Append(Adjective.ToUpper());
                sb.Append(separator);
                sb.Append(Color.ToUpper());
                sb.Append(separator);
                sb.Append(Animal.ToUpper());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, "Unexpected style");
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public bool Equals(string? other) => Equals((ReadOnlySpan<char>)other);

    /// <inheritdoc cref="Equals(string)"/>
    public bool Equals(ReadOnlySpan<char> other)
    {
        if (other.Length == 0)
        {
            return string.IsNullOrWhiteSpace(Adjective) && string.IsNullOrWhiteSpace(Color) &&
                   string.IsNullOrWhiteSpace(Animal);
        }

        var expectedLength = Adjective.Length + Color.Length + Animal.Length + 2;
        if (other.Length != expectedLength) return false;

        var adjectiveEnd = Adjective.Length;
        if (0 != other[..adjectiveEnd].CompareTo(Adjective, StringComparison.OrdinalIgnoreCase)) return false;

        var colorStart = adjectiveEnd + 1;
        var colorEnd = colorStart + Color.Length;
        if (0 != other[colorStart..colorEnd].CompareTo(Color, StringComparison.OrdinalIgnoreCase)) return false;

        var animalStart = colorEnd + 1;
        var animalEnd = animalStart + Animal.Length;
        if (0 != other[animalStart..animalEnd].CompareTo(Animal, StringComparison.OrdinalIgnoreCase)) return false;

        return other[adjectiveEnd] == other[colorEnd];
    }

    internal static ReadOnlySpan<byte> HexDigest(ReadOnlySpan<byte> data)
    {
#if NET7_0_OR_GREATER
        var digest = MD5.HashData(data);
#else
        var md5 = MD5.Create();
        var digest = md5.ComputeHash(data.ToArray());
#endif
        var results = new byte[3];
        Compress(digest.Length / 3, digest, results);
        return results;
    }

    private static ReadOnlySpan<byte> HexDigest(string str) =>
        // Cannot be optimized by using ReadOnlySpan<char> since it always converts to a char[] internally.
        HexDigest(Encoding.UTF8.GetBytes(str));

    private static void Compress(int segmentLength, ReadOnlySpan<byte> bytes, Span<byte> dest)
    {
        while (true)
        {
            if (bytes.Length < 2 * segmentLength)
            {
                dest[0] = Reduce(bytes);
                break;
            }

            dest[0] = Reduce(bytes[..segmentLength]);
            bytes = bytes[segmentLength..];
            dest = dest[1..];
        }
    }

    private static byte Reduce(ReadOnlySpan<byte> bytes)
    {
        byte accumulator = 0;
        foreach (var b in bytes)
        {
            accumulator ^= b;
        }

        return accumulator;
    }

#if NET7_0_OR_GREATER
    private static string EnsureValidWord(string input, [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(input))] string inputExpression = "")
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The value of `{inputExpression}` must be non-null and non-whitespace", inputExpression);
        }

        if (input.Length < 2)
        {
            throw new ArgumentOutOfRangeException(inputExpression, input, $"The length of `{inputExpression}` is too short");
        }

        return input;
    }
#else
    private static string EnsureValidWord(string input)
    {
        if (input.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(input), input, $"The length of `{input}` is too short");
        }

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"The value of `{input}` must be non-null and non-whitespace", nameof(input));
        }

        return input;
    }
#endif
}

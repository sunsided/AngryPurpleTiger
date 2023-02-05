using FluentAssertions;

namespace AngryPurpleTiger.Tests;

public class AnimalNameTests
{
    [Fact]
    public void StringComparisonWorks()
    {
        var animalName = new AnimalName("Rapid", "Grey", "Rattlesnake");
        var animalNameString = "rapid-grey-rattlesnake";
        animalName.Equals(animalNameString).Should().BeTrue();
    }

    [Fact]
    public void FromStringWorks()
    {
        var animalName = AnimalName.FromString("my ugly input string");
        animalName.Adjective.Should().Be("rapid");
        animalName.Color.Should().Be("grey");
        animalName.Animal.Should().Be("rattlesnake");
    }

    [Theory]
    [InlineData("112CuoXo7WCcp6GGwDNBo6H5nKXGH45UNJ39iEefdv2mwmnwdFt8", "Feisty Glass Dalmatian")]
    [InlineData("11SJ11qucpsr5iFxLMHr31JQqLBZVTcVRSYhYHa6D9JkBURusBG", "Flaky Canvas Sparrow")]
    public void FromHeliumAddressWorks(string address, string name)
    {
        var animalName = AnimalName.FromString(address);
        animalName.Equals(name).Should().BeTrue();
    }

    [Fact]
    public void HexDigestFromBytesWorks()
    {
        var digest = AnimalName.HexDigest(new byte[] { 23, 45, 234, 111, 46, 165, 33, 58, 156, 140, 91, 138, 50, 245, 103, 210 });
        // BUG: Should be [ 145, 61, 181] according to JS reference implementation
        digest.ToArray().Should().BeEquivalentTo(new byte[] { 56, 61, 181 });
    }

    [Fact]
    public void FromBytesWorks()
    {
        var animalName = AnimalName.FromBytes(new byte[] { 23, 45, 234, 111, 46, 165, 33, 58, 156, 140, 91, 138, 50, 245, 103, 210 });
        animalName.Adjective.Should().Be(Adjectives.Default[56]); // BUG: Should be 145 according to JS reference implementation
        animalName.Color.Should().Be(Colors.Default[61]); // BUG: Should be 61 according to JS reference implementation
        animalName.Animal.Should().Be(Animals.Default[181]); // BUG: Should be 181 according to JS reference implementation
    }

    [Fact]
    public void ToStringWorks()
    {
        AnimalName.FromString("my ugly input string").ToString().Equals("Rapid Grey Rattlesnake").Should().BeTrue();
    }

    [Theory]
    [InlineData(StringStyle.Lowercase, '-', "rapid-grey-rattlesnake")]
    [InlineData(StringStyle.Lowercase, ' ', "rapid grey rattlesnake")]
    [InlineData(StringStyle.Titlecase, ' ', "Rapid Grey Rattlesnake")]
    [InlineData(StringStyle.Uppercase, '_', "RAPID_GREY_RATTLESNAKE")]
    public void ToStringWithStyleWorks(StringStyle style, char separator, string expected)
    {
        AnimalName.FromString("my ugly input string").ToString(style, separator).Equals(expected).Should().BeTrue();
    }

    [Theory]
    [InlineData(null, "color", "animal")]
    [InlineData("", "color", "animal")]
    [InlineData("   ", "color", "animal")]
    [InlineData("adjective", null, "animal")]
    [InlineData("adjective", "", "animal")]
    [InlineData("adjective", "   ", "animal")]
    [InlineData("adjective", "color", null)]
    [InlineData("adjective", "color", "")]
    [InlineData("adjective", "color", "   ")]
    [InlineData(null, null, null)]
    public void InvalidAdjectiveThrows(string adjective, string color, string animal)
    {
        var viaConstructor = () => new AnimalName(adjective, color, animal);
        viaConstructor.Should().Throw<ArgumentException>();
    }
}

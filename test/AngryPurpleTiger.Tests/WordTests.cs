using FluentAssertions;

namespace AngryPurpleTiger.Tests;

public class WordTests
{
    [Fact]
    public void WordListEntriesAreLowercase()
    {
        Adjectives.Default.Concat(Colors.Default).Concat(Animals.Default)
            .All(word => word.All(char.IsLower))
            .Should().BeTrue();
    }

    [Fact]
    public void WordListEntriesHaveAtLeastTwoCharacters()
    {
        Adjectives.Default.Concat(Colors.Default).Concat(Animals.Default)
            .All(word => word.Length > 2)
            .Should().BeTrue();
    }

    [Fact]
    public void DefaultWordListsHave256Entries()
    {
        Adjectives.Default.Count.Should().Be(256);
        Colors.Default.Count.Should().Be(256);
        Animals.Default.Count.Should().Be(256);
    }

    [Fact]
    public void DefaultWordListsEnumeratorYields256Entries()
    {
        Adjectives.Default.Should().HaveCount(256);
        Colors.Default.Should().HaveCount(256);
        Animals.Default.Should().HaveCount(256);
    }

    [Fact]
    public void WordListShouldContainNoDuplicateEntries()
    {
        var wordCount = Adjectives.Default.Concat(Colors.Default).Concat(Animals.Default)
            .Aggregate(new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase), (tally, word) =>
            {
                if (!tally.TryAdd(word, 1))
                {
                    tally[word] += 1;
                }

                return tally;
            });

        // There is an upstream bug in the Helium implementation. It is tracked at https://github.com/helium/angry-purple-tiger/issues/6
        wordCount.Keys.Should().HaveCountLessOrEqualTo(256 * 3, "because in theory the list should contain 3x 256 distinct values");
        wordCount.Keys.Should().HaveCount(256 * 3 - 11, "because the list contains {0} known duplicates", 11);

        // Duplicate attributes.
        wordCount["skinny"].Should().Be(2, "because it is a known duplicate");
        wordCount["long"].Should().Be(2, "because it is a known duplicate");
        wordCount["short"].Should().Be(3, "because it is a known duplicate");
        wordCount["hot"].Should().Be(2, "because it is a known duplicate");
        wordCount["cool"].Should().Be(2, "because it is a known duplicate");
        wordCount["tangy"].Should().Be(2, "because it is a known duplicate");

        // Duplicate animals.
        wordCount["raccoon"].Should().Be(2, "because it is a known duplicate");

        // Animals that are also adjectives.
        wordCount["swift"].Should().Be(2, "because it is used as an animal and adjective");
        wordCount["mammoth"].Should().Be(2, "because it is used as an animal and mammoth");

        // Animals that are also colors.
        wordCount["tiger"].Should().Be(2, "because it is used as an animal and color");

        wordCount.Where(kvp => !IsKnownDuplicate(kvp.Key)).Should().AllSatisfy(kvp => kvp.Value.Should().Be(1, "because the word \"{0}\" should only appear once", kvp.Key));

        static bool IsKnownDuplicate(string word) =>
            word is "skinny" or "long" or "short" or "hot" or "cool" or "tangy" or "swift" or "mammoth" or "tiger" or "raccoon";
    }
}

﻿using System.Collections;

namespace AngryPurpleTiger;

public sealed class Colors : IEnumerable<string>
{
#if NET7_0_OR_GREATER
    private static readonly System.Collections.Immutable.ImmutableList<string> DefaultWords = System.Collections.Immutable.ImmutableList.Create<string>(
#else
    private static readonly List<string> DefaultWords = new ()
    {
#endif
        "white",
        "pearl",
        "alabaster",
        "snowy",
        "ivory",
        "cream",
        "cotton",
        "chiffon",
        "lace",
        "coconut",
        "linen",
        "bone",
        "daisy",
        "powder",
        "frost",
        "porcelain",
        "parchment",
        "velvet",
        "tan",
        "beige",
        "macaroon",
        "hazel",
        "felt",
        "metal",
        "gingham",
        "sand",
        "sepia",
        "latte",
        "vinyl",
        "glass",
        "hazelnut",
        "canvas",
        "wool",
        "yellow",
        "golden",
        "daffodil",
        "flaxen",
        "butter",
        "lemon",
        "mustard",
        "tartan",
        "blue",
        "cloth",
        "fiery",
        "banana",
        "plastic",
        "dijon",
        "honey",
        "blonde",
        "pineapple",
        "orange",
        "tangerine",
        "marigold",
        "cider",
        "rusty",
        "ginger",
        "tiger",
        "bronze",
        "fuzzy",
        "opaque",
        "clay",
        "carrot",
        "corduroy",
        "ceramic",
        "marmalade",
        "amber",
        "sandstone",
        "concrete",
        "red",
        "cherry",
        "hemp",
        "merlot",
        "garnet",
        "crimson",
        "ruby",
        "scarlet",
        "burlap",
        "brick",
        "bamboo",
        "mahogany",
        "blood",
        "sangria",
        "berry",
        "currant",
        "blush",
        "candy",
        "lipstick",
        "pink",
        "rose",
        "fuchsia",
        "punch",
        "watermelon",
        "rouge",
        "coral",
        "peach",
        "strawberry",
        "rosewood",
        "lemonade",
        "taffy",
        "bubblegum",
        "crepe",
        "hotpink",
        "purple",
        "mauve",
        "violet",
        "boysenberry",
        "lavender",
        "plum",
        "magenta",
        "lilac",
        "grape",
        "eggplant",
        "eggshell",
        "iris",
        "heather",
        "amethyst",
        "raisin",
        "orchid",
        "mulberry",
        "carbon",
        "slate",
        "sky",
        "navy",
        "indigo",
        "cobalt",
        "cedar",
        "ocean",
        "azure",
        "cerulean",
        "spruce",
        "stone",
        "aegean",
        "denim",
        "admiral",
        "sapphire",
        "arctic",
        "green",
        "chartreuse",
        "juniper",
        "sage",
        "lime",
        "fern",
        "olive",
        "emerald",
        "pear",
        "mossy",
        "shamrock",
        "seafoam",
        "pine",
        "mint",
        "seaweed",
        "pickle",
        "pistachio",
        "basil",
        "brown",
        "coffee",
        "chrome",
        "peanut",
        "carob",
        "hickory",
        "wooden",
        "pecan",
        "walnut",
        "caramel",
        "gingerbread",
        "syrup",
        "chocolate",
        "tortilla",
        "umber",
        "tawny",
        "brunette",
        "cinnamon",
        "glossy",
        "teal",
        "grey",
        "shadow",
        "graphite",
        "iron",
        "pewter",
        "cloud",
        "silver",
        "smoke",
        "gauze",
        "ash",
        "foggy",
        "flint",
        "charcoal",
        "pebble",
        "lead",
        "tin",
        "fossilized",
        "black",
        "ebony",
        "midnight",
        "inky",
        "oily",
        "satin",
        "onyx",
        "nylon",
        "fleece",
        "sable",
        "jetblack",
        "coal",
        "mocha",
        "obsidian",
        "jade",
        "cyan",
        "leather",
        "maroon",
        "carmine",
        "aqua",
        "chambray",
        "holographic",
        "laurel",
        "licorice",
        "khaki",
        "goldenrod",
        "malachite",
        "mandarin",
        "mango",
        "taupe",
        "aquamarine",
        "turquoise",
        "vermilion",
        "saffron",
        "cinnabar",
        "myrtle",
        "neon",
        "burgundy",
        "tangelo",
        "topaz",
        "wintergreen",
        "viridian",
        "vanilla",
        "paisley",
        "raspberry",
        "tweed",
        "pastel",
        "opal",
        "menthol",
        "champagne",
        "gunmetal",
        "infrared",
        "ultraviolet",
        "rainbow",
        "mercurial",
        "clear",
        "misty",
        "steel",
        "zinc",
        "citron",
        "cornflower",
        "lava",
        "quartz",
        "honeysuckle",
        "chili"
#if NET7_0_OR_GREATER
    );
#else
    };
#endif

    /// <summary>
    ///     Gets the default colors.
    /// </summary>
    public static Colors Default { get; } = new ();

    /// <summary>
    ///     Gets the number of words in this set.
    /// </summary>
    public int Count => DefaultWords.Count;

    /// <summary>
    ///     Gets the word at the specified index.
    /// </summary>
    /// <param name="index">The word index.</param>
    public string this[byte index] => DefaultWords[index];

    /// <inheritdoc />
    public IEnumerator<string> GetEnumerator() => DefaultWords.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

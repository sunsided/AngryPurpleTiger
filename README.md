# Angry Purple Tiger

animal-based hash digests for humans ... in C# — a port of [helium/angry-purple-tiger-rs](https://github.com/helium/angry-purple-tiger-rs).

## Overview

Angry Purple Tiger generates animal-based hash digests meant to be memorable
and human-readable. Angry Purple Tiger is apt for anthropomorphizing project
names, crypto addresses, UUIDs, or any complex string of characters that
needs to be displayed in a user interface.

```shell
dotnet add package AngryPurpleTiger --version 0.1.0
```

## Example

```csharp
using AngryPurpleTiger;

var digest = AnimalName.FromString("my ugly input string");
Console.WriteLine(digest);
//=> Rapid Grey Rattlesnake
```

## Notes

The algorithm is known to produce more `Short Raccoons` than other animals.
The word lists provided by the upstream implementation(s) contain duplicates
of the adjectives `skinny`, `long`, `short`, `hot`, `cool` and `tangy`, 
as well as the animal `raccoon`.  In addition, the words `swift` and `mammoth`
act as both animals and adjectives, while `tiger` serves as both animal and color.

This should not affect the usability of the library but care needs to be taken
when assuming the number of possible combinations.

## License

Apache 2.0 © 2023 Markus Mayer; © 2018 Helium Systems, Inc.

This library is a port of [helium/angry-purple-tiger](https://github.com/helium/angry-purple-tiger) and 
[helium/angry-purple-tiger-rs](https://github.com/helium/angry-purple-tiger-rs), both of
which are released under the Apache-2.0 license. See the [LICENSE](LICENSE)
file for more information.

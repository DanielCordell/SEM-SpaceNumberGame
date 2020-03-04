Style Guide
===========================

- [Asset Naming](#asset-naming)
    - [Folders](#folders)
    - [Source code](#source-code)
    - [Non-code assets](#non-code-assets)
- [Directory/File structure](#directory-file-structure)
    - [Assets](#assets)
    - [Scripts](#scripts)
    - [Models](#models)
- [Code](#code)
    - [Separation](#separation)
    - [Naming](#naming)
    - [Commenting](#commenting)
    - [Spacing](#spacing)
    - [General](#general)

# Asset Naming

First of all, no `spaces` in file or directory names.

## Folders

`PascalCase`

Prefer a deep folder structure over having long asset names.

Directory names should be as concise as possible, prefer one or two words. If a directory name is too long, it probably makes sense to split it into sub directories.

Try to have only one file type per folder. Use `Textures/Trees`, `Models/Trees` and not `Trees/Textures`, `Trees/Models`. That way its easy to set up root directories for the different software involved, for example, Substance Painter would always be set to save to the Textures directory.

If your project contains multiple environments or art sets, use the asset type for the parent directory: `Trees/Jungle`, `Trees/City` not `Jungle/Trees`, `City/Trees`. Since it makes it easier to compare similar assets from different art sets to ensure continuity across art sets.

## Source Code

Use the naming convention of the programming language. For C# and shader files use `PascalCase`, as per C# convention.

## Non-Code Assets

Use `tree_small` not `small_tree`. While the latter sound better in English, it is much more effective to group all tree objects together instead of all small objects.

`camelCase` where necessary. Use `weapon_miniGun` instead of `weapon_gun_mini`. Avoid this if possible, for example, `vehicles_fighterJet` should be `vehicles_jet_fighter` if you plan to have multiple types of jets.

Prefer using descriptive suffixes instead of iterative: `vehicle_truck_damaged` not `vehicle_truck_01`. If using numbers as a suffix, always use 2 digits. And **do not** use it as a versioning system! Use `git` or something similar.

### Persistent/Important GameObjects

`_snake_case`

Use a leading underscore to make object instances that are not specific to the current scene stand out.

# Directory/File Structure
## Assets
e.g.
```
Assets
+---Fonts
+---Plugins
+---Resources
|   +---Art
|       +---Animations
|       +---Mateirals
|       +---Textures
|   +---Audio
|       +---Music
|       +---Sounds
|   +---Level
|       +---Prefabs
|       +---Scenes
|   +---Scripts
|       +---Level
|       +---Shield
|       +---Spaceship
|       +---Timer
|       +---UI
|       +---Visual
+---Tests
|   +---EditMode
|   +---PlayMode
```
## Scripts

Use namespaces that match your directory structure.

e.g.

```
Scripts
+---Level
+---Shield
+---Spaceship
+---Timer
+---UI
+---Visuals
```

# Code
## Separation
Don't try to shove all behaviour into one script! Logical separation of behaviour, _single responsibility_.

## Naming
- Public Variables - `PascalCase`
- Other Variables - `camelCase`
- Classes / Data Types - `PascalCase`
    - File Names should match Class Names.
- Functions - `PasclCase`
- Globals/Constants - `CAPS_UNDERSCORE_CASE`
- If in doubt - `PascalCase`
## Bracketing
When creating a block with `{}`, put newline before opening bracket.
```C#
void test(int i)
{
    PerformAction();
}
```
When in a situation where `{}` are not necessary, leave them out unless it breaks the code flow or makes the code more confusing to understand (this is up to your interpretation).

Also keep your else statements on separate lines from the brackets:
```C#
// Good
if (myBoolean)
    DoThis();
else
    DoThat();

// Bad
if (myBoolean) 
    DoThis();
else 
{
    DoThat();
    AlsoDoThis();
}

// Better
if (myBoolean) 
{
    DoThis();
}
else 
{
    DoThat();
    AlsoDoThis();
}
```

## Commenting

Put spaces after `//` in comments:
```c#
//This is bad
// This is good
```

At the top of the document, comment if you worked on the file. For attribution later on:

```c#
// Worked on by X, Y, Z
```

Don't comment _everything_. Anything that's trivial or easy to understand, don't comment. You can break long sections up with comments if you think it helps with readability.

- Comment anything that isn't clear just by looking at it what it does, or anything obscure.

- You can comment on a specific _process_ you're undertaking if explaining _why_ this process is being undertaken is important to the understanding of the code.

If you've made any choices about doing something one way or another, leave a comment saying:

```c#
// I'm doing it this way not that way because ...
```
Above all, use common sense!

## Spacing
Binary operators should be surrounded by a space:
```c#
x+y=12 // bad
x + y = 12 // good
```

Try to indent to maintain code flow:
```c#
x          = a + b + c
myVariable = x * a + b
myOtherVar = x + myVariable     
```

## General
- https://en.wikipedia.org/wiki/SOLID use this!
Don't be afraid to use brackets `()` to make things clearer.
```c#
myBool = firstBool || secondBool && thirdBool || fourthBool // bad
myBool = firstBool || (secondBool && thirdBool) || fourthBool // good
```

Similarly, don't be afraid to move long statements to new lines, but make sure they're indented properly:

```c#
if (x >= 20 || 
    y <= 50 || 
    (Z > 20 && X < 30))
```

Credits
=============
Written by Daniel & Yao
## Sources:

https://github.com/stillwwater/UnityStyleGuide
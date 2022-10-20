# Chalkos' Subnautica Mods

Subnautica & Subnautica Below Zero
- [Habitat Shrinker](#habitat-shrinker)
- [Cyclops Drift Fix](#cyclops-drift-fix)

---------------------------------------------------

## Instructions

For how to mod, check https://snm.crd.co or the [Subnautica Modding Discord](https://discord.com/invite/UpWuWwq)

Otherwise just download the latest zip for each plugin.

---------------------------------------------------

## Cyclops Drift Fix

Fixes the bug that causes you to be pushed out of the cyclops.
Also fixes seatruck and/or seatruck modules starting to wobble or drift.

[Download for Subnautica](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/CyclopsDriftFix_SN1_v1.0.1.zip)
|
[Download for Below Zero](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/CyclopsDriftFix_BZ_v1.0.1.zip)

### Usage

Install and forget the fear of being glitched out of the Cyclops.

### Known issues

- if you don't have plants in your cyclops/seatruck, this mod is unlikely to fix the bug for you
  - if you want to try to help fix the bug you are experiencing, drop by the [Subnautica Modding Discord](https://discord.com/invite/UpWuWwq) and mention `@Chalkos` with more info about how the bug is happening for you

### Changelog

- v1.0.1
  - more likely to actually work
- v1.0.0
  - initial

## Habitat Shrinker

Modify the size of modules when building them with your Habitat Builder. You'll be able to change each size component (height/width/depth) individually, making them as thin/short/wide/tall as you want. Also supports module enlargement. And the size of plants you grow in resized growbeds or plant pots will match the growbed/pot size.

[Download for Subnautica](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/HabitatShrinker_SN1_v1.0.1.zip)
|
[Download for Below Zero](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/HabitatShrinker_BZ_v1.0.1.zip)

### Usage

Check ingame mod configs, then build something.
To resize, activate resizing (default key `I`) before you place the module.

Ingame mod config works to resize modules from 0.05x up to 2.5x their original size. You can use bigger/smaller values by editing the `config.json`.

The safety override allows you to resize **UNSUPPORTED** modules, like the external base modules. Enable it if you want to break the game.

### Compatibility
- BuildingTweaks (SN1, BZ): yes
- BuilderModule (SN1, BZ): should be ok, needs testing
- BuildInSeatruck (BZ): yes

### Known issues

- **[wontfix, for now]** you'll only be able to place a resized module where you would normally be able to place the non-resized version (collision is not resized)
  - this is because the boundaries for the resized module are not resized. The game does not take scale into consideration at all when calculating those
  - fix: use BuildingTweaks' full override feature and you'll be able to place things wherever you want
  - collision after building the object works fine
- **[to fix]** plants have a maxHeight, so scaling them bigger than that will result in them getting shrunk to the maxHeight when they mature

### Changelog

- v1.0.1
  - bugfix: toggling off resizing will now restore the original (prefab) scale instead of scaling it to 100% 
- v1.0.0
  - initial

---------------------------------------------------

## Building from source

To build you'll need
* this https://github.com/MrPurple6411/AssemblyPublicizerdrag and drag your dll in Subnautica_Data\Managed or SubnauticaZero_Data\Managed to the exe
* adjust the paths in `Common.targets`


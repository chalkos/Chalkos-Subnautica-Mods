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

Fixes the bug that causes you to be pushed out of the cyclops (and sea truck).

Note: growing plants' interaction/collision volume has been made constant in order to fix the bug (in vanilla they grow over time). When they grow, plants will have their vanilla interact/collision size.

[Download for Subnautica](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/CyclopsDriftFix_SN1_v1.0.0.zip)
|
[Download for Below Zero](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/CyclopsDriftFix_BZ_v1.0.0.zip)

### Usage

Optional configuration:
* **InteractScale** `percentage as a float between 0 and 1, default: 0.8` - Growing plants will be fixed at this size % for interaction purposes. In other words, you'll be able to interact with growing plants as if they were at this percentage of their growth
  * examples: `0` will disable interacting with growing plants; `0.5` plants will be interactable as if they were at 50% growth; `1` plants will be interactable as if they were fully grown
* **VisualStartScale** `percentage as a float between 0 and 1, default: 0` - Growing plants will never be smaller (visually) than this, meaning they will visually start as if they were already this size and only visually start growing when they are actually bigger than this
  * examples: `1` will make the plant (only the visuals) appear fully grown from the moment it is planted; `0` is the default vanilla behaviour; `0.5` plants below 50% growth will appear to be at 50%

### Known issues

- if you don't have plants in your cyclops, this mod won't fix the bug for you
  - if you want to try to help fix the bug you are experiencing, drop by the [Subnautica Modding Discord](https://discord.com/invite/UpWuWwq) and mention `@Chalkos` with more info about how the bug is happening for you

## Habitat Shrinker

Modify the size of modules when building them with your Habitat Builder. You'll be able to change each size component (height/width/depth) individually, making them as thin/short/wide/tall as you want. Also supports module enlargement. And the size of plants you grow in resized growbeds or plant pots will match the growbed/pot size.

[Download for Subnautica](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/HabitatShrinker_SN1_v1.0.0.zip)
|
[Download for Below Zero](https://github.com/chalkos/Chalkos-Subnautica-Mods/releases/download/1/HabitatShrinker_BZ_v1.0.0.zip)

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

- you'll only be able to place a resized module where you would normally be able to place the non-resized version (collision is not resized)
  - this is because the boundaries for the resized module are not resized. The game does not take scale into consideration at all when calculating those
  - fix: use BuildingTweaks' full override feature and you'll be able to place things wherever you want
  - collision after building the object works fine

---------------------------------------------------

## Building from source

To build you'll need
* this https://github.com/MrPurple6411/AssemblyPublicizerdrag and drag your dll in Subnautica_Data\Managed or SubnauticaZero_Data\Managed to the exe
* adjust the paths in `Common.targets`


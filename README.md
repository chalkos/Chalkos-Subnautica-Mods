# Mods

## Habitat Shrinker

Modify the size of modules when building them with your Habitat Builder. You'll be able to change each size component (height/width/depth) individually, making them as thin/short/wide/short as you want. Also supports module enlargement.

### Usage

Check ingame mod configs, then build something.
To resize, activate resizing (default key `I`) before you place the module.

Resizing works up to 2.5x the size

The safety override allows you to resize **UNSUPPORTED** modules, like the external base modules. Use this if you want to break the game or yourself.

### Compatibility
- (SN1, BZ) BuildingTweaks: yes
- (SN1, BZ) BuilderModule: should be ok, needs testing
- (BZ) BuildInSeatruck: ok

## Known issues

- you'll only be able to place a resized module where you would normally be able to place the non-resized version (collision is not resized)
  - this is because the boundaries for the resized module are not resized. The game does not take scale into consideration at all when calculating those
  - fix: use BuildingTweaks' full override feature and you'll be able to place things wherever you want
  - collision after building the object works fine 

## Building

To build you'll need this https://github.com/MrPurple6411/AssemblyPublicizer and drag your dll in Subnautica_Data\Managed or SubnauticaZero_Data\Managed to the exe
and adjust paths in .sln or .csproj


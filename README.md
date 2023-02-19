# Planning Extended

This mod adds learning XP for stonecutting again, as well as bulk recipes, cutting three chunks at once.

## Features

### Implemented

- Draw colored designations, such as walls, doors, objects and floors
- Optionally skip (don't overwrite) already placed plan designations
- Use different shapes, e.g. points, lines, rectangles
- Load / Save plan blueprints, quickly select last loaded plans
- Cut / Copy / Paste your plans
- Undo / Redo your planned actions
- Show / Hide your planning designations (incl. global shortcut)
- Change plan texture sets and opacity for each plan type individually
- Convert your plan from vanilla Rimworld or MorePlanning mod

### Planned

- Improve grabbing position while pasting
- Add more shapes, such as ellipse, fixed shapes (e.g. sun lamp, max room size)
- Add more shape options, e.g. width
- Add plan groups / layers
- Add ability to convert plan to real blueprints
- Add toolbox
- Add text overlay to describe areas
- Add mining, pipe, cable designations?
- Add door variants
- Display wall designations according to surrounding walls
- Allow blueprints to be stored within save game or settings (cloud sync)

## Default Shortcuts
- Q, E: Rotation, Number of Columns
- Z, X: Flip, Width, Number of Rows
- V: Change Shape-Variant
- Ctrl: Color-Picker
- Alt: Skip / Replace
- Shift: Shape-Modfier (e.g. square)
- Backslash ('\\'): Global Plan Visibility Toggle

## Shapes

### Variants

- Point
- Line
  - Simple Line
  - Line Grid
- Rectangle
  - Filled
  - Grid (Number of Segments)
  - Outline
- *Ellipse
  - Filled
  - Outline
- *Fixed
  - Max Room-Size
  - Sun-Lamp
  - Orbital-Trade-Station

**To be implemented*

### Modifier

- Simple Line: draws a horizontal or vertical line
- applies square modfier

## FAQ

Can this mod be safely added and removed at any time?

> This mod can be added at any time. By removing this mod you will get a one-time error message and lose all your planning designations. Other than that, you should be fine.

I feel this mod is missing something or could be improved!
> Feel free to make any suggestions on [GitHub](https://github.com/Scherub/rw-planning-extended/). If you feel confident, you're more than welcome to branch the code and create a pull request for a newly implemented feature.


I really want this mod available in my native language!
> Unfortunately, I'll have to rely on the community here. So if you would like to support the project, feel free to translate this file (link coming soon).

Why does my performance decrease when I paint lots of planning designations?
> The problem is, that the designations aren't rendered in a batched call. When I add about 20.000 designations and fully zoom out (using Camera+), my FPS takes a slight hit. You will encounter the same problem, when using the MorePlanning mod or vanilla paint-tool (for floors or buildings/structures) and add that many designations.


## Supported Languages
- English
- Japanese translation via sub-mod by Proxyer
- Russian translation by mmavka

## Installation

* Go to the [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2877392159) and subscribe to the mod.
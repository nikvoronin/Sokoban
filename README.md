# Sokoban

This is a small (about 64Kb) [Sokoban](https://en.wikipedia.org/wiki/Sokoban) game written in C# with vector graphics. Game and levels are packed in one .exe file. Primary goals of this project are use of vector graphics, MVC-pattern, packed embedded resources, [XInput gamepads](https://github.com/nikvoronin/XInput.Wrapper) and using of GDI+ only.

[DOWNLOAD](https://github.com/nikvoronin/sokoban/releases/latest) latest release here.

![Main menu](https://cloud.githubusercontent.com/assets/11328666/16518051/83aeb698-3f89-11e6-8efb-33c1f4483686.png)

- CURSOR, WASD, [D-Pad] — to move.
- ESCAPE, [START]  — to select another level.
- CTRL+, CTRL-, [RB, LB]  — resizes game board (Numpad's +- keys and Gamepad's bumper-keys does the same).
- BACKSPACE, [B]  — to undo last movement.
- F5, [BACK]  — restarts current level.

> [!NOTE]
> You can use gamepad at select level menu: [D-Pad] to navigate, [A] as ENTER and [B] as TAB.

![Ingame screen](https://cloud.githubusercontent.com/assets/11328666/16518066/981edcca-3f89-11e6-9e6a-c54808de643d.png)

## Thanks to

- **Rabbit** levels by [Thinking Rabbit](https://en.wikipedia.org/wiki/Thinking_Rabbit)
- **SVB** levels by [Belyaev S. V.](http://svb-sokoban.narod.ru)
- **GRIGoRusha** levels by [Evgeny Grigoriev](http://grigr.narod.ru)

## Level Map

The game contains about 838 levels.

![Select level](https://cloud.githubusercontent.com/assets/11328666/16518070/9b01f09e-3f89-11e6-9c3b-18ced594cb25.png)

Level is a text file with CRLF line ending. First line is a name of the level. Next lines contains blocks of level. Each level's map ends with an empty line (CRLF only) — except the last level in a file, where the end of file itself is enough.

Legend:

- `_` ' ' underscore or space (without quotes) for empty cells.
- `#` Wall
- `$` Barrel or box
- `.` Plate
- `*` Barrel on the plate
- `@` Player's start point
- `+` Player starts over the plate

### Example 'Rabbit 01'

Do not forget that levels are separated each other by CRLF (empty line).

```text
Rabbit 01
____#####__________
____#___#__________
____#$__#__________
__###__$##_________
__#__$_$_#_________
###_#_##_#___######
#___#_##_#####__..#
#_$__$__________..#
#####_###_#@##__..#
____#_____#########
____#######________

```

## Levels Pack

Levels are contained in a single text file. You can pack one or more level files into a single `.zip` archive — each file inside the archive becomes its own pack in the level-select menu (shown by its file name, without extension), with its own list of levels. The `Rabbit` pack is selected by default when present. Zip-packed files should use the `.zip` extension; the game recognizes a zip archive by its content, not by file name.

## Command-line

```shell
> Sokoban.exe
> Sokoban.exe userDefinedLevels.pack
> Sokoban.exe userDefinedLevels.zip
```

# Sokoban

This is a small (about 50Kb) [Sokoban](https://en.wikipedia.org/wiki/Sokoban) game written in C# with vector graphics. Game and levels are packed in one .exe file.

Primary goal of this project are use of vector graphics, MVC-pattern, packed embedded resources and using of GDI+ only.

![Main menu](/doc/mainmenu002.png)

CURSOR or WASD to move.<br/>
ESCAPE to select another level.<br/>
CTRL+, CTRL- resizes game board (Numpad's +- keys does the same).<br/>
BACKSPACE to undo last movement.<br/>
F5 restarts current level.<br/>

![Ingame screen](/doc/ingame001.png)


## Thanks to

**ZipStorer** by [Jaime Olivares](https://github.com/jaime-olivares/zipstorer)<br/>
**Rabbit** levels by [Thinking Rabbit](https://en.wikipedia.org/wiki/Thinking_Rabbit)<br/>
**SVB** levels by [Belyaev S. V.](http://svb-sokoban.narod.ru)<br/>
**GRIGoRusha** levels by [Evgeny Grigoriev](http://grigr.narod.ru)


# Level Map

The game contains about 838 levels.

![Select level](/doc/selectlevel003.png)

Level is a text file with CRLF line ending. First line is a name of the level. Next lines contains blocks of level. Level map must ends with empty line (CRLF only).

Legend (without quotes):<br/>
<pre>_ ' ' underscore or space for empty cells.
# Wall
$ Barrel or box
. Plate
* Barrel on the plate 
@ Player's start point
+ Player starts over the plate
</pre>


## Example 'Rabbit 01'

Do not forget that levels are separated each other by CRLF (empty line).

<pre>Rabbit 01
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

</pre>


## Levels Pack

Levels are contained in a single text file. You can pack that file into a .zip archive. One .zip contains one level file. Game will automatically recognize zipped files.


# Command-line

Sokoban.exe<br>
Sokoban.exe userDefinedLevels.pack
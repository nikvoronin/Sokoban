# Sokoban

This is a small (about 50Kb) [Sokoban](https://en.wikipedia.org/wiki/Sokoban) game written in C# with procedurally generated graphics. Game and levels are packed in one .exe file.

Primary goal of this project are use of procedural graphic, Model-View-Control pattern, embedded resources and GDI+ only.

![Main menu](/doc/mainmenu002.png)

Cursor keys or WASD to move.<br/>
Press ESCAPE to select another level.<br/>
Ctrl+, Ctrl- change size of game window (Numpads +- does the same)

![Ingame screen](/doc/ingame001.png)

## Thanks to

ZipStorer, by Jaime Olivares<br/>
http://github.com/jaime-olivares/zipstorer

Rabbit_ levels by Thinking Rabbit, Japan.

SVB_ levels by Belyaev S. V.<br/>
http://svb-sokoban.narod.ru

GRIGoRusha_ levels by Evgeny Grigoriev<br/>
http://grigr.narod.ru

# Level Map

The game contains about 838 levels.

![Select level](/doc/selectlevel003.png)

Level is a text file with CRLF line ending. First line is a name of the level. Next lines contains blocks of level. Level map must ends with empty line (CRLF only).

Legend (without quotes):<br/>
_ ' ' underscore or space for empty cells.<br/>
# Wall<br/>
$ Barrel or box<br/>
. Plate<br/>
* Barrel on the plate<br/> 
@ Player's start point<br/>
+ Player starts over the plate

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

Levels are contained in a single text file with the predefined name 'levels.pack'. You can pack file into a .zip file with the same name. Game will automatically recognize zipped files.

# Command-line

Sokoban.exe<br>
Sokoban.exe userDefinedLevels.pack
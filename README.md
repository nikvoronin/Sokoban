# Sokoban

This is small (about 50Kb) [Sokoban](https://en.wikipedia.org/wiki/Sokoban) game written in C# with procedurally generated graphics. Game and levels they are all in one .exe file.

![Ingame screen](/doc/ingame001.png)

Cursor keys or WASD to move. Press ESCAPE to go to the main menu.


## Thanks to

ZipStorer, by Jaime Olivares<br/>
http://github.com/jaime-olivares/zipstorer

S. V. Belyaev<br/>
http://svb-sokoban.narod.ru

Evgeny Grigoriev<br/>
http://grigr.narod.ru

# Level Map
Level is a text file with CRLF line ending. First line is a name of the level. Next lines are level blocks. Level terminates with empty line (just CRLF only).

Cells legend (without quotes):<br/>
'_', ' ' Empty<br/>
'#$' Wall<br/>
'.' Plate<br/>
'*' Barrel on plate<br/> 
'@' Start point<br/>
'+' Start point on plate

## Example 'Rabbit 01'

Do not forget that levels separated each other with CRLF (empty line).

Rabbit 01<br/>
____#####__________<br/>
____#___#__________<br/>
____#$__#__________<br/>
__###__$##_________<br/>
__#__$_$_#_________<br/>
###_#_##_#___######<br/>
#___#_##_#####__..#<br/>
#_$__$__________..#<br/>
#####_###_#@##__..#<br/>
____#_____#########<br/>
____#######________<br/>


## Levels Pack

Levels contains in one text file with predefined name 'levels.pack'. You can pack it into zip-file with the same name. Game automatically recognizes when you use zip or plain text file.

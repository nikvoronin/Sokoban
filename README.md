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
<pre>____#####__________
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

Levels contains in one text file with predefined name 'levels.pack'. You can pack it into zip-file with the same name. Game automatically recognizes when you use zip or plain text file.

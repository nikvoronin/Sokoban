using System.Drawing;
using Sokoban.Core;

namespace Sokoban.Tests;

public class LogicTests
{
    // "#######"
    // "#@_.#$#"   player, empty, plate (unrelated to the tested move), an isolated barrel
    // "#######"
    private static Level StepBlockedMap() =>
        new("Step/Blocked", "#######\r\n#@_.#$#\r\n#######\r\n");

    // "#######"
    // "#@$_$.#"   pushing the first barrel right lands it on an empty cell, not the plate
    // "#######"
    private static Level PushIntoEmptyMap() =>
        new("Push Into Empty", "#######\r\n#@$_$.#\r\n#######\r\n");

    // "#####"
    // "#@$.#"     the only barrel/plate pair: pushing it right completes the level
    // "#####"
    private static Level WinMap() =>
        new("Win", "#####\r\n#@$.#\r\n#####\r\n");

    [Fact]
    public void MovePlayer_Step_MovesPlayerIntoEmptyCell()
    {
        var logic = new Logic(StepBlockedMap());

        WhatsUp result = logic.MovePlayer(new Point(1, 0));

        Assert.Equal(WhatsUp.Step, result);
        Assert.Equal(2, logic.PlayerX);
        Assert.Equal(1, logic.PlayerY);
        Assert.Equal(1, logic.Steps);
    }

    [Fact]
    public void MovePlayer_BlockedByWall_DoesNotMovePlayer()
    {
        var logic = new Logic(StepBlockedMap());

        WhatsUp result = logic.MovePlayer(new Point(-1, 0));

        Assert.Equal(WhatsUp.Nothing, result);
        Assert.Equal(1, logic.PlayerX);
        Assert.Equal(1, logic.PlayerY);
        Assert.Equal(0, logic.Steps);
    }

    [Fact]
    public void MovePlayer_PushBarrelIntoEmptyCell_MovesBarrelWithoutCompletingLevel()
    {
        var logic = new Logic(PushIntoEmptyMap());

        WhatsUp result = logic.MovePlayer(new Point(1, 0));

        Assert.Equal(WhatsUp.Move, result);
        Assert.Equal(2, logic.PlayerX);
        Assert.Equal(Cell.Empty, logic.CellAt(2, 1));
        Assert.Equal(Cell.Barrel, logic.CellAt(3, 1));
    }

    [Fact]
    public void MovePlayer_PushBarrelOntoLastPlate_ReturnsWin()
    {
        var logic = new Logic(WinMap());

        WhatsUp result = logic.MovePlayer(new Point(1, 0));

        Assert.Equal(WhatsUp.Win, result);
        Assert.Equal(1, logic.InPlace);
        Assert.Equal(Cell.BarrelOnPlate, logic.CellAt(3, 1));
    }

    [Fact]
    public void Undo_RevertsPushedBarrelAndPlayerPosition()
    {
        var logic = new Logic(PushIntoEmptyMap());
        logic.MovePlayer(new Point(1, 0));

        WhatsUp result = logic.Undo();

        Assert.Equal(WhatsUp.Undo, result);
        Assert.Equal(1, logic.PlayerX);
        Assert.Equal(Cell.Barrel, logic.CellAt(2, 1));
        Assert.Equal(Cell.Empty, logic.CellAt(3, 1));
    }

    [Fact]
    public void Undo_WithEmptyHistory_ReturnsNothing()
    {
        var logic = new Logic(StepBlockedMap());

        WhatsUp result = logic.Undo();

        Assert.Equal(WhatsUp.Nothing, result);
    }
}

using System.Drawing;
using Sokoban.Core;

namespace Sokoban.Tests;

public class LevelTests
{
    [Fact]
    public void Parse_ComputesDimensionsCountsAndCells_ForPlayerStartsAtMarker()
    {
        var level = new Level("Test Level", "@ $.*#\r\n");

        Assert.Equal("Test Level", level.Name);
        Assert.Equal(6, level.Width);
        Assert.Equal(1, level.Height);
        Assert.Equal(new Point(0, 0), level.StartAt);
        Assert.Equal(2, level.Barrels);
        Assert.Equal(2, level.Plates);
        Assert.Equal(1, level.InPlace);

        Assert.Equal(Cell.Empty, level.Cells[0, 0]);          // '@' -> Empty, player start recorded separately
        Assert.Equal(Cell.Empty, level.Cells[1, 0]);          // ' ' -> Empty
        Assert.Equal(Cell.Barrel, level.Cells[2, 0]);         // '$'
        Assert.Equal(Cell.Plate, level.Cells[3, 0]);          // '.'
        Assert.Equal(Cell.BarrelOnPlate, level.Cells[4, 0]);  // '*'
        Assert.Equal(Cell.Wall, level.Cells[5, 0]);           // '#'
    }

    [Fact]
    public void Parse_PlayerOnPlateMarker_CountsAsPlateAndSetsStartAt()
    {
        var level = new Level("Player On Plate", "+#\r\n");

        Assert.Equal(2, level.Width);
        Assert.Equal(1, level.Height);
        Assert.Equal(new Point(0, 0), level.StartAt);
        Assert.Equal(0, level.Barrels);
        Assert.Equal(1, level.Plates);
        Assert.Equal(0, level.InPlace);

        Assert.Equal(Cell.Plate, level.Cells[0, 0]); // '+' -> Plate, player starts there
        Assert.Equal(Cell.Wall, level.Cells[1, 0]);  // '#'
    }
}

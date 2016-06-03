namespace Sokoban.Core
{
    public enum Cell : byte
    {
        Empty           = (byte)'_',
        EmptySp         = (byte)' ',
        Barrel          = (byte)'$',
        Plate           = (byte)'.',
        BarrelOnPlate   = (byte)'*',
        Wall            = (byte)'#',
        PlayerStartsAt  = (byte)'@',
        PlayerOnPlate   = (byte)'+'
    }
}

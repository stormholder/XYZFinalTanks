namespace XYZFinalTanks.Tanks;

internal struct Cell
{
    public int X;
    public int Y;
    public int Width = 4;
    public int Height = 2;

    public Cell(int x, int y) { X = x; Y = y; }
}

namespace XYZFinalTanks.Shared;

public interface IRenderer
{
    public void SetPixel(int w, int h, char val, byte colorIdx);
    public void DrawString(string text, int atWidth, int atHeight, ConsoleColor color);
    public void Render();
    public void Clear();

    public int GetHeight();
    public int GetWidth();
}
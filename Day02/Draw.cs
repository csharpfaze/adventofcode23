using System.Drawing;

public class Draw
{
    public int Amount { get; set; }
    public Color Color { get; set; }
    public Draw(int amount, Color color)
    {
        Amount = amount;
        Color = color;
    }
}
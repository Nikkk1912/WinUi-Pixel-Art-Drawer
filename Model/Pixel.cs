using Microsoft.UI.Xaml.Media;

namespace Pixel_Art_Project.Model;

public class Pixel(Brush color, int layer)
{
    private Brush _color = color;

    public Brush Color { get; set; } = color;

    public int Layer { get; set; } = layer;
}
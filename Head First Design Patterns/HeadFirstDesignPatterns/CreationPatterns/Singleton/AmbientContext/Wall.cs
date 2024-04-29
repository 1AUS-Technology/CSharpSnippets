using System.Drawing;

namespace HeadFirstDesignPatterns.CreationPatterns.Singleton.AmbientContext;

public class Wall(Point start, Point end, int? height = null)
{
    public Point Start = start;
    public Point End = end;
    public int Height = height ?? BuildingContext.Current.Height;
}
using System;

namespace Pixel_Art_Project.Events;

public class GridSizeChangedEventArgs : EventArgs
{
    public int Columns { get; }
    public int Rows { get; }

    public GridSizeChangedEventArgs(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
    }
}
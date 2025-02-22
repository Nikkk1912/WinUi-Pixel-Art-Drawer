using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace Pixel_Art_Project.Model
{
    public class PixelSheet
    {
        private static int _columns = 30;
        private static int _rows = 20;
        private static double _pixelSize = 40;
        
        private static PixelSheet _instance;
        private static readonly object _lock = new object();
        private Pixel[,] _pixels;

        private PixelSheet(int rows, int cols)
        {
            _pixels = new Pixel[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _pixels[i, j] = new Pixel(new SolidColorBrush(Colors.Transparent), 0);
                }
            }

            Console.WriteLine("Pixel sheet array size: " + _pixels.GetLength(0) * _pixels.GetLength(1));
        }

        public static PixelSheet Instance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new PixelSheet(_rows, _columns);
                }
                return _instance;
            }
        }

        public Pixel GetPixel(int rowNum, int colNum)
        {
            return _pixels[rowNum, colNum];
        }

        public void SetPixelColor(int rowNum, int colNum, Brush color)
        {
            _pixels[rowNum, colNum].Color = color;
            Console.WriteLine($"Pixel at ({rowNum}, {colNum}) has color: {color}");
        }

        public static int Columns
        {
            get => _columns;
            set => _columns = value;
        }
        public static int Rows
        {
            get => _rows;
            set => _rows = value;
        }
        public static double PixelSize
        {
            get => _pixelSize;
            set => _pixelSize = value;
        }
        
    }
}
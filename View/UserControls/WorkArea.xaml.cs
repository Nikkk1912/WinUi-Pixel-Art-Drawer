using System;
using System.Diagnostics;
using Windows.Devices.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Pixel_Art_Project.Model;
using PointerDeviceType = Microsoft.UI.Input.PointerDeviceType;
using Windows.UI;
using Pixel_Art_Project.Events;

namespace Pixel_Art_Project.View.UserControls
{
    public sealed partial class WorkArea : UserControl
    {
        private double _pixelSize = 40;

        private PixelSheet _pixelSheet = PixelSheet.Instance();
        private ColorInventory _colorInventory = ColorInventory.Instance;
        
        private Canvas _pixelCanvas;
        private Grid _containerGrid;

        private bool _isPainting;
        private Brush _selectedColor;
        
        // Constructor
        public WorkArea()
        {
            InitializeComponent();
            
            KeyDown += WorkArea_KeyDown;

            CreateUi();
            UpdatePixelSize();
        }

        // Methods
        private void CreateUi()
        {
            if (WorkAreaGrid != null && PixelCanvas != null)
            {
                _containerGrid = WorkAreaGrid;
                _pixelCanvas = PixelCanvas;

                Content = _containerGrid;

                SizeChanged += WorkArea_SizeChanged;
                PointerWheelChanged += WorkArea_MouseWheel;
            }
        }
        private void WorkArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePixelSize();
            GeneratePixelCanvas();
        }
        private void UpdatePixelSize()
        {
            if (_containerGrid is not { ActualWidth: > 0, ActualHeight: > 0 }) return;
            _pixelSize = Math.Min(_containerGrid.ActualWidth / PixelSheet.Columns, _containerGrid.ActualHeight / PixelSheet.Rows);
            Debug.WriteLine("Pixel size - " + _pixelSize);
        }
        private void GeneratePixelCanvas()
        {
            _pixelCanvas.Children.Clear();

            for (var x = 0; x < PixelSheet.Columns; x++)
            {
                for (var y = 0; y < PixelSheet.Rows; y++)
                {
                    var rect = new Rectangle
                    {
                        Width = _pixelSize,
                        Height = _pixelSize,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = _pixelSheet.GetPixel(y, x).Color
                    };
                    
                    var positionX = ((WorkAreaGrid.ActualWidth - (_pixelSize * PixelSheet.Columns)) / 2) + (x * _pixelSize);
                    var positionY = ((WorkAreaGrid.ActualHeight - (_pixelSize * PixelSheet.Rows)) / 2) + (y * _pixelSize);
                    
                    Canvas.SetLeft(rect, positionX);
                    Canvas.SetTop(rect, positionY);

                    rect.PointerPressed += (s, e) =>
                    {
                        UpdateSelectedColor(e);
                        _isPainting = true;
                        PaintPixel(s, e);
                    };

                    rect.PointerMoved += (s, e) =>
                    {
                        if (_isPainting)
                        {
                            PaintPixel(s, e);
                        }
                    };

                    rect.PointerReleased += (s, e) => { _isPainting = false; };

                    _pixelCanvas.Children.Add(rect);
                }
            }
        }
        private void PaintPixel(object sender, PointerRoutedEventArgs e)
        {
            var rect = sender as Rectangle;
            if (rect == null) return;

            var mousePosition = e.GetCurrentPoint(_pixelCanvas).Position;

            // int clickedColumn = (int)((mousePosition.X - ((WorkAreaGrid.ActualWidth / 2) - (_pixelSize * (PixelSheet.Columns / 2)))) / _pixelSize);
            // int clickedRow = (int)((mousePosition.Y - ((WorkAreaGrid.ActualHeight / 2) - (_pixelSize * (_rows / 2)))) / _pixelSize);

            int clickedColumn = (int)((mousePosition.X - ((WorkAreaGrid.ActualWidth - (_pixelSize * PixelSheet.Columns)) / 2)) /
                                      _pixelSize);
            int clickedRow = (int)((mousePosition.Y - ((WorkAreaGrid.ActualHeight - (_pixelSize * PixelSheet.Rows)) / 2)) /
                                   _pixelSize);


            if (rect.Fill == _selectedColor || clickedRow >= PixelSheet.Rows || clickedColumn >= PixelSheet.Columns) return;
            rect.Fill = _selectedColor;
            _pixelSheet.SetPixelColor(clickedRow, clickedColumn, rect.Fill);
        }
        private void UpdateSelectedColor(PointerRoutedEventArgs e)
        {
            _selectedColor = e.Pointer.PointerDeviceType switch
            {
                PointerDeviceType.Mouse => e.GetCurrentPoint(_pixelCanvas).Properties.IsLeftButtonPressed
                    ? new SolidColorBrush(_colorInventory.Color1) 
                    : new SolidColorBrush(_colorInventory.Color2),
                _ => _selectedColor
            };
        }
        private void WorkArea_MouseWheel(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("Pixel size - " + _pixelSize);
            double maxPixelSizeWidth = _containerGrid.ActualWidth / PixelSheet.Columns;
            double maxPixelSizeHeight = _containerGrid.ActualHeight / PixelSheet.Rows;

            double maxPixelSize = Math.Min(maxPixelSizeHeight, maxPixelSizeWidth);
            double minPixelSize = PixelSheet.Columns * 2;
            
            Debug.WriteLine("Max pixel size - " + maxPixelSize);

            if (_pixelSize > maxPixelSize) _pixelSize = maxPixelSize - 1;

            if (_pixelSize <= maxPixelSizeWidth && _pixelSize <= maxPixelSizeHeight && _pixelSize >= minPixelSize)
            {
                double zoomFactor = e.GetCurrentPoint(_pixelCanvas).Properties.MouseWheelDelta > 0 ? 1.1 : 0.9;
                _pixelSize *= zoomFactor;

                if (_pixelSize > maxPixelSize)
                {
                    _pixelSize = maxPixelSize;
                }


                if (_pixelSize < minPixelSize)
                {
                    _pixelSize = minPixelSize;
                }
            }

            GeneratePixelCanvas();
        }
        private void WorkArea_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            
            if (e.Key == Windows.System.VirtualKey.X)
            {
                _colorInventory.SwapColors();
            }
        }
    }
}
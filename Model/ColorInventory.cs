using System;
using System.ComponentModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Pixel_Art_Project.Model;

public class ColorInventory : INotifyPropertyChanged
{

    private static ColorInventory _instance;
    private static readonly object _lock = new object();
    
    private Color _color1;
    private Color _color2;

    private ColorInventory()
    {
        _color1 = Colors.Aquamarine;
        _color2 = Colors.White;
    }

    public static ColorInventory Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ColorInventory();
                }
                return _instance;
            }
        }
    }
    
    public Color Color1
    {
        get => _color1;
        set
        {
            if (_color1 != value)
            {
                _color1 = value;
                OnPropertyChanged(nameof(Color1));
            }
        }
    }

    public Color Color2
    {
        get => _color2;
        set
        {
            if (_color2 != value)
            {
                _color2 = value;
                OnPropertyChanged(nameof(Color2));
            }
        }
    }
    
    public void SwapColors()
    {
        (_color1, _color2) = (_color2, _color1);
        
        OnPropertyChanged(nameof(Color1));
        OnPropertyChanged(nameof(Color2));
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
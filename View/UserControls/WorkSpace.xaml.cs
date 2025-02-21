using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Pixel_Art_Project.Model;

namespace Pixel_Art_Project.View.UserControls
{
    public sealed partial class WorkSpace : UserControl
    {
        private ColorInventory _colorInventory = ColorInventory.Instance;
        public WorkSpace()
        {
            InitializeComponent();
            
            PrimaryColorPicker.DataContext = ColorInventory.Instance;
            PrimaryColorPicker.Color = ColorInventory.Instance.Color1;
            
            KeyDown += WorkArea_KeyDown;
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

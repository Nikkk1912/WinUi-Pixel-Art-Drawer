using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Pixel_Art_Project.View.UserControls
{
    public sealed partial class MenuBarStrip : UserControl
    {
        public MenuBarStrip()
        {
            this.InitializeComponent();
        }
        
        private void ChangeGridSize_Click(object sender, RoutedEventArgs e)
        {
            // Call the method in the main window to open the grid size change dialog
            var parentWindow = Window.Current.Content as MainWindow;
            parentWindow?.ChangeGridSize_Click(sender, e);
        }
    }
}

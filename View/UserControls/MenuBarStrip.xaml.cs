using System;
using Microsoft.UI.Xaml;

namespace Pixel_Art_Project.View.UserControls
{
    public sealed partial class MenuBarStrip
    {
        public event EventHandler CallWorkSpaceMethod = null!;
        
        public MenuBarStrip()
        {
            InitializeComponent();
        }
        
        private void ChangeGridSize_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (Application.Current as App)?.MWindow as MainWindow; 
            parentWindow?.OpenGridSizeDialog();
        }
    }
}

using Microsoft.UI.Xaml;
using Pixel_Art_Project.View.UserControls;


namespace Pixel_Art_Project
{
    public sealed partial class MainWindow : Window
    {
        private WorkArea _workArea;
        
        public MainWindow()
        {
            this.InitializeComponent();
            _workArea = (WorkArea)FindName("WorkArea");
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Pixel_Art_Project.Model;
using Pixel_Art_Project.View.UserControls;


namespace Pixel_Art_Project
{
    public sealed partial class MainWindow
    {
        
        private ColorInventory _colorInventory = ColorInventory.Instance;
        private WorkArea _workArea;
        
        public MainWindow()
        {
            InitializeComponent();
            
            PrimaryColorPicker.DataContext = ColorInventory.Instance;
            PrimaryColorPicker.Color = ColorInventory.Instance.Color1;
            // KeyDown += WorkArea_KeyDown;
        }
        
        public void OpenGridSizeDialog()
        {
            var gridSizeDialog = new ContentDialog
            {
                Title = "Change Grid Size",
                PrimaryButtonText = "OK",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            var stackPanel = new StackPanel();

            var rowsTextBox = new TextBox { Header = "Rows", Margin = new Thickness(0, 0, 0, 5) };
            var columnsTextBox = new TextBox { Header = "Columns" };

            stackPanel.Children.Add(rowsTextBox);
            stackPanel.Children.Add(columnsTextBox);

            gridSizeDialog.Content = stackPanel;

            gridSizeDialog.PrimaryButtonClick += (sender, args) =>
            {
                if (int.TryParse(rowsTextBox.Text, out int rows) && int.TryParse(columnsTextBox.Text, out int cols))
                {
                    PixelSheet.Columns = cols;
                    PixelSheet.Rows = rows;
                    // _workArea.OnGridSizeChanged(cols, rows);
                }
                else
                {
                    // Handle invalid input
                    var errorDialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Please enter valid integer values for rows and columns.",
                        CloseButtonText = "OK"
                    };
                    errorDialog.ShowAsync();
                }
            };

            gridSizeDialog.XamlRoot = Content.XamlRoot;
            
            gridSizeDialog.ShowAsync();
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

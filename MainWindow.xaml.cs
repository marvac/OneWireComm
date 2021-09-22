using MaterialDesignExtensions.Controls;
using OneWireComm.ViewModels;
using System.Windows;

namespace OneWireComm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void InitializeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializeTouchpen();
        }

        private void ReadButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ReadButton();
        }
    }
}

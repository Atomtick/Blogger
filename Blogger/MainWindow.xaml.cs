using Blogger;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WinDiskBlogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new MainWindowViewModel();
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }

    public class IndexConverter : IValueConverter
    {

    }
}
using Blogger;
using System.Windows;

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
}
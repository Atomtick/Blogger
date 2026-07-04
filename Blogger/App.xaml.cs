using System.Reflection;
using System.Windows;

namespace WinDiskBlogger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        private void MyTrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
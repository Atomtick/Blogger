using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

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

            // 1. 在后台动态创建一个 TaskbarIcon
            var trayIcon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();

            // 2. 直接提取 Windows 系统自带的“信息”图标，绕过所有的路径和格式问题
            trayIcon.IconSource = new BitmapImage(new Uri("pack://application:,,,/Blogger;component/assets/blogger.ico"));

            // 3. 设置提示文字并显示
            trayIcon.ToolTipText = "Blogger";

            trayIcon.TrayLeftMouseDown += (s, e) =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                {
                    Application.Current.MainWindow.Show();
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                }
            };
            trayIcon.TrayRightMouseDown += (s, e) =>
            {
                if (MessageBox.Show("Are you sure to shut down app?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            };
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
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

        private Mutex _mutex; // 定义成字段，防止被释放。
        private bool _createNew; // 在Application的Exit的事件处理程序中用于区分是单例正常退出还是多开退出

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(
                initiallyOwned: true,
                name: "1797D68E-CEAF-446B-9FCC-8AF604D570BE",
                out _createNew
            );
            if (_createNew == false)
            {
                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);

            // 1. 在后台动态创建一个 TaskbarIcon
            var trayIcon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();

            // 2. 直接提取 Windows 系统自带的“信息”图标，绕过所有的路径和格式问题
            trayIcon.IconSource = new BitmapImage(
                new Uri("pack://application:,,,/Blogger;component/assets/blogger.ico")
            );

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
                if (
                    MessageBox.Show(
                        "Are you sure to shut down app?",
                        "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    ) == MessageBoxResult.Yes
                )
                {
                    Application.Current.Shutdown();
                }
            };
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}

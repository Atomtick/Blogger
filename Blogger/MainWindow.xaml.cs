using Atomtick.Configuration;
using Blogger;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;

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

        private void TreeViewItem_RequestBringIntoView(
            object sender,
            RequestBringIntoViewEventArgs e
        )
        {
            // 直接阻断事件，彻底彻底禁止点击时的所有自动滚动（包括水平和垂直）
            e.Handled = true;
        }

        private void TreeView_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            // 直接阻断事件，彻底彻底禁止点击时的所有自动滚动（包括水平和垂直）
            e.Handled = true;
        }

        private void self_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var configSource = Application.Current.Properties["configs"] as PrimitiveConfigSource;
            // 获取当前窗口的句柄
            var interopHelper = new WindowInteropHelper(this);
            var currentScreen = Screen.FromHandle(interopHelper.Handle);
            configSource.BeginTransaction(out var transactionId);
            configSource.Write(transactionId, "Blogger.MainWindow.WidthRatio", (this.ActualWidth / currentScreen.WorkingArea.Width).ToString(".00"));
            configSource.Write(transactionId, "Blogger.MainWindow.HeightRatio", (this.ActualHeight / currentScreen.WorkingArea.Height).ToString(".00"));
            configSource.CommitTransaction(transactionId);

            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
        }

        private void MyTrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }

        private void self_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void self_Loaded(object sender, RoutedEventArgs e)
        {
            var interopHelper = new WindowInteropHelper(this);
            var currentScreen = Screen.FromHandle(interopHelper.Handle);

            var configSource = Application.Current.Properties["configs"] as PrimitiveConfigSource;

            var width_i = configSource.SelectConfigItem("Blogger.MainWindow.WidthRatio");
            var width = configSource.Read(width_i).ToDouble();
            var height_i = configSource.SelectConfigItem("Blogger.MainWindow.HeightRatio");
            var height = configSource.Read(height_i).ToDouble();

            this.Width = currentScreen.WorkingArea.Width * width;
            this.Height = currentScreen.WorkingArea.Height * height;
        }
    }

    public class FileIconConverter : IMultiValueConverter
    {
        static BitmapImage LoadImage(string path)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        static ImageSource Markdown = LoadImage("pack://application:,,,/assets/Icons/markdown.png");
        static ImageSource Rar = LoadImage("pack://application:,,,/assets/Icons/rar.png");
        static ImageSource Excel = LoadImage("pack://application:,,,/assets/Icons/excel.png");
        static ImageSource Word = LoadImage("pack://application:,,,/assets/Icons/word.png");
        static ImageSource Pdf = LoadImage("pack://application:,,,/assets/Icons/pdf.png");
        static ImageSource Txt = LoadImage("pack://application:,,,/assets/Icons/txt.png");
        static ImageSource Image = LoadImage("pack://application:,,,/assets/Icons/image.png");
        static ImageSource Music = LoadImage("pack://application:,,,/assets/Icons/music.png");
        static ImageSource Video = LoadImage("pack://application:,,,/assets/Icons/video.png");
        static ImageSource Folder = LoadImage("pack://application:,,,/assets/Icons/folder.png");
        static ImageSource FolderOpen = LoadImage(
            "pack://application:,,,/assets/Icons/folder_open.png"
        );
        static ImageSource Exe = LoadImage("pack://application:,,,/assets/Icons/exe.png");

        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            var extension = Path.GetExtension(values[0] as string);
            var itemType = (ItemType)values[1];
            var isExpanded = (bool)values[2];

            if (itemType == ItemType.Folder && isExpanded)
            {
                return FolderOpen;
            }
            if (itemType == ItemType.Folder && isExpanded == false)
            {
                return Folder;
            }

            switch (extension)
            {
                case ".md":
                    return Markdown;
                case ".zip":
                case ".rar":
                case ".7z":
                case ".tar":
                case ".iso":
                    return Rar;
                case ".csv":
                case ".xlsx":
                case ".xls":
                    return Excel;
                case ".docx":
                case ".doc":

                    return Word;
                case ".pdf":
                    return Pdf;
                case ".txt":
                    return Txt;
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".bmp":
                case ".tiff":
                case ".svg":
                case ".webp":
                case ".ico":
                case ".heic":
                case ".raw":
                    return Image;
                case ".mp3":
                case ".wav":
                case ".flac":
                    return Music;
                case ".mp4":
                case ".avi":
                case ".mkv":
                case ".mov":
                case ".wmv":
                case ".flv":
                    return Video;
                case ".exe":
                    return Exe;
                default:
                    return Txt;
            }
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}

using WinDiskBlogger;

namespace Blogger.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ItemType ItemType { get; set; }
        public FileSystemItem Parent { get; set; }
        public List<FileSystemItem> SubItems { get; set; }

        public static FileSystemItem[] Build(string rootFolder)
        {


        }
    }
}
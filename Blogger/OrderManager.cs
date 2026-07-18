using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WinDiskBlogger
{
    internal class OrderManager
    {
        public static OrderManager Instance { get; private set; } = new OrderManager();

        private OrderManager()
        { }

        public const string ORDER_FILE_NAME = ".order";

        public async Task<Dictionary<string, int>> LoadOrder(string directoryPath)
        {
            var orderDict = new Dictionary<string, int>();
            string orderFilePath = System.IO.Path.Combine(directoryPath, ORDER_FILE_NAME);
            if (System.IO.File.Exists(orderFilePath))
            {
                var lines = await System.IO.File.ReadAllLinesAsync(orderFilePath).ConfigureAwait(false);
                for (int i = 0; i < lines.Length; i++)
                {
                    string fileName = lines[i].Trim();
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        orderDict[fileName] = i;
                    }
                }
            }
            return orderDict;
        }

        public async Task SaveOrder(string directoryPath, IEnumerable<string> itemNames)
        {
            string orderFilePath = System.IO.Path.Combine(directoryPath, ORDER_FILE_NAME);
            await System.IO.File.WriteAllLinesAsync(orderFilePath, itemNames).ConfigureAwait(false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace Blogger.Avalonia
{
    public class Node<T> : BindableBase
    {
        public T Data { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
        public Node<T> Parent { get; set; }
        public List<Node<T>> SubNodes { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsFile { get; set; }
        public bool IsSelected { get; set; }
    }
}

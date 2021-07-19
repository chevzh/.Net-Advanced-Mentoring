using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Adapter.Task1
{
    public class MyContainer<T> : IContainer<T>
    {
        public MyContainer(IElements<T> elements)
        {
            Items = elements.GetElements();
            Count = Items.Count();
        }

        public IEnumerable<T> Items { get; }

        public int Count { get; }
    }
}

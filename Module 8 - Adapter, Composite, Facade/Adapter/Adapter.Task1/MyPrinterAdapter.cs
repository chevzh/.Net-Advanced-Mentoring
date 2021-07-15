using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter.Task1
{
    public class MyPrinterAdapter: IMyPrinter
    {
        private readonly Printer printer;

        public MyPrinterAdapter(Printer printer)
        {
            this.printer = printer;
        }

        public void Print<T>(IElements<T> elements)
        {
            printer.Print<T>(new MyContainer<T>(elements));
        }
    }
}

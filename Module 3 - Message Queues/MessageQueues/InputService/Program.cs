using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InputService
{
    class Program
    {
        static void Main(string[] args)
        {
            InputService inputService = new InputService();            

            inputService.Start();

            Console.ReadLine();
        }
    }
}

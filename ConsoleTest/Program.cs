using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new MenuParser().GetTodaysMenu();
            Console.WriteLine(x);
        }
    }
}

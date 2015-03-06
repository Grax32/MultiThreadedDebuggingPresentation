using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var counter = 0;
            Parallel.For(0, 1000, v =>
            {
                var x = counter + 1;
                counter = x;
                // counter++;
            });

            if (counter != 1000)
            {
                throw new Exception("Counter not 1000");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReturnValueFromProcess
{
    class Program
    {
        static int Main(string[] args)
        {
            Thread.Sleep(5000);
            return 40;
        }
    }
}

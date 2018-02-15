using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchValueFromProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo startInfo =new ProcessStartInfo();
            startInfo.FileName = @"C:\Users\vijay.siraparapu\OneDrive\Visual Studio Projects\Algorithmica\ReturnValueFromProcess\bin\Release\ReturnValueFromProcess.exe";
            startInfo.Arguments = "";
            var process= Process.Start(startInfo);
            process.WaitForExit();
            Console.WriteLine(process.ExitCode);
        }
    }
}

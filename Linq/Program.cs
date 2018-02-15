using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums = from n in numbers where n < 5 select n;   //Query based syntax

            var methSyn = numbers.Where(n => n <= 5);

            foreach (var VARIABLE in methSyn)
            {
                Console.WriteLine(VARIABLE);
            }

            DataTable dt = null;

            var ThrddHighSal =dt.AsEnumerable().OrderByDescending(rw => rw["Salary"]).Skip(2).Select(rw1 => rw1["salary"]).FirstOrDefault();
        }


        class  testLinq
        {
             string name;
             int x;
        }
    }
}

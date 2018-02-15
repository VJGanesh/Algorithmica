using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    class Program
    {
        private static volatile bool cancel = false;
        static void Main(string[] args)
        {
            //int x = 10;
            //List<String> tst = new List<String>();
            Thread t=new Thread(SayHello);
            t.Start();
            //t.Start(10);
            //Console.WriteLine("press enter to cancel");
            //Console.ReadLine();
            //t.Abort();
            //cancel = true;
            //t.Join();
            Console.WriteLine("Main Done");
        }

        static void Add(int a, int b)
        {
            Console.WriteLine("[{0}] Add,({1},{2}=>{3})", Thread.CurrentThread.ManagedThreadId, a,b, a+b);
        }

        static void SayHello()
        {
            while (!cancel)
            {
                Console.WriteLine("Hellow Thread");
                Thread.Sleep(500);
            }
            Console.WriteLine("Thread Exited gracefully");
        }
        //static void SayHello(object arg)
        //{
        //    int i = (int)arg;
        //    //while (!cancel)
        //    //{
        //    //    Console.WriteLine("Hellow Thread");
        //    //    Thread.Sleep(1000);
        //    //}
        //    //Console.WriteLine("Thread Exited gracefully");
        //    for (int j = 1; j <= i; j++)
        //    {
        //        Console.WriteLine("[{0}] Hello, World,{1}!,({2})", Thread.CurrentThread.ManagedThreadId, j, Thread.CurrentThread.IsBackground);
        //    }
        //}
    }

    //public class List1 : List<String>
    //{
        
    //}

    //public  static class tst
    //{

    //    public static List1 Where(this List1 lst, Predicate<string> pred)
    //    {
            
    //        return new List1();
    //    }
    //}
}

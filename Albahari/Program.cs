using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Albahari
{
    class Program
    {
        static void Main(string[] args)
        {
            //dynamic t =new ThreadStart(sayHello);
            
            //ThreadStart t = sayHello;
            //ParameterizedThreadStart pt = sayHello;
            //Thread T=new Thread(t);
            //T.Start(2);
            Func<string, String> meth = sayHello;
            IAsyncResult res = meth.BeginInvoke("Vijay", Done,meth);
            //meth.EndInvoke(res);
            
            Thread.Sleep(100000);
           

        }

        static void Done(IAsyncResult res)
        {
            var s =(Func<string,string>) res.AsyncState;
            var res1 = s.EndInvoke(res);
            Console.WriteLine("Call Back res: " + res1);
        }
        static string sayHello(string msg)
        {
            Console.WriteLine(msg);
            System.Diagnostics.Debug.Print(msg);
            Thread.Sleep(2000);
            return msg;
        }

        //static void sayHello(object x)
        //{
          
        //}
    }
}

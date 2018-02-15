using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsynchronousProgrammingModel;


namespace DelegateInners
{
    public delegate void BinaryOperation(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called",Thread.CurrentThread.ManagedThreadId);
#if(false)
            Add(2,2);
#else
            BinaryOperation binaryOp = Add;


            //https://msdn.microsoft.com/en-us/library/2e08f6yc(v=vs.110).aspx
            //https://www.codeproject.com/Articles/10311/What-s-up-with-BeginInvoke
            //This makes a call to ThreadPool.QueueUserWorkItem and executes the Add method 
           IAsyncResult asyncResult= binaryOp.BeginInvoke(2, 2, null, null);
           binaryOp.EndInvoke(asyncResult);
#endif
            Console.WriteLine("[{0}] Main Done", Thread.CurrentThread.ManagedThreadId);


            int i = 20;
           //i.Saleem();

        }

        static void Add(int a, int b)
        {
            Console.WriteLine("[{0}] Add ({1},{2})=>{3}", Thread.CurrentThread.ManagedThreadId,a ,b,a+b);
        }
    }
}

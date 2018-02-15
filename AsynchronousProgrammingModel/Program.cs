using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProgrammingModel
{
    //For complete information
    //https://msdn.microsoft.com/en-us/library/2e08f6yc(v=vs.110).aspx
   public class AsyncMain
    {
        static void Main(string[] args)
        {
            // The asynchronous method puts the thread id here.
            int threadId;

            // Create an instance of the test class.
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(100,
                out threadId, new AsyncCallback(CallbackMethod), "Test Object State");
            

            Thread.Sleep(0);
            Console.WriteLine("Main thread {0} does some work.",
                Thread.CurrentThread.ManagedThreadId);

             //Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            //while (result.IsCompleted == false)
            //{
            //    Thread.Sleep(250);
            //    Console.Write(".");
            //}

            // Perform additional processing here.
            // Call EndInvoke to retrieve the results.
            //string returnValue = caller.EndInvoke(out threadId, result);

            //// Close the wait handle.
            //result.AsyncWaitHandle.Close();

            //while (result.IsCompleted==false)
            //{
            //    Thread.Sleep(250);
            //    Console.Write(".");
            //}

            //Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".",
            //    threadId, returnValue);
            //IAsyncResult ss;
            //ss.AsyncState;

        }

        static void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;

            // Retrieve the format string that was passed as state 
            // information.
            string formatString = (string)ar.AsyncState;

            // Define a variable to receive the value of the out parameter.
            // If the parameter were ref rather than out then it would have to
            // be a class-level field so it could also be passed to BeginInvoke.
            int threadId = 0;

            // Call EndInvoke to retrieve the results.
            string returnValue = caller.EndInvoke(out threadId, ar);

            // Use the format string to format the output message.
            Console.WriteLine(formatString, threadId, returnValue);
            Console.WriteLine("Callback Thread: " + Thread.CurrentThread.ManagedThreadId);
            
        }
    }

    public class AsyncDemo
    {
        public string TestMethod(int callDuration, out int threadId)
        {
            Console.WriteLine("Test method Thread:" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }
    // The delegate must have the same signature as the method
    // it will call asynchronously.
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);


    public class tst
    {
        private int x=10;
        private static string tst1;
        //public void tstttt(int c)
        //{

        //    collection.Where((x) => { return x[0].ToString() == "saleem" ; }).Select()
        //    Console.WriteLine((x*c).ToString());
        //}

        public static void stTstt(int c)
        {

            
            Console.WriteLine(tst1);

        }

        private List<DataRow> collection = null;

        



    }
}

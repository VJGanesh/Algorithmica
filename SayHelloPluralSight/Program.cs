using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace SayHelloPluralSight
{
    class ThreadStart   
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[{0}] Processor/Core count = {1}", Thread.CurrentThread.ManagedThreadId,Environment.ProcessorCount);
            
            Thread t = new Thread(SayHello);
            t.Name = "Hello Thread";
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();

            
            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
        }

        
        static void SayHello()
        {
            Console.WriteLine("[{0}] Hello world", Thread.CurrentThread.ManagedThreadId);
        }
        
    }

    class ParameterizedThreadStart
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[{0}] Processor/Core count = {1}", Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount);
            
            Thread t = new Thread(SayHello);
            t.Name = "Hello Thread";
            t.Priority = ThreadPriority.BelowNormal;
            t.Start("Hello World");

            Console.WriteLine("Press Enter to cancel.");
        }
        static void SayHello(object stateArg)
        {
            string msg = stateArg as string;
            Console.WriteLine("[{0}] {1}", Thread.CurrentThread.ManagedThreadId, msg);
        }
    }

    class CoordinatingThreadShutdown
    {
        private static volatile bool cancel = false;

        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[{0}] Processor/Core count = {1}", Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount);
            Thread t = new Thread(SayHello);
            t.Start();

            Console.WriteLine("Press Enter to cancel.");

            Console.ReadLine();
            cancel = true;
            t.Join();
        }

        static void SayHello()
        {
            while (!cancel)
            {
                Console.WriteLine("[{0}] Hello world", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }

    class IsBackGround
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Thread t=new Thread(SayHello);

            //change IsBackground property and conly press ctrl + F5 to see the difference
            t.IsBackground = true;
            t.Start(10);
            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
        }

        static void SayHello(object arg)
        {
            int Iterations = (int) arg;

            for (int i = 0; i < Iterations; i++)
            {
                Console.WriteLine("[{0}] Hello world {1}!  ({2})", Thread.CurrentThread.ManagedThreadId,i, Thread.CurrentThread.IsBackground);
            }
        }
    }

    class ThreadPoolDemo
    {
        //Thread pool threads always run as background threads.
        static void Main()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);

            // Order of execution of threads in pool is not guaranteed.
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(SayHello, i);
            }

            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
        }

        static void SayHello(object stateArg)
        {
            int n = (int)stateArg;
            Console.WriteLine("[{0}] Hello world {1}!  ({2})", Thread.CurrentThread.ManagedThreadId,stateArg, Thread.CurrentThread.IsBackground);
        }
    }

    class DelegateBeginInvoke
    {
        delegate void BinaryOperation(int x, int y);

        static void Main()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            BinaryOperation asyncAdd = Add;
            //Add method run by threadpool thread;
            //Third parameter here is the call back method
            asyncAdd.BeginInvoke(2, 2,null, null);
            Thread.Sleep((1000));
        }

        static void Add(int a, int b)
        {
            //Console.WriteLine("{0}", Thread.CurrentThread.IsBackground);
            Console.WriteLine("[{0}] Add ({1}, {2}  => {3})", Thread.CurrentThread.ManagedThreadId,a,b,(a+b));
        }

    }

    class DelegateEndInvoke
    {
        delegate int BinaryOperation(int x, int y);
        
        
        static void Main()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            BinaryOperation asyncAdd = Add;
            //Add method run by threadpool thread;
            IAsyncResult asyncResult= asyncAdd.BeginInvoke(2, 2, new AsyncCallback(callBack), null);
            //Blocks the main thread till the add operation invoked by Begin Invoke finishes.
            int res = asyncAdd.EndInvoke(asyncResult);
            Console.WriteLine("BeginInvoke finished with result {0}", res);
            //Thread.Sleep((1000));
        }

        static int Add(int a, int b)
        {
            Thread.Sleep(2000);
            Console.WriteLine("[{0}] BeginInvoke Add ({1}, {2}  => {3})", Thread.CurrentThread.ManagedThreadId, a, b, (a + b));
            return (a + b);
        }

        //call backs also run in the same thread in which begin invoke runs on.
        static void callBack(IAsyncResult result)
        {
            string res =  result.ToString();
            Console.WriteLine("[{0}] Call back method res ={1}",Thread.CurrentThread.ManagedThreadId,res);
        }
       
    }

    //This is how actual BeginInvoke and EndInvoke works
    class DelegateEndInvokeAsyncState
    {
        delegate int AsyncMethodCaller(int x, int y);


        static void Main()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            AsyncMethodCaller asyncAdd = Add;
            //Add method run by threadpool thread;
            IAsyncResult asyncResult = asyncAdd.BeginInvoke(2, 2, new AsyncCallback(callBack), "Information to be passed to callback method, This can be any custom object");
            
            while (!asyncResult.IsCompleted)
            {
                Thread.Sleep((1000));
            }
            
        }

        static int Add(int a, int b)
        {
            Thread.Sleep(2000);
            Console.WriteLine("[{0}] BeginInvoke Add ({1}, {2}  => {3})", Thread.CurrentThread.ManagedThreadId, a, b, (a + b));
            return (a + b);
        }

        //call backs also run in the same thread in which begin invoke runs on.
        static void callBack(IAsyncResult iAsynResult)
        {
            string infoPassed = (string)iAsynResult.AsyncState;
            AsyncResult AsResult = (AsyncResult)iAsynResult;
            AsyncMethodCaller op = (AsyncMethodCaller)AsResult.AsyncDelegate;
            int res = op.EndInvoke(iAsynResult);
            Console.WriteLine("Infor passed from BegingInvoke: " + infoPassed);
            
            Console.WriteLine("[{0}] Call back method res ={1}", Thread.CurrentThread.ManagedThreadId, res);
        }

    }

    class DelegateEndInvokeAsyncStateWaitHandle
    {
        delegate int AsyncMethodCaller(int x, int y);


        static void Main()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            AsyncMethodCaller asyncAdd = Add;
            //Add method run by threadpool thread;
            IAsyncResult asyncResult = asyncAdd.BeginInvoke(2, 2, new AsyncCallback(callBack), "Information to be passed to callback method, This can be any custom object");

            asyncResult.AsyncWaitHandle.WaitOne();

            Console.WriteLine("Wait Handle Signaled");
            Thread.Sleep(0);
            asyncResult.AsyncWaitHandle.Close();

        }

        static int Add(int a, int b)
        {
            Thread.Sleep(2000);
            Console.WriteLine("[{0}] BeginInvoke Add ({1}, {2}  => {3})", Thread.CurrentThread.ManagedThreadId, a, b, (a + b));
            return (a + b);
        }

        //call backs also run in the same thread in which begin invoke runs on.
        static void callBack(IAsyncResult iAsynResult)
        {
            string infoPassed = (string)iAsynResult.AsyncState;

            AsyncResult AsResult = (AsyncResult)iAsynResult;

            AsyncMethodCaller op = (AsyncMethodCaller)AsResult.AsyncDelegate;
            int res = op.EndInvoke(iAsynResult);
            Console.WriteLine("Infor passed from BegingInvoke: " + infoPassed);

            Console.WriteLine("[{0}] Call back method res ={1}", Thread.CurrentThread.ManagedThreadId, res);
        }

    }

    //Critical Sections
    class CriticalSection
    {
        private static int sum = 0;

        static void Main()
        {
            Thread[] threads =new Thread[100];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i]=new Thread(AddOne);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            //Sum is not always the same.
            Console.WriteLine(sum);
        }

        static void AddOne()
        {
            Console.WriteLine("[{0}] Thread AddOne is called" , Thread.CurrentThread.ManagedThreadId);
            sum++;
        }
    }

    class AtomicUpdate
    {
        private static int sum = 0;

        static void Main()
        {
            Thread[] threads = new Thread[100];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(AddOne);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            //Sum is not always the same.
            Console.WriteLine(sum);
        }

        static void AddOne()
        {
            Console.WriteLine("[{0}] Thread AddOne is called", Thread.CurrentThread.ManagedThreadId);
           
            Interlocked.Increment(ref sum);
            // Above statement is same as 
            //int temp = sum;
            //temp++;
            //sum = temp;
        }
    }

    
    // Data Partitioning
    class ArrayProcessor
    {
        private double[] data;
        private int firstIndex;
        private int lastIndex;
        private double sum;

        public ArrayProcessor(double[] Data,int FirstIndex, int LastIndex)
        {
            data = Data;
            firstIndex = FirstIndex;
            lastIndex = LastIndex;
        }

        static void Main()
        {
            int coreCount = Environment.ProcessorCount;
            Console.WriteLine("Processor/Core count={0}", coreCount);

            double[] data = GetData();

            Stopwatch sw=Stopwatch.StartNew();

            ArrayProcessor wholeArray= new ArrayProcessor(data,0,data.Length-1);
            wholeArray.ComputeSum();
            sw.Stop();
            Console.WriteLine("1 thread computed {0:n0} in {1:n0} ms", wholeArray.sum,sw.ElapsedMilliseconds);
        }
        public void ComputeSum()
        {
            sum = 0;
            for (int i = firstIndex; i < lastIndex; i++)
            {
                sum += data[i];
                Thread.Sleep(1);
            }
        }

        public double Sum
        {
            get { return sum; }
        }

        static double[] GetData()
        {
            double[] data=new double[5000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            return data;
        }
    }

    //Data Partitioning
    class ArrayProcessorDataPartitioned
    {
        private double[] data;
        private int firstIndex;
        private int lastIndex;
        private double sum;

        public ArrayProcessorDataPartitioned(double[] Data, int FirstIndex, int LastIndex)
        {
            data = Data;
            firstIndex = FirstIndex;
            lastIndex = LastIndex;
        }

        static void Main()
        {
            int coreCount = Environment.ProcessorCount;
            Console.WriteLine("Processor/Core count={0}", coreCount);

            double[] data = GetData();

            Stopwatch sw = Stopwatch.StartNew();

            ArrayProcessorDataPartitioned[] slices=new ArrayProcessorDataPartitioned[coreCount];
            Thread[] threads=new Thread[coreCount];

            int indexPerThread = data.Length / coreCount;
            int leftOverIndexes = data.Length % coreCount;

            for (int i = 0; i < coreCount; i++)
            {
                int firstIndex = (i * indexPerThread);
                int lastIndex = firstIndex + indexPerThread - 1;
                if (i == (coreCount - 1))
                {
                    lastIndex += leftOverIndexes;
                }

                ArrayProcessorDataPartitioned slice = new ArrayProcessorDataPartitioned(data, firstIndex, lastIndex);
                slices[i] = slice;
                threads[i]=new Thread(slice.ComputeSum);
                threads[i].Start();
            }

            double res = 0;

            for (int i = 0; i < coreCount; i++)
            {
                threads[i].Join();
                res += slices[i].Sum;
            }
            sw.Stop();
            Console.WriteLine("1 thread computed {0:n0} in {1:n0} ms",res, sw.ElapsedMilliseconds);
        }
        public void ComputeSum()
        {
            sum = 0;
            for (int i = firstIndex; i < lastIndex; i++)
            {
                sum += data[i];
                Thread.Sleep(1);
            }
        }

        public double Sum
        {
            get { return sum; }
        }

        static double[] GetData()
        {
            double[] data = new double[5000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            return data;
        }
    }

    //Monitor Usage
    class Widget2D
    {
        private int x;
        private int y;
        object _lock=new object();

        public Widget2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        #region  criticalSection

        public void MoveBy(int deltaX, int deltaY)
        {
            lock (_lock)//this block is equivalent to below commented code
            {
                x += deltaX;
                y += deltaY;
            }
            //Monitor.Enter(_lock);
            //try
            //{
            //    x += deltaX;
            //    y += deltaY;
            //}
            //finally 
            //{
            //    Monitor.Exit(_lock);
            //}
           
           
        }

        public void GetPos(out int x, out int y)
        {
            lock (_lock)//this block is equivalent to below commented code
            {
                x = this.x;
                y = this.y;
            }
           
            //Monitor.Enter(_lock);
            //try
            //{
            //    x = this.x;
            //    y = this.y;
            //}
            //finally
            //{
            //    Monitor.Exit(_lock);
            //}
        }

        #endregion
        
    }

    //Monitor Usage
    public class Bakery
    {
        Queue<Donut> donutTray=new Queue<Donut>();

        public Donut GetDonut()
        {
            lock (donutTray)
            {
                if (donutTray.Count == 0)
                {
                    //Lets the calling thread to sleep and releases the lock.
                    Monitor.Wait(donutTray);
                }
                return donutTray.Dequeue();
            }
        }

        public void RefillTray(Donut[] freshDonuts)
        {
            lock (donutTray)
            {
                foreach (Donut d in freshDonuts)
                {
                    donutTray.Enqueue(d);
                }
                //pulses all the threads which are sleeping due to Monitor.Wait
                Monitor.PulseAll(donutTray);
            }

        }
       public class Donut
        {
           //not requred for above example.
        }
    }

    //DeadLocks
    class Bank
    {
        static BankAccount[] s_bankAccounts=new BankAccount[SimulationParameters.NUMBER_OF_ACCOUNTS];
        private static volatile bool s_simulationOver = false;

        static void Main()
        {
            Thread.CurrentThread.Name = "Main";
            Console.WriteLine("[Main] Starting program. Total funds on deposit would always be");

            for (int n = 0; n < SimulationParameters.NUMBER_OF_ACCOUNTS; n++)
            {
                s_bankAccounts[n]=new BankAccount(n,SimulationParameters.INITIAL_DEPOSIT);
            }

            Thread[] transferThreads=new Thread[SimulationParameters.NUMBER_OF_TRANSFER_THREADS];
            System.Threading.ThreadStart threadProc = new System.Threading.ThreadStart(TransferThreadProc);

            for (int n = 0; n < SimulationParameters.NUMBER_OF_TRANSFER_THREADS; n++)
            {
                transferThreads[n]=new Thread(threadProc);
                transferThreads[n].Name = string.Format("X-{0}", n);
                transferThreads[n].Start();
            }
            Thread.Sleep((SimulationParameters.SIMULATION_LENGTH));
            Console.WriteLine("[Main] shutting down simulation");
            s_simulationOver = true;

            for (int n = 0; n < transferThreads.Length; n++)
            {
                transferThreads[n].Join();
            }

            Console.WriteLine("[Main] Simulation comlete, verifying accounts.");

            VerifyAccounts();
        }

        private static void VerifyAccounts()
        {
            string threadName = Thread.CurrentThread.Name;
            double totalDepositsIfNoErrors = SimulationParameters.INITIAL_DEPOSIT *
                                             SimulationParameters.NUMBER_OF_ACCOUNTS;
            double totalDeposits = 0;

            for (int n = 0; n < SimulationParameters.NUMBER_OF_ACCOUNTS; n++)
            {
                totalDeposits += s_bankAccounts[n].Balance;
            }

            if (totalDeposits == totalDepositsIfNoErrors)
            {
                Console.WriteLine("[{0}] Audit result: bank accounts are consistent ({1:C0} on deposit)",threadName,totalDeposits);
            }
            else
            {
                Console.WriteLine("[{0}] Audit result: ***inconsistencies detected ({1:C0} on deposit)", threadName, totalDeposits);
            }
            
        }

        static void TransferThreadProc()
        {
            string threadName = Thread.CurrentThread.Name;
            while (!s_simulationOver)
            {
                double transferAmount = GetRandomTransferAmount();

                int debitAccount = GetRandomAccountIndex();
                int creditAccount = GetRandomAccountIndex();

                while (creditAccount == debitAccount)
                {
                    creditAccount = GetRandomAccountIndex();
                }

                s_bankAccounts[creditAccount].TransferFrom((s_bankAccounts[debitAccount]), transferAmount);
            }
            Thread.Sleep((SimulationParameters.TRANSFER_THREAD_PERIOD));
        }

        private static int GetRandomAccountIndex()
        {
            throw new NotImplementedException();
        }

        private static double GetRandomTransferAmount()
        {
            throw new NotImplementedException();
        }
    }
    class SimulationParameters
    {
        public const double INITIAL_DEPOSIT = 1000;
        public const double MIN_TRANSFER_AMOUNT = 25;
        public const double MAX_TRANSFER_AMOUNT = 250;
        public  const int NUMBER_OF_ACCOUNTS=10;
        public const int NUMBER_OF_TRANSFER_THREADS = 4;
        public const int TRANSFER_THREAD_PERIOD = 200;
        public const int SIMULATION_LENGTH = 10000;
    }
    public class BankAccount
    {
        public readonly int AccountNumber;
        private double balance;

        public BankAccount(int acctNum, double initDeposit)
        {
            AccountNumber = acctNum;
            balance = initDeposit;
        }

        public void Credit(double amt)
        {
            double temp = balance;
            temp += amt;
            Thread.Sleep(1);
            balance = temp;
        }

        public void Debit(double amt)
        {
            Credit(-amt);
        }

        public double Balance { get { return balance; } }
       
        public  void TransferFrom(BankAccount otherAccount, double amt)
        {
            Console.WriteLine("[{0}] Transferring {1:C0} from account {2} to {3}", Thread.CurrentThread.Name,amt,otherAccount.AccountNumber, this.AccountNumber);
            otherAccount.Debit(amt);
            this.Credit(amt);
        }
    }

    //https://msdn.microsoft.com/library/system.threading.monitor.aspx
    #region Monitor
    
    //This generates Synchronization Lock Exception
    class MonitorDemoSynchronizationLockException
    {

        static void Main()
        {
            int nTasks = 0;
            List<Task> tasks = new List<Task>();

            try
            {
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(250);
                        Monitor.Enter(nTasks);
                        Console.WriteLine(nTasks.GetType().ToString());
                        try { nTasks += 1; }
                        finally { Monitor.Exit(nTasks); }
                    }));
                    Task.WaitAll(tasks.ToArray());
                    Console.WriteLine("{0} Tasks started and executed.", nTasks);
                }
            }
            catch (AggregateException e)
            {
                string msg = String.Empty;
                foreach (var ie in e.InnerExceptions)
                {
                    Console.WriteLine("{0}", ie.GetType().Name);
                    if (!msg.Contains(ie.Message))
                        msg += ie.Message + Environment.NewLine;
                }
                Console.WriteLine("\nException Message(s):");
                Console.WriteLine(msg);
            }
        }
    }

    class MonitorDemoBoxingBeforeCall
    {

        static void Main()
        {
            int nTasks = 0;
            object o = nTasks;
            List<Task> tasks = new List<Task>();

            try
            {
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(250);
                        Monitor.Enter(o);
                        try { nTasks += 1; }
                        finally { Monitor.Exit(o); }
                    }));
                    Task.WaitAll(tasks.ToArray());
                    Console.WriteLine("{0} Tasks started and executed.", nTasks);
                }
            }
            catch (AggregateException e)
            {
                string msg = String.Empty;
                foreach (var ie in e.InnerExceptions)
                {
                    Console.WriteLine("{0}", ie.GetType().Name);
                    if (!msg.Contains(ie.Message))
                        msg += ie.Message + Environment.NewLine;
                }
                Console.WriteLine("\nException Message(s):");
                Console.WriteLine(msg);
            }
        }
    }

    class MonitorInterLockedAdd
    {
        static void Main()
        {
            List<Task> tasks = new List<Task>();
            Random rnd = new Random();
            long total = 0;
            int n = 0;

            for (int taskCtr = 0; taskCtr < 10; taskCtr++)
            {
                tasks.Add(Task.Run(() =>
                {
                    int[] values = new int[10000];
                    int taskTotal = 0;
                    int taskN = 0;
                    int ctr = 0;

                    Monitor.Enter(rnd);
                    for (ctr = 0; ctr < 10000; ctr++)
                        values[ctr] = rnd.Next(0, 1001);
                    Monitor.Exit(rnd);
                    taskN = ctr;
                    foreach (var value in values)
                        taskTotal += value;
                    Console.WriteLine("Mean for task {0,2}: {1:N2} (N={2:N0})", Task.CurrentId, (taskTotal * 1.0) / taskN, taskN);
                    Interlocked.Add(ref n, taskN);
                    Interlocked.Add(ref total, taskTotal);

                }));
                try
                {
                    Task.WaitAll(tasks.ToArray());
                    Console.WriteLine("\nMean for all tasks:{0:N2} (N={1:N0})", total * 1.0 / n, n);
                }
                catch (AggregateException e)
                {
                    foreach (var ie in e.InnerExceptions)
                    {
                        Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);

                    }
                }
            }
        }
    }

    #region Combined Use  of Monitor class, Interocked class, AutoResetEvent calss

    internal class SyncResource
    {
        public void Access()
        {
            lock (this)
            {
                Console.WriteLine("Starting synchronized resource access on thread #{0}", Thread.CurrentThread.ManagedThreadId);
                if (Thread.CurrentThread.ManagedThreadId % 2 == 0)
                    Thread.Sleep(2000);
                Thread.Sleep(200);
                Console.WriteLine("Stopping synchronized resource access in thread #{0}", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }

    internal class UnSyncedResource
    {
        public void Access()
        {
            Console.WriteLine("Starting unsynchronized resource access on thread #{0}", Thread.CurrentThread.ManagedThreadId);
            if (Thread.CurrentThread.ManagedThreadId % 2 == 0)
                Thread.Sleep(2000);
            Thread.Sleep(200);
            Console.WriteLine("Stopping unsynchronized resource access in thread #{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }

    class MonitorTest
    {
        private static int numOps;
        private static AutoResetEvent opsAreDone = new AutoResetEvent(false);
        private static SyncResource SyncRes = new SyncResource();
        private static UnSyncedResource unSyncRes = new UnSyncedResource();

        public static void Main()
        {
            numOps = 5;
            for (int ctr = 0; ctr <= 4; ctr++)
                ThreadPool.QueueUserWorkItem(new WaitCallback(SyncUpdateResource));
            opsAreDone.WaitOne();

            numOps = 5;
            for (int ctr = 0; ctr <= 4; ctr++)
                ThreadPool.QueueUserWorkItem(new WaitCallback(UnSyncUpdateResource));
            opsAreDone.WaitOne();
        }

        static void SyncUpdateResource(object state)
        {
            SyncRes.Access();

            if (Interlocked.Decrement(ref numOps) == 0)
                opsAreDone.Set();
        }

        static void UnSyncUpdateResource(object state)
        {
            unSyncRes.Access();

            if (Interlocked.Decrement(ref numOps) == 0)
                opsAreDone.Set();
        }
    }
    #endregion

    //TO DO
    //Monitor.wait()
    //Monitor.Pulse()
    //Monitor.PulseAll();


    #endregion

    #region Mutex

    

    #endregion

    #region Spin Lock


    #endregion

    #region semaphore

    

    #endregion


}

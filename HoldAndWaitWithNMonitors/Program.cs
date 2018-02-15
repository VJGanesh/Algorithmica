using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoldAndWaitWithNMonitors
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Bakery
    {
        Queue<Donut> _donutTray=new Queue<Donut>();

        public Donut GetDonut()
        {
            lock (_donutTray)
            {
                while (_donutTray.Count==0)
                {
                    Monitor.Wait(_donutTray);//Releases the lock and let other to acquire lock
                }
               return _donutTray.Dequeue();
            }
            return new Donut();
        }

        public void RefillTray(Donut[] freshDonuts)
        {
            lock (_donutTray)
            {
                foreach (Donut d in freshDonuts)
                {
                    _donutTray.Enqueue(d);
                }
                Monitor.PulseAll(_donutTray);
            }
        }

    }

    public class Donut
    {
        
    }
}

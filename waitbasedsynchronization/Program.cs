using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace waitbasedsynchronization
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Widget2D
    {
        private int _x;
        private int _y;
        object _lock=new object(); 
        public Widget2D(int x, int y)
        {
            _x = x;
            _y = y;
        }


        public void MoveBy(int deltaX, int deltaY)
        {
            #region Critical section
           // Monitor.Enter(_lock);
           // try
           // {
           //     _x += deltaX;
           //     _y += deltaY;
           // }
           //finally

           // { Monitor.Exit(_lock); } 

            lock (_lock)
            {
                _x += deltaX;
                _y += deltaY;
            }
            #endregion
           
        }

        public void GetPos(out int x, out int y)
        {
            #region CriticalSection
            Monitor.Enter(_lock);
            x = _x;
            y = _y;
            Monitor.Exit(_lock);
           #endregion
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Doer doer=new Doer();
            doer.Attach(new Logger());
            doer.Attach(new UserInterface());
            doer.DosemethingWith("Vijay");
        }
    }
}

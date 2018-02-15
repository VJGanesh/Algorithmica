using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Doer:ISubject
    {
        private string _data;
        public IList<IObserver> Observers=new List<IObserver>();
        public void DosemethingWith(string data)
        {
            _data = data;
            Console.WriteLine("Doing something with {0}.", _data);
            this.Notify(this);
        }

        public void Notify(ISubject sender)
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }


        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }


        public string Data
        {
            get { return _data; }
        }
    }
}

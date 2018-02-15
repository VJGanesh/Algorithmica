using System;

namespace ObserverPattern
{
    public class UserInterface:IObserver
    {

       

        public void Update(ISubject subject)
        {
            Console.WriteLine("Hey user, Look at this: " + subject.Data);
        }
    }
}
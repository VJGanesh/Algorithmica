using System;

namespace ObserverPattern
{
    public class Logger:IObserver
    {

        public void Update(ISubject subject)
        {
            Console.WriteLine("Message Logged: " + subject.Data);
        }
    }
}
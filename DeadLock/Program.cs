﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeadLock
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class BankAccount
    {
        public readonly int AccountNumber;
        private double _balance;
        object _lock =new object();

        public BankAccount(int acctNum, double initDeposit)
        {
            AccountNumber = acctNum;
            _balance = initDeposit;
        }

        public void Credit(double amt)
        {
            lock (_lock)
            {
                double temp = _balance;
                temp += amt;
                Thread.Sleep(1);
                _balance = temp;
            }
            
        }

        public void Debit(double amt)
        {
            Credit(-amt);
        }

        public double Balance
        {
            get
            {
                double b = 0;

                lock (_lock)
                {
                    b = _balance;
                }
                return b;
            }
        }

        public void TransferForm(BankAccount otherAcct, double amt)
        {
            Console.WriteLine("[{0}] Transferring {1:C0} from account {2} to {3}",Thread.CurrentThread.Name,amt,otherAcct.AccountNumber,this.AccountNumber);
            lock (this._lock)
            {
                Thread.Sleep(20);
                
                lock (otherAcct._lock)
                {
                    otherAcct.Debit(amt);
                    this.Credit(amt);
                }
            }
           
        }
    }

   
}



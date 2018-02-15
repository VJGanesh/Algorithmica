using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Model;
using MVVMApp;

namespace App.ViewModel
{
    class UserViewModel
    {

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        private ObservableCollection<User> _UsersList;

        public UserViewModel()
        {
           _UsersList = new ObservableCollection<User>
            {
                new User{UserId = 1,FirstName="Raj",LastName="Beniwal",City="Delhi",State="DEL",Country="INDIA"},
                new User{UserId=2,FirstName="Mark",LastName="henry",City="New York", State="NY", Country="USA"},
                new User{UserId=3,FirstName="Mahesh",LastName="Chand",City="Philadelphia", State="PHL", Country="USA"},
                new User{UserId=4,FirstName="Vikash",LastName="Nanda",City="Noida", State="UP", Country="INDIA"},
                new User{UserId=5,FirstName="Harsh",LastName="Kumar",City="Ghaziabad", State="UP", Country="INDIA"},
                new User{UserId=6,FirstName="Reetesh",LastName="Tomar",City="Mumbai", State="MP", Country="INDIA"},
                new User{UserId=7,FirstName="Deven",LastName="Verma",City="Palwal", State="HP", Country="INDIA"},
                new User{UserId=8,FirstName="Ravi",LastName="Taneja",City="Delhi", State="DEL", Country="INDIA"}            
            };
        }

        private bool canExecute1(object obj)
        {
            return true;
        }

        private void execute1(object obj)
        {
            _UsersList.Add(new User { UserId = ID , FirstName = FirstName, LastName = LastName, City = City, State = State, Country = Country });
        }

        public ObservableCollection<User> Users
        {
            get { return _UsersList; }
            set { _UsersList = value; }
        }

        private ICommand mUpdater;

            public ICommand UpdateCommand
            {
                get { return mUpdater ?? (mUpdater = new DelegateCommand(execute1, canExecute1)); }
                set{ mUpdater = value; }
            }
        }

    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using App.ViewModel;

namespace MVVMApp.View
{
    /// <summary>
    /// Interaction logic for User2.xaml
    /// </summary>
    public partial class User2 : Window
    {
        public User2()
        {
            InitializeComponent();
            DataContext=new UserViewModel();
        }
    }
}

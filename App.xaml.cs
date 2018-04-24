using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DateTime nu = DateTime.Now;
            Model.Parkeerbon deBon = new Model.Parkeerbon(nu, nu, nu, 0);
            ViewModel.ParkeerbonMetValuesVM vm = new ViewModel.ParkeerbonMetValuesVM(deBon);
            View.ParkingBonView mijnParkingBonView = new View.ParkingBonView();

            mijnParkingBonView.Closing += vm.OnWindowClosing;
            mijnParkingBonView.DataContext = vm;
            mijnParkingBonView.Show();
        }
    }
}

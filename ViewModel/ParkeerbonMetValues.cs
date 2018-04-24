using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace ParkingBonMVVM.ViewModel
{
    public class ParkeerbonMetValuesVM : ViewModelBase
    {
        private Model.Parkeerbon ingevuldeParkeerbon;
        public ParkeerbonMetValuesVM (Model.Parkeerbon deBon)
        {
            ingevuldeParkeerbon = deBon;
        }

        public DateTime Datum
        {
            get { return ingevuldeParkeerbon.Datum; }
            set { ingevuldeParkeerbon.Datum = value;
                RaisePropertyChanged("Datum"); }
        }
        public DateTime AankomstTijd
        {
            get { return ingevuldeParkeerbon.AankomstTijd; }
            set { ingevuldeParkeerbon.AankomstTijd = value;
                RaisePropertyChanged("AankomstTijd");
            }
        }
        public DateTime VertrekTijd
        {
            get { return ingevuldeParkeerbon.VertrekTijd; }
            set
            {
                ingevuldeParkeerbon.VertrekTijd = value;
                RaisePropertyChanged("VertrekTijd");
            }
        }
        public int Bedrag
        {
            get { return ingevuldeParkeerbon.Bedrag; }
            set
            {
                ingevuldeParkeerbon.Bedrag = value;
                RaisePropertyChanged("Bedrag");
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Afsluiten", "Wilt u het programma sluiten?", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
        public RelayCommand MeerCommand
        {
            get { return new RelayCommand(Meer); }
        }
        private void Meer()
        {
            int bedrag = ingevuldeParkeerbon.Bedrag;
            if (bedrag >= 0)
            {
                bedrag += 1;
                ingevuldeParkeerbon.Bedrag = bedrag;
                RaisePropertyChanged("Bedrag");
                DateTime date = ingevuldeParkeerbon.VertrekTijd;
                date = date.AddMinutes(30);
                ingevuldeParkeerbon.VertrekTijd = date;
                RaisePropertyChanged("VertrekTijd");
            }
        }
        public RelayCommand MinderCommand
        {
            get { return new RelayCommand(Minder); }
        }
        private void Minder()
        {
            int bedrag = ingevuldeParkeerbon.Bedrag;
            if (bedrag > 0)
            {
                bedrag -= 1;
                ingevuldeParkeerbon.Bedrag = bedrag;
                RaisePropertyChanged("Bedrag");
                DateTime date = ingevuldeParkeerbon.VertrekTijd;
                date = date.AddMinutes(-30);
                ingevuldeParkeerbon.VertrekTijd = date;
                RaisePropertyChanged("VertrekTijd");
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".bon";
            dlg.Filter = "Ribbon documents |*.bon";

            if (dlg.ShowDialog() == true)
            {
                using (StreamReader bestand = new StreamReader(dlg.FileName))
                {
                    ingevuldeParkeerbon.Datum = DateTime.Parse(bestand.ReadLine());
                    ingevuldeParkeerbon.AankomstTijd = DateTime.Parse(bestand.ReadLine());
                    ingevuldeParkeerbon.VertrekTijd = DateTime.Parse(bestand.ReadLine());
                    ingevuldeParkeerbon.Bedrag = Int32.Parse(bestand.ReadLine());
                }
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show("Uw bestand zou moeten worden opgeslaan","Opslaan", MessageBoxButton.OK);
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Ribbon documents |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(ingevuldeParkeerbon.Datum.ToShortDateString());
                        bestand.WriteLine(ingevuldeParkeerbon.AankomstTijd.ToShortDateString());
                        bestand.WriteLine(ingevuldeParkeerbon.VertrekTijd.ToShortTimeString());
                        bestand.WriteLine(ingevuldeParkeerbon.Bedrag.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }


        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DateTime nu = DateTime.Now;
            ingevuldeParkeerbon.Datum = nu;
            ingevuldeParkeerbon.AankomstTijd = nu;
            ingevuldeParkeerbon.VertrekTijd = nu;
            ingevuldeParkeerbon.Bedrag = 0;
            RaisePropertyChanged("Datum");
            RaisePropertyChanged("AankomstTijd");
            RaisePropertyChanged("VertrekTijd");
            RaisePropertyChanged("Bedrag");

        }
    }
}

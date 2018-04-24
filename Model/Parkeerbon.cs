using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBonMVVM.Model
{
    public class Parkeerbon
    {
        public DateTime Datum
        {
            get; set; 
        }
        public DateTime AankomstTijd
        {
            get; set;
        }
        public DateTime VertrekTijd
        {
            get;
            set;
        }
        public int Bedrag { get; set; }

        public Parkeerbon(DateTime datum, DateTime aankomst, DateTime vertrek, int bedrag)
        {
            Datum = datum;
            AankomstTijd = aankomst;
            VertrekTijd = vertrek;
            Bedrag = bedrag;
        }

    }
}

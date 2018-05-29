using MB_ERP_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Models
{
    public class Info
    { 
        public string Komunikat { get; set; }
        public bool Sukces { get; set; }
        public bool Rodzic { get; set; }        
        public decimal DoUkonczenia { get; set; }
        private decimal _IleUkonczono { get; set; }
        public decimal IleUkonczono
        {
            get { return _IleUkonczono; }
            set
            {
                _IleUkonczono = value;
            }
        }
    }
}

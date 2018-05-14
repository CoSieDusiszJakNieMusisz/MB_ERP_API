using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Models
{
    public class Towar
    {
        public int TowarId { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public decimal Ilosc { get; set; }
        public int DostawaId { get; set; }
        public decimal Cena { get; set; }
    }
}

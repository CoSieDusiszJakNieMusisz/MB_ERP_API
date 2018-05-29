using MB_ERP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Interfaces
{
    public interface IDekompletacja
    {
        List<Towar> ListaTowarowRW { get; set; }
        List<Towar> ListaTowarowPW { get; set; }
        IDokument RW { get; set; }
        IDokument PW { get; set; }

        int RozpocznijDekompletacje();
    }
}

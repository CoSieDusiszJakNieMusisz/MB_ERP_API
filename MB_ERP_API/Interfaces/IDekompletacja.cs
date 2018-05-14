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
        Dokument Dokument { get; set; }
        List<Towar> Towary { get; set; }

        void Dekompletuj();
    }
}

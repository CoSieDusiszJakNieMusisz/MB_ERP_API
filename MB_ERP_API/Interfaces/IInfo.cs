using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Interfaces
{
    public interface IInfo
    {
        string Komunikaty { get; set; }
        bool Sukces { get; set; }
    }
}

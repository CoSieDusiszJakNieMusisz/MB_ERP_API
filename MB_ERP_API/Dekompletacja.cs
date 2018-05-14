using MB_ERP_API.Interfaces;
using MB_ERP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API
{
    public class Dekompletacja : IDekompletacja
    {
        private Dokument _Dokument { get; set; }
        public Dokument Dokument
        {
            get
            {
                return _Dokument;
            }
            set
            {
                _Dokument = value;
            }
        }

        private List<Towar> _Towary { get; set; }
        public List<Towar> Towary
        {
            get
            {
                return _Towary;
            }
            set
            {
                _Towary = value;
            }
        }

        public void Dekompletuj()
        {
            throw new NotImplementedException();
        }
    }
}

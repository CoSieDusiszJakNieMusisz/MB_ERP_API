using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Models
{
    public class Dokument
    {
        public int DokumentId { get; set; }
        public int DokumentTyp { get; set; }
        public string Kontrahent { get; set; }
        public string SeriaDokumentu { get; set; }
        public string MagazynZrodlowy { get; set; }
        public string Opis { get; set; }
    }
}

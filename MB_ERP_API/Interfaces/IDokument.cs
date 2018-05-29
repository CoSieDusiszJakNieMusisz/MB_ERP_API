using MB_ERP_API.Interfaces;
using MB_ERP_API.Models;
using MB_ERP_API.ObserverInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Interfaces
{
    public interface IDokument : ISubject
    {
        /// <summary>
        /// Sesja użytkownika CDN ERP XL
        /// </summary>
        int Sesja { get; set; }
        /// <summary>
        /// ID dokumentu - służy do otwierania, zamykania dokumentów.
        /// </summary>
        int DokumentId { get; set; }        
        /// <summary>
        /// GIDNumer dokumentu
        /// </summary>
        int DokumentNumer { get; set; }
        /// <summary>
        /// Typ dokumentu: (Dokumenty handlowe: RW - 1616, PW - 1617
        /// </summary>
        int DokumentTyp { get; set; } 
        /// <summary>
        /// Kontrahent do którego będzie przypisany dokument
        /// </summary>        
        string Kontrahent { get; set; }
        /// <summary>
        /// Seria dokumentu
        /// </summary>
        string SeriaDokumentu { get; set; }
        /// <summary>
        /// Magazyn źródłowy dokumentu
        /// </summary>
        string MagazynZrodlowy { get; set; }
        /// <summary>
        /// Opis w nagłówku dokumentu
        /// </summary>
        string Opis { get; set; }
        /// <summary>
        /// Wartość dokumentu
        /// </summary>
        decimal WartoscDokumentu { get; set; }
        /// <summary>
        /// Lista produktów które mają trafić na dokument.
        /// </summary>
        IList<Towar> ListaProduktow { get; set; }
        /// <summary>
        /// Stan dokumentu: -1 - Dokument zamknięty, 0 - Dokument otwarty
        /// </summary>
        int StanDokumentu { get; set; }

        int NowyDokument();
        decimal DodajPozycjeDoDokumentu();
        int ZamknijDokument(int tryb);
        IList<int> GetReturns();
        int OtworzDokument();
        int AnulujDokument();
    }
}

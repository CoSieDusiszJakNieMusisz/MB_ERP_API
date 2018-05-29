using cdn_api;
using MB_ERP_API.Dokumenty;
using MB_ERP_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public decimal DodajDoDokumentu(IDokument dokument)
        {
            AddTool add = new AddTool(this, dokument);            
            return add.IloscTowaru;
        }
    }

    class AddTool
    {
        private decimal _IloscTowaru { get; set; }
        public decimal IloscTowaru
        {
            get { return _IloscTowaru; }
            set
            {
                _IloscTowaru = value;
            }
        }
        private readonly Towar _Towar;
        private Info info = new Info();
        private IDokument _Dokument;
        private DokumentHandlowy _DokumentHandlowy { get; set; }
        private DokumentHandlowy DokumentHandlowy
        {
            get { return _DokumentHandlowy; }
            set
            {
                _DokumentHandlowy = value;
                IloscTowaru = DodajDoDokumentuHandlowego();
            }
        }

        public AddTool(Towar towar, IDokument dokument)
        {
            _Towar = towar;
            _Dokument = dokument;
            SprawdzDokument();
        }

        private void SprawdzDokument()
        {
            if (_Dokument is DokumentHandlowy)
                DokumentHandlowy = _Dokument as DokumentHandlowy;
        }

        private decimal DodajDoDokumentuHandlowego()
        {
            decimal iloscProduktu = 0;           
            int wynik;
            XLDokumentElemInfo_20171 dodajPozycje = new XLDokumentElemInfo_20171()
            {
                Wersja = Const.Wersja,
                TowarKod = _Towar.Kod,
                Ilosc = _Towar.Ilosc.ToString().Replace(".", ","),
                DstNumer = _Towar.DostawaId                
            };
            if (DokumentHandlowy.DokumentTyp != 1616)
                dodajPozycje.Cena = _Towar.Cena.ToString().Replace(".", ",");
            wynik =cdn_api.cdn_api.XLDodajPozycje(DokumentHandlowy.DokumentId, dodajPozycje);
            DokumentHandlowy.WartoscDokumentu += ParsujWartoscProduktu(dodajPozycje.Wartosc);
            iloscProduktu = _Towar.Ilosc - ParsujWartoscProduktu(dodajPozycje.Ilosc);

            if (wynik == 0)
            {
                info.Komunikat = "Do dokumentu został dodany towar: " + dodajPozycje.TowarKod + " - " + dodajPozycje.TowarNazwa +
                     "Ilość: " + iloscProduktu + ".";
                info.Sukces = true;
                info.Rodzic = false;
                DokumentHandlowy.NotifyObservers(info);
            }
            else
            {
                info.Komunikat = "Podczas wywołania funkcji XLDodajPozycje wystąpił błąd. Nr błędu: " + wynik.ToString() + ". " +
                    "Kod produktu: " + dodajPozycje.TowarKod + ". Wszystkie wprowadzone zmiany zostaną cofnięte.";
                info.Sukces = false;
                info.Rodzic = false;
                DokumentHandlowy.NotifyObservers(info);
            }
                
            return iloscProduktu;
        }

        private decimal ParsujWartoscProduktu(string cena)
        {
            decimal wartoscProduktu = 0;
            if (decimal.TryParse(cena.Replace(".",","), out wartoscProduktu))
                return wartoscProduktu;
            else
                return 0m; 
        }

        private void DodajDoDokumentuMagazynowego()
        {

        }

        private void DodajDoDokumentuImportowego()
        {

        }

        private void DodajDoDokumentuSAD()
        {

        }
    }
}

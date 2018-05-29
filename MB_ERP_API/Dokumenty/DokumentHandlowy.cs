using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cdn_api;
using MB_ERP_API.Interfaces;
using MB_ERP_API.Models;
using MB_ERP_API.ObserverInterfaces;

namespace MB_ERP_API.Dokumenty
{
    public class DokumentHandlowy : Dokument
    {  
        private List<int> wyniki = new List<int>();
        private Info info = new Info();
        private string NazwaDokumentu { get; set; }

        public override int NowyDokument()
        {
            int idDokumentu = 0;
            int wynik=-1;
            XLDokumentNagInfo_20171 nowyDokument = new XLDokumentNagInfo_20171()
            {
                Tryb = 2,
                Wersja = Const.Wersja,
                Typ = DokumentTyp,
                MagazynZ = MagazynZrodlowy,
                Seria = SeriaDokumentu,
                Akronim = Kontrahent,
                Opis = Opis
            };
            wynik=cdn_api.cdn_api.XLNowyDokument(Sesja, ref idDokumentu, nowyDokument);
            DokumentId = idDokumentu;
            DokumentNumer = nowyDokument.GIDNumer;
            DokumentTyp = nowyDokument.GIDTyp;
            NazwaDokumentu = Enum.GetName(typeof(DokumentyHandlowe), DokumentTyp);

            if (wynik == 0)
            {
                StanDokumentu = 0;
                info.Komunikat = NazwaDokumentu + ": Wystawiono nowy dokument.";
                info.Sukces = true;
                info.Rodzic = true;
                info.IleUkonczono += 1;
                NotifyObservers(info);
            }                                     
            else
            {
                info.Komunikat = NazwaDokumentu + ": Podczas wywołania funkcji XLNowyDokument wystąpił błąd. Nr błędu: " + wynik.ToString() + ". " +
                                 "Wszystkie wprowadzone zmiany zostaną cofnięte.";
                info.Sukces = false;
                info.Rodzic = true;
                NotifyObservers(info);

                ZamknijDokument(-1);
            }
            wyniki.Add(wynik);
           return wynik;
        }

        public override decimal DodajPozycjeDoDokumentu()
        {
            List<decimal> iloscSztukTowaru = new List<decimal>();
            
            foreach (Towar twr in ListaProduktow)
            {
                
                Towar towar = new Towar()
                {
                    Kod = twr.Kod,
                    Ilosc = twr.Ilosc,
                    DostawaId = twr.DostawaId,
                    Cena = twr.Cena
                };
                iloscSztukTowaru.Add(towar.DodajDoDokumentu(this));
                info.Komunikat = null;
                info.IleUkonczono += 1;
                NotifyObservers(info);
            }

            if (iloscSztukTowaru.Sum() != ListaProduktow.Sum(wartosc => wartosc.Ilosc))
            {
                info.Komunikat = NazwaDokumentu + ": Ilość na dokumencie różni się od ilości zadeklarowanej przez przez użytkownika. Dokument zostanie usunięty.";
                info.Sukces = false;
                info.Rodzic = false;
                NotifyObservers(info);
                ZamknijDokument(-1);
                wyniki.Add(1);
                return iloscSztukTowaru.Sum();
            }           
            return 0;
        }

        public override int ZamknijDokument(int tryb)
        {
            int wynik;
            XLZamkniecieDokumentuInfo_20171 zamknijDok = new XLZamkniecieDokumentuInfo_20171()
            {
                Wersja = Const.Wersja,
                Tryb = tryb,
                Magazynowe = 3,
            };
            wynik = cdn_api.cdn_api.XLZamknijDokument(DokumentId, zamknijDok);

            if (tryb == -1)
            {
                info.Komunikat = NazwaDokumentu + ": Usuwanie dokumentu. Wszystkie wprowadzone zmiany zostały cofnięte.";
                info.Sukces = true;
                info.Rodzic = true;
                info.IleUkonczono += 0;
                StanDokumentu = -1;
                NotifyObservers(info);                
            }
            else if (wynik != 0)
            {
                info.Komunikat = NazwaDokumentu + ": Podczas wywołania funkcji XLZamknijDokument wystąpił błąd. Nr błędu: " + wynik.ToString() + ". " +
                    "Dokument nie został zamknięty. GIDNumer dokumentu: " + zamknijDok.GidNumer + ".";
                info.Sukces = false;
                info.Rodzic = true;
                NotifyObservers(info);
            }
            else if (wynik == 0 && tryb ==0 )
            {
                info.Komunikat = NazwaDokumentu + ": Dokument został zamknięty.";
                info.Sukces = true;                
                NotifyObservers(info);
                StanDokumentu = -1;
            }
            wyniki.Add(wynik);
            return wynik;
        }

        public override int AnulujDokument()
        {
            int wynik;
            XLZamkniecieDokumentuInfo_20171 zamknijDok = new XLZamkniecieDokumentuInfo_20171()
            {
                Wersja = Const.Wersja,
                Tryb = -2,
                Magazynowe = 3,
                GidNumer = DokumentNumer,
                GidTyp = DokumentTyp,
                GidFirma = 769770,
                GidLp = 0
            };           
            wynik = cdn_api.cdn_api.XLZamknijDokument(0, zamknijDok);
            if (wynik != 0)
            {
                info.Komunikat = NazwaDokumentu + ": Podczas wywołania funkcji XLZamknijDokument wystąpił błąd. Nr błędu: " + wynik.ToString() + ". " +
                    "Dokument nie został anulowany. GIDNumer dokumentu: " + zamknijDok.GidNumer + ".";
                info.Sukces = false;
                info.Rodzic = true;
                NotifyObservers(info);
            }  
            else
            {
                info.Komunikat = NazwaDokumentu + ": Anulowanie dokumentu. Wszystkie wprowadzone zmiany zostaną cofnięte.";
                info.Sukces = true;
                info.Rodzic = true;
                NotifyObservers(info);
                StanDokumentu = -1;
            }

            wyniki.Add(wynik);
            return wynik;
        }

        public override IList<int> GetReturns()
        {            
            return wyniki;
        }

        public override int OtworzDokument()
        {
            throw new NotImplementedException();
        }
    }

    public enum DokumentyHandlowe
    {
        RW = 1616,
        PW = 1617
    }
}

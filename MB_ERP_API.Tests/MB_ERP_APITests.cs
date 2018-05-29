using MB_ERP_API.Interfaces;
using MB_ERP_API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Tests
{
    [TestFixture]
    
    public class MB_ERP_APITests
    {
        int Sesja { get; set; }
        Dokumenty.DokumentHandlowy dokumentTestowy;
        
        [Test]        
        public void AA_Logowanie_LogowanieUzytkownikaDoXL_ZwracaZero()
        {
            //var login = Login.Logowanie("DekompletacjaCDNXL", 0);
            //Sesja = login.Sesja;
            ////int wynik = nowyDokument.NowyDokument();
            //Assert.AreEqual(0, login.Wynik);
        }

        [Test]
        public void AB_NowyDokument_DodajemyNowyDokumentHandlowyDoXL_ZwracaZero()
        {
            var nowyDokument = new Dokumenty.DokumentHandlowy()
            {
                DokumentTyp = 1616,
                MagazynZrodlowy = "TOW",
                SeriaDokumentu = "KOM",
                Kontrahent = "PMBAMBINO199",
                Opis = "."
            };

            dokumentTestowy = nowyDokument;
            dokumentTestowy.Sesja = Sesja;
            int wynik = dokumentTestowy.NowyDokument();
            Assert.AreEqual(0, wynik);
        }

        [Test]
        public void AC_DodajPozycjeDoDokumentu_DodawaniePozycjiDoDokumentuHandlowego_ZwracaListeZWynikami()
        {
            var listaTowarow = new List<Towar>
            {
                new Towar() { TowarId = 74125, Kod= "ZEST5211", Ilosc = 1, Cena = 117.66m, DostawaId = 1121474 },
                new Towar() { TowarId = 74125, Kod= "ZEST5211", Ilosc = 1, Cena = 117.66m, DostawaId = 1127178 },
                new Towar() { TowarId = 74125, Kod= "ZEST5211", Ilosc = 5, Cena = 117.67m, DostawaId = 1108447 },
                new Towar() { TowarId = 74125, Kod= "ZEST5211", Ilosc = 1, Cena = 117.67m, DostawaId = 1120882 },
                new Towar() { TowarId = 74125, Kod= "ZEST5211", Ilosc = 1, Cena = 117.67m, DostawaId = 1140703 },
            };

            dokumentTestowy.ListaProduktow = listaTowarow;

            //IKomunikator view;

            //decimal wyniki = dokumentTestowy.DodajPozycjeDoDokumentu();
            Assert.AreEqual(0, 1);
        }

        [Test]
        public void AD_ZamknijDokument_ZamkniecieDokumentuHandlowy_ZwracaZero()
        {
            var wynik = dokumentTestowy.ZamknijDokument(0);
            Assert.AreEqual(0, wynik);
        }

        [Test]
        public void AE_OtworzDokument_OtwarcieDokumentuHandlowy_ZwracaZero()
        {
            var wynik = dokumentTestowy.OtworzDokument();
            Assert.AreEqual(0, wynik);
        }

        [Test]
        public void AF_AnulujDokument_AnulowanieDokumentuHandlowy_ZwracaZero()
        {
            var wynik = dokumentTestowy.AnulujDokument();
            Assert.AreEqual(0, wynik);
        }

        [Test]
        public void AG_Wylogowanie_WylogowanieUzytkownikaZXL_ZwracaZero()
        {
            //int wynik = Login.Wylogowanie(Sesja);
            //Assert.AreEqual(0, wynik);
        }
    }
}

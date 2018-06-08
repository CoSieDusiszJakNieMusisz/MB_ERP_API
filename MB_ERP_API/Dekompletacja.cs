//using MB_ERP_API.Handlowe;
using MB_ERP_API.Dokumenty;
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
        private List<Towar> _ListaTowarowRW { get; set; }
        public List<Towar> ListaTowarowRW
        {
            get { return _ListaTowarowRW; }
            set
            {
                _ListaTowarowRW = value;
            }
        }

        private List<Towar> _ListaTowarowPW { get; set; }
        public List<Towar> ListaTowarowPW
        {
            get { return _ListaTowarowPW; }
            set
            {
                _ListaTowarowPW = value;
            }
        }

        private IDokument _RW { get; set; }
        public IDokument RW
        {
            get { return _RW; }
            set
            {
                _RW = value;
            }
        }

        private IDokument _PW { get; set; }
        public IDokument PW
        {
            get { return _PW; }
            set
            {
                _PW = value;
            }
        }

        private decimal WartoscRW { get; set; }
        private decimal WartoscPW { get; set; }
        Info info = new Info();
        IList<int> ZwracaneWyniki;
        

        /// <summary>
        /// W konstruktorze Dekompletacji dostarczamy wszystkie informację potrzebne do dekomompletacji (Dokumenty rw/pw, widok z zaimplementowanym interfejsem IInfo. Aby wykonać dekompletację wywołujemy metodę RozpocznijDekompletacje())
        /// </summary>
        /// <param name="view">Widok z którego uruchamiana jest dekompletacja.</param>
        /// <param name="DokumentRW">Dokument RW typu DokumentHanldowy.</param>
        /// <param name="DokumentPW">Dokument PW typu DokumentHanldowy.</param>
        /// <param name="towaryRW">Lista towarów do dekompletacji</param>
        /// <param name="towaryPW">Lista składników dekompletowanych towarow</param>
        public Dekompletacja(IDokument DokumentRW, IDokument DokumentPW)
        {
            RW = DokumentRW;
            PW = DokumentPW;
            ListaTowarowRW = RW.ListaProduktow as List<Towar>;
            ListaTowarowPW = PW.ListaProduktow as List<Towar>;
        }

        public int RozpocznijDekompletacje()
        {
            NowyDokument(RW);
            WartoscRW = RW.WartoscDokumentu;
            ZwracaneWyniki = RW.GetReturns();
            List<int> _ZwracaneWyniki = new List<int>();
            _ZwracaneWyniki.AddRange(ZwracaneWyniki);

            if (SprawdzPoprawnoscDokumentu())
            {
                NowyDokument(PW);
                _ZwracaneWyniki.AddRange(PW.GetReturns());
                return _ZwracaneWyniki.Sum();
            }
            else
            {
                info.Sukces = false;
                info.Komunikat = "Różnica wartości dokumentów RW / PW jest zbyt duża.Wszystkie dokumenty dekompletacji zostaną cofnięte.";
                info.Rodzic = true;

                RW.NotifyObservers(info);
                AnulujDekompletacje();
                _ZwracaneWyniki.AddRange(ZwracaneWyniki);
                return _ZwracaneWyniki.Sum();
            }
        }

        private void AnulujDekompletacje()
        {
            if (PW.StanDokumentu != -1 && PW.DokumentNumer!=0)
            {
                PW.AnulujDokument();
                ZwracaneWyniki = PW.GetReturns();
                RW.AnulujDokument();
            }
            else if(RW.StanDokumentu == -1 && RW.DokumentNumer!=0)
            {
                RW.AnulujDokument();
            }             
        }

        private void NowyDokument(IDokument dokument)
        {
            info.DoUkonczenia = dokument.ListaProduktow.Count();
            dokument.NotifyObservers(info);
            dokument.NowyDokument();
            dokument.DodajPozycjeDoDokumentu();
            if(dokument.StanDokumentu!=-1)
            {
                dokument.ZamknijDokument(0);
            }
            info.DoUkonczenia = 0;
            dokument.NotifyObservers(info);
        }

        private bool SprawdzPoprawnoscDokumentu()
        {
            ZwracaneWyniki = RW.GetReturns();
            var wartoscPW = ListaTowarowPW.Sum(k => k.Cena * k.Ilosc);
            if (SprawdzMarginesBleduRWPW(WartoscRW, wartoscPW) <= 0.20m && ZwracaneWyniki.Sum() == 0)
                return true;
            else                
                return false;
        }

        private decimal SprawdzMarginesBleduRWPW(decimal wartoscRW, decimal wartoscPW)
        {
            decimal roznica = wartoscRW - wartoscPW;
            if (roznica < 0)
                roznica *= -1;
            return roznica;
        }
    }
}

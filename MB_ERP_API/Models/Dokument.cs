using MB_ERP_API.Interfaces;
using MB_ERP_API.ObserverInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.Models
{
    public abstract class Dokument : IDokument
    {
        List<IObserver> ObserversList;

        public Dokument()
        {
            ObserversList = new List<IObserver>();
        }

        private int _Sesja { get; set; }
        public int Sesja { get => _Sesja; set => _Sesja = value; }

        private int _DokumentId { get; set; }
        public int DokumentId { get => _DokumentId; set => _DokumentId = value; }

        private int _DokumentNumer { get; set; }
        public int DokumentNumer { get => _DokumentNumer; set => _DokumentNumer = value; }

        private int _DokumentTyp { get; set; }
        public int DokumentTyp { get => _DokumentTyp; set => _DokumentTyp=value; }

        private string _Kontrahent { get; set; }
        public string Kontrahent { get => _Kontrahent; set => _Kontrahent = value; }

        private string _SeriaDokumentu { get; set; }
        public string SeriaDokumentu { get => _SeriaDokumentu; set => _SeriaDokumentu = value; }

        private string _MagazynZrodlowy { get; set; }
        public string MagazynZrodlowy { get => _MagazynZrodlowy; set => _MagazynZrodlowy = value; }

        private string _Opis { get; set; }
        public string Opis { get => _Opis; set => _Opis = value; }

        private decimal _WartoscDokumentu { get; set; }
        public decimal WartoscDokumentu { get => _WartoscDokumentu; set => _WartoscDokumentu = value; }

        private IList<Towar> _ListaProduktow { get; set; }
        public IList<Towar> ListaProduktow { get => _ListaProduktow; set => _ListaProduktow = value; }

        private int _StanDokumentu { get; set; }
        public int StanDokumentu { get => _StanDokumentu; set => _StanDokumentu = value; }

        public void NotifyObservers<T>(T info)
        {
            if(ObserversList.Count!=0)
            {
                foreach(IObserver observer in ObserversList)
                {                    
                    observer.Update(info);
                }
            }
        }

        public void Register(IObserver observer)
        {
            ObserversList.Add(observer);
        }

        public void UnRegister(IObserver observer)
        {
            ObserversList.Remove(observer);
        }

        public abstract int NowyDokument();
        public abstract decimal DodajPozycjeDoDokumentu();
        public abstract int ZamknijDokument(int tryb);
        public abstract IList<int> GetReturns();
        public abstract int AnulujDokument();
        public abstract int OtworzDokument();
    }
}

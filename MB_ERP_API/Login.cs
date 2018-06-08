using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cdn_api;

namespace MB_ERP_API
{
    public class Login 
    {
        public int Wynik { get; set; }
        private int _Sesja = 0;
        public int Sesja
        {
            get { return _Sesja; }
        }
        private int UtworzSesje { get; set; }
        private string ProgramID { get; set; }
        
        [DllImport("ClaRUN.dll")]
        public static extern void AttachThreadToClarion(int flag);
        /// <summary>
        /// Metoda do logowania użytkownika
        /// </summary>
        /// <param name="ProgramId">Nazwa programu który korzysta z biblioteki</param>
        /// <param name="UtworzWlasnaSesje">Czy program ma tworzyć własną sesje ?
        /// ( 0 - program NIE tworzy własnej sesj )
        /// ( 1 - program tworzy własną sesję ) 
        /// Jeżeli 1, zostanie wywołane okno do logowania.</param>
        /// <returns>Metoda zwraca wynik w postaci int oraz numer sesji uzytkownika.</returns>      
        public Login(string ProgramId, int UtworzWlasnaSesje = 0)
        {
            UtworzSesje = UtworzWlasnaSesje;
            ProgramID = ProgramId;
        }

        public int Loguj()
        {
            XLLoginInfo_20171 login = new XLLoginInfo_20171()
            {
                Wersja = Const.Wersja,
                ProgramID = ProgramID,
                Baza = Const.Baza,
                UtworzWlasnaSesje = UtworzSesje
            };
            try
            {
                Wynik = cdn_api.cdn_api.XLLogin(login, ref _Sesja);
                return Sesja;
            }
            catch(Exception ex)
            {
                return Sesja;
            }
        }

        /// <summary>
        /// Metoda do wylogowania uzytkownika
        /// </summary>
        /// <param name="Sesja">Sesja uzytkownika.</param>
        /// <returns></returns>
        public int Wylogowanie(int Sesja)
        {
            int wynik;
            XLLogoutInfo_20171 logout = new XLLogoutInfo_20171() { Wersja = Const.Wersja };
            wynik = cdn_api.cdn_api.XLLogout(Sesja);
            return wynik;
        }
    }
}




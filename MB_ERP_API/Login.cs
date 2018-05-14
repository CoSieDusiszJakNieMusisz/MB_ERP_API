using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cdn_api;

namespace MB_ERP_API
{
    public struct Results
    {
        public int Sesja;
        public int Wynik;
    }

    public static class Login
    {
        /// <summary>
        /// Metoda do logowania użytkownika
        /// </summary>
        /// <param name="ProgramId">Nazwa programu który korzysta z biblioteki</param>
        /// <param name="UtworzWlasnaSesje">Czy program ma tworzyć własną sesje ?
        /// ( 0 - program NIE tworzy własnej sesj )
        /// ( 1 - program tworzy własną sesję ) 
        /// Jeżeli 1, zostanie wywołane okno do logowania.</param>
        /// <returns>Metoda zwraca wynik w postaci int oraz numer sesji uzytkownika.</returns>      
        public static Results Logowanie(string ProgramId, int UtworzWlasnaSesje = 0)
        {
            Results results = new Results();
            XLLoginInfo_20171 login = new XLLoginInfo_20171()
            {
                Wersja = Const.Wersja,
                ProgramID = ProgramId,
                Baza = Const.Baza,
                UtworzWlasnaSesje = UtworzWlasnaSesje
            };

            try
            {
                results.Wynik = cdn_api.cdn_api.XLLogin(login, ref results.Sesja);
            }
            catch (Exception ex)
            {

            }

            return results;
        }

        /// <summary>
        /// Metoda do wylogowania uzytkownika
        /// </summary>
        /// <param name="Sesja">Sesja uzytkownika.</param>
        /// <returns></returns>
        public static int Wylogowanie(int Sesja)
        {
            int wynik;
            XLLogoutInfo_20171 logout = new XLLogoutInfo_20171() { Wersja = Const.Wersja };
            wynik = cdn_api.cdn_api.XLLogout(Sesja);
            return wynik;
        }
    }
}




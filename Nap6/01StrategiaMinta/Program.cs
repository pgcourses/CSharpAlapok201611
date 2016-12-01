using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01StrategiaMinta
{
    class Program
    {
        static void Main(string[] args)
        {
            var adatOsztaly = new AdatOsztaly(adatok: new List<int>(new int[] { 1,3,6,7,9,11,20,3,6}));

            var osszeg = adatOsztaly.Osszeg();

            //Stratégia mintával ezt így tudjuk megvalósítani:
            adatOsztaly.MuveletMegadasa(new OsszegzoMuvelet1());
            var osszeg2 = adatOsztaly.MuveletElvegzese();


        }
    }

    /// <summary>
    /// Ez az interface definiálja, hogy milyen műveletet tud fogadni az
    /// adatokat kezelő osztály
    /// </summary>
    public interface IOsszegzoMuvelet
    {
        int Osszegzes(List<int> adatok);
    }

    public class OsszegzoMuvelet1 : IOsszegzoMuvelet
    {
        public int Osszegzes(List<int> adatok)
        {
            return adatok.Sum();
        }
    }

    public class OsszegzoMuvelet2 : IOsszegzoMuvelet
    {
        public int Osszegzes(List<int> adatok)
        {
            var osszeg = 0;
            foreach (var adat in adatok)
            {
                osszeg += adat;
            }
            return osszeg;
        }
    }

    public class AdatOsztaly
    {
        List<int> adatok;
        private IOsszegzoMuvelet muvelet;

        public void MuveletMegadasa(IOsszegzoMuvelet muvelet)
        {
            this.muvelet = muvelet;
        }

        public AdatOsztaly(List<int> adatok)
        {
            this.adatok = adatok;
        }

        public int MuveletElvegzese()
        {
            if (this.muvelet==null)
            {
                throw new ArgumentNullException("muvelet");
            }

            return this.muvelet.Osszegzes(adatok);
        }

        internal int Osszeg()
        {
            return adatok.Sum();
        }
    }

}

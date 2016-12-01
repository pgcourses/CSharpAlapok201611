using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04EsemenyekMegoldas
{
    class Program
    {
        static void Main(string[] args)
        {
            var bszmla = new Bankszamla();
            bszmla.MinuszbaMenne += EsemenyKezelo;

            bszmla.Jovairas(1500);
            bszmla.Jovairas(3000);
            bszmla.Jovairas(500);
            bszmla.Jovairas(3500);

            //PROBLÉMÁK (az eseményekkel ez kipipálva)
            //1. Így az osztálypéldányon kívülről átírható a híváslista
            //bszmla.ErtesitesiHivaslista = null;
            //2.
            //bszmla.ErtesitesiHivaslista();

            bszmla.Jovairas(-10000);

            //Ezt nem engedhetem
            //ezt private set-tel ki is védtünk
            //bszmla.Egyenleg = 1000000;

            Console.ReadLine();

        }

        private static void EsemenyKezelo(object sender, EsemenyDTO e)
        {
            e.MehetAJovairas = false;

            Console.WriteLine("Mínuszba menne a számla! Egyenleg előtte: {0}, Jóváírandó összeg: {1}, Egyenleg utána: {2}",
                e.EgyenlegElotte, e.JovairtOsszeg, e.EgyenlegUtana);
        }
    }

    class Bankszamla
    {

        //Események
        //Speciális delegatek, a következő megkötésekkel
        //1. csak void típusú metódust használhatunk: nem lehet visszatérési értéke
        //2. Nem lehet az osztályon kívülről hívni
        //3. nem lehet az osztályon kívül (= vel) értéket adni, így felülírni a híváslistát
        //public delegate void MinuszEgyenlegErtesitesiFvDef();
        //public event MinuszEgyenlegErtesitesiFvDef ErtesitesiHivaslista = null;

        //A delegate definíció kiváltására a Func<> és az Action<> mintájára 
        //előre bekészített delegate definíció szolgál:
        public event EventHandler<EsemenyDTO> MinuszbaMenne = null;

        private bool OnMinuszbaMenne(int osszeg, int egyenlegElotte, int egyenlegUtana)
        {
            var dto = new EsemenyDTO(osszeg, egyenlegElotte, Egyenleg + osszeg);
            //Ertesíteni kell akit érint
            var hvlista = MinuszbaMenne;
            if (hvlista != null)
            {
                hvlista(this, dto);
            }
            return dto.MehetAJovairas;
        }


        public int Egyenleg { get; private set; }

        //Erre azért nincs szükség, mert ezt a fordító elvégzi, mivel az int
        //alapértelmezett értéke a 0.
        //public Bankszamla()
        //{
        //    Egyenleg = 0;
        //}

        public void Jovairas(int osszeg)
        {
            var egyenlegElotte = Egyenleg;

            if (Egyenleg + osszeg < 0)
            {
                if (OnMinuszbaMenne(osszeg, egyenlegElotte, Egyenleg + osszeg))
                {
                    Egyenleg += osszeg;
                    Console.WriteLine("Osszeg: {0}, Új egyenleg: {1}", osszeg, Egyenleg);
                }
            }
            else
            { //todo: ez ne legyen duplikálva
                Egyenleg += osszeg;
                Console.WriteLine("Osszeg: {0}, Új egyenleg: {1}", osszeg, Egyenleg);
            }
        }
    }

    public class EsemenyDTO : EventArgs
    {

        public EsemenyDTO(int osszeg, int egyenlegElotte, int egyenlegUtana)
        {
            this.JovairtOsszeg = osszeg;
            this.EgyenlegElotte = egyenlegElotte;
            this.EgyenlegUtana = egyenlegUtana;
            this.MehetAJovairas = true;
        }

        public int JovairtOsszeg { get; set; }
        public int EgyenlegElotte { get; set; }
        public int EgyenlegUtana { get; set; }

        public bool MehetAJovairas { get; set; }
    }
}

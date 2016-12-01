using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Esemenyek
{
    class Program
    {
        static void Main(string[] args)
        {
            var bszmla = new Bankszamla();
            bszmla.ErtesitesiHivaslista += delegate { Console.WriteLine("Mínuszba ment a számla!"); };

            bszmla.Jovairas(1500);
            bszmla.Jovairas(3000);
            bszmla.Jovairas(500);
            bszmla.Jovairas(3500);

            //PROBLÉMÁK
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
    }

    class Bankszamla
    {
        //delegate-tel próbáljuk megoldani
        public delegate void MinuszEgyenlegErtesitesiFvDef();
        //A refelercia típusok alapértelmezett értéke a null, ezért ezt ha nem adom meg, a fordító megteszi.
        public MinuszEgyenlegErtesitesiFvDef ErtesitesiHivaslista = null;

        //Ezt a két sort ezzel az egy sorral is le tudjuk írni
        //public Action ErtesitesiHivaslista = null;

        public int Egyenleg { get; private set; }

        //Erre azért nincs szükség, mert ezt a fordító elvégzi, mivel az int
        //alapértelmezett értéke a 0.
        //public Bankszamla()
        //{
        //    Egyenleg = 0;
        //}

        public void Jovairas(int osszeg)
        {
            Egyenleg += osszeg;
            Console.WriteLine("Osszeg: {0}, Új egyenleg: {1}", osszeg, Egyenleg);

            if (Egyenleg<0)
            {
                //Ertesíteni kell akit érint
                var hvlista = ErtesitesiHivaslista;
                if (hvlista!=null)
                {
                    hvlista();
                }
            }
        }
    }
}

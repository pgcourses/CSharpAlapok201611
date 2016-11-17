using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04Sikidomok
{
    class Program
    {
        static void Main(string[] args)
        {
            var teglalap = new Teglalap(magassag: 3, szelesseg: 2);
            var haromszog = new Haromszog(alap: 10, magassag: 5);
            var kor = new Kor(sugar: 12);

            var lista = new List<ISikidom>();

            lista.Add(teglalap);
            lista.Add(haromszog);
            lista.Add(kor);

            var sum = 0;
            foreach (var sikidom in lista)
            {
                sum = sum + sikidom.Terulet();
            }

            Console.WriteLine("Területek összege: {0}", sum);

            Console.ReadLine();
        }

        class Teglalap : ISikidom
        {
            public Teglalap(int magassag, int szelesseg)
            {
                Magassag = magassag;
                Szelesseg = szelesseg;
            }

            public int Magassag { get; private set; }
            public int Szelesseg { get; private set; }

            public int Terulet()
            {
                return Magassag * Szelesseg;
            }
        }

        class Haromszog : ISikidom
        {
            public Haromszog(int alap, int magassag)
            {
                Alap = alap;
                Magassag = magassag;
            }

            public int Alap { get; private set; }
            public int Magassag { get; private set; }

            public int Terulet()
            {
                return Alap * Magassag;
            }
        }

        class Kor : ISikidom
        {
            public Kor(int sugar)
            {
                Sugar = sugar;
            }

            public int Sugar { get; private set; }

            public int Terulet()
            {
                return (int)(Sugar * Sugar * Math.PI);
            }
        }

        /// <summary>
        /// Közös felület, minden síkidomnak kell, hogy legyen egy terület függvénye
        /// </summary>
        interface ISikidom
        {
            int Terulet();
        }

        //Közös ősosztályból nem tudok kiindulni, mert nem tudom megmondani
        //hogy mennyi a területe
        //class Sikidom
        //{
        //    public int Terulet()
        //    {
        //        return 0;
        //    }
        //}

    }
}

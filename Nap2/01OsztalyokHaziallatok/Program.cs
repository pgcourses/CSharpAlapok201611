using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01OsztalyokHaziallatok
{
    class Program
    {
        //Objektum: Egyértelmű határokkal rendelkezik
        // 1. Azonosítható
        // 2. Van állapota
        // 3. Van viselkedése

        static void Main(string[] args)
        {

            //Azonosíthatóság példa: referencia alapján
            var haziallat = new Haziallat();
            var haziallat2 = new Haziallat();

            if (haziallat == haziallat2)
            { //ebben az esetben ugyanaz a referencia, ugyanaz a példány
                Console.WriteLine("Ugyanaz a kettő");
            }
            else
            { //ebben az esetben nem azonos a két példány
                Console.WriteLine("Különbözik a kettő");
            }

            //Ez megtöri az egységbezárást
            haziallat.LabakSzama = 3;

            //készítek egy private változót az osztályon belül
            //készítek egy public függvényt, ami ennek az értékével tér vissza
            //ezzel egységbe tudom zárni ezt az információt.
            haziallat.HanyLabaVanLekerdezes();

            var lepes = 5;
            var ennyitLepett = 6;
            haziallat.LepjenEnnyit(ref lepes, out ennyitLepett);
            Console.WriteLine("Lépés: {0}", lepes);

            //Ha van alapértelmezett paraméterérték, akkor nem kell megadnom, így is hívhatom
            haziallat.NevMegadasa();

            //A szignatúra alapján a fordító eldönti, hogy itt milyen hívás történik
            haziallat.NevMegadasa(5);

            Console.ReadLine();
        }

        class Haziallat
        {
            //2. Állapot kezelése 
            //nem adunk meg láthatósági módosítást, akkor az a private kulcsszó alapértelmezésben
            string ValamiSzoveg;
            //Ahhoz hogy lássuk osztályon kívülről, ahhoz public láthatósági módosító kulcsszó kell
            public string KivulrolIsLatszik;

            public int LabakSzama;

            //ehelyett csinálok egy saját változót
            int labakSzama;
            //majd készítek egy lekérdezőfüggvényt
            public int HanyLabaVanLekerdezes()
            {
                return labakSzama;
            }
            //ha kiegészítem egy beállítófüggvénnyel, akkor kiváltom az előbb létrehozott mezőt
            public void HanyLabaVanMegadas(int labak)
            {
                labakSzama = labak;
            }

            //Ugyanezt tudja nekünk implementálni a C# fordító:
            //ez egy ugyanilyen függvénypárt és private változót hoz létre.
            public int HanySzemeVan { get; private set; }

            //van olyan, hogy csak lekérdezni lehet
            public int HanyFuleVan { get; }

            //vagy csak beállítani lehet
            //A settert nem lehet automatikusan implementálni, ezért nekem kell 
            //kitöltenem a működést.
            private int hanyFogaVan;
            public int HanyFogaVan { set { hanyFogaVan = value; } }

            private int hanyFarkaVan;
            public int HanyFarkaVan
            {
                get
                {
                    return hanyFarkaVan;
                }
                set
                {
                    hanyFarkaVan = value;
                }
            }

            //3. Viselkedés definíció
            //Metódusokkal tudom a viselkedést definiálni
            public void LepjenEnnyit(ref int lepes, out int ennyitLeptem) //szignatúra pl: LepjenEnnyit(ref int, out int)
            {
                //A kimeneti paraméter befelé nem hoz magával semmit, így nem használható:
                //Console.WriteLine("ez jött paraméterként: {0}", ennyitLeptem);

                Console.WriteLine("Ennyit lépek: {0}", lepes);
                lepes = 4;
                ennyitLeptem = 5;
            }

            string Nev;
            public void NevMegadasa(string nev = "Bambi")
            {
                Nev = nev;
                //Ezt is írhatnám, ez az objektumon keresztül hivatkozás
                //this.Nev = nev;
            }

            public void NevMegadasa(int akarmi)
            {

            }

        }

    }
}

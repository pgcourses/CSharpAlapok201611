using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Delegate
{
    /// <summary>
    /// A Delegate használatának lépései
    /// 
    /// 1. Készítünk egy függvény típust (ez maga a delegate)
    /// 2. Készítünk egy híváslistát
    /// 3. Meghívjuk a híváslistán szereplő függvényeket
    /// </summary>


    class Program
    {
        //Ez leírja, hogy nincs visszaadott értéke, és egy stringet vár az a függvénytípus, amit a delegate meghatároz
        delegate void PeldaDelegate(string uzenet);

        delegate int FvDelegatePelda();

        static void Main(string[] args)
        {
            //ElsoDelegatePelda();

            //MasodikDelegatePelda();

            //HarmadikDelegatePelda();

            NegyedikDelegatePelda();


            Console.ReadLine();

        }

        /// <summary>
        /// Bemutatjuk, hogy hogyan kell biztosítani hogy a híváslistát
        /// csak akkor hívjuk meg, ha van rá feliratkozott metódus.
        /// Különben elszáll a hívás System.NullReferenceException kivétellel
        /// </summary>
        private static void NegyedikDelegatePelda()
        {
            PeldaDelegate hivaslista=null;

            //ez helyett:
            //if (hivaslista != null)
            //Lemásoljuk a híváslistát egy lokális változóba
            var lista = hivaslista;
            //Erre ellenőrzünk
            if (lista != null)
            { //majd ha lehet, akkor hívunk híváslistát
                lista("paraméter");
            }

            //ha valakinek VS2015 vagy azutáni környezete van, akkor c C# már ilyet is tud:
            //lista?.Invoke("paraméter");


            hivaslista("ez egy példa");
        }

        /// <summary>
        /// Ez a példa azt mutatja meg, hogy visszatérési értékkel rendelkező
        /// függvényeket is feliratkoztathatunk egy ilyen híváslistára. De itt csak az egyik
        /// visszatérési értékét kapjuk vissza, ami nem garantált, hogy listán az utolsó függvény 
        /// hívásával képződött eredményt kapjuk
        /// </summary>
        private static void HarmadikDelegatePelda()
        {
            FvDelegatePelda fuggvenyek;
            
            fuggvenyek = delegate { return 0; };

            fuggvenyek += ElsoFuggveny;

            fuggvenyek += MasodikFuggveny;

            //biztosítjuk, hogy legyen valaki a híváslistán
            var fv = fuggvenyek;
            if (fv!=null)
            {
                var eredmeny = fuggvenyek();
                Console.WriteLine("Eredmeny {0}", eredmeny);
            }

        }

        private static int MasodikFuggveny()
        {
            Console.WriteLine("2-dik");
            return 2;
        }

        private static int ElsoFuggveny()
        {
            Console.WriteLine("1-ső");
            return 1;
        }

        /// <summary>
        /// Egy példa arra, hogy egy módosítást is átadhatunk delegate segítségével
        /// </summary>
        private static void MasodikDelegatePelda()
        {
            var modositoOsztaly = new ModositoOsztaly();

            modositoOsztaly.Add("Első elem");
            modositoOsztaly.Add("Második elem");
            modositoOsztaly.Add("Harmadik elem");
            modositoOsztaly.Add("Negyedik elem");
            modositoOsztaly.Add("Ötödik elem");
            modositoOsztaly.Add("Hatodik elem");

            modositoOsztaly.ModositasElvegzese(VeddKiAzOBetuket);

            ModositoOsztaly.fvDefinicio modositasok;

            modositasok = VeddKiAzMBetuket;

            modositasok += delegate (ref string szoveg)
            {
                szoveg = szoveg.Replace("k", "");
            };

            modositoOsztaly.ModositasElvegzese(modositasok);

            modositoOsztaly.Tartalom();
        }

        private static void VeddKiAzMBetuket(ref string modositando)
        {
            modositando = modositando.Replace("m", "");
        }

        private static void VeddKiAzOBetuket(ref string modositando)
        {
            modositando = modositando.Replace("o", "");
        }

        private static void ElsoDelegatePelda()
        {
            //Itt egy konkrét hiváslistát gyártok le a delegate alapján
            PeldaDelegate hivasLista;

            //Itt értéket adok a híváslistának, egy egy elemű listát
            hivasLista = EgyikFuggveny;

            //Itt a meglévő híváslistához hozzáadok egy újat
            hivasLista += EgyikFuggveny;

            //Itt egy másik, ugyanolyan függvényt veszek fel a híváslistára
            hivasLista += MasikFuggveny;


            hivasLista += HarmadikFuggveny;

            //itt pedig a híváslistát aktivizálom

            //biztosítjuk, hogy legyen valaki a híváslistán
            var hl = hivasLista;
            if (hl!=null)
            {
                hivasLista("ElsoUzenet");
            }
        }

        private static void HarmadikFuggveny(string uzenet)
        {
            Console.WriteLine("Harmadik Függvény: {0}", uzenet); 
        }

        static void EgyikFuggveny(string Param)
        {
            Console.WriteLine("Egyik Függvény: {0}", Param);
        }

        static void MasikFuggveny(string Valami)
        {
            Console.WriteLine("Másik Függvény: {0}", Valami);
        }

    }

    internal class ModositoOsztaly
    {
        List<string> lista = new List<string>();
        public delegate void fvDefinicio(ref string modositando);

        public void ModositasElvegzese(fvDefinicio fvHivaslista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                var x = lista[i];
                //biztosítjuk, hogy legyen valaki a híváslistán
                var fvhl = fvHivaslista;
                if (fvhl!=null)
                {
                    fvHivaslista(ref x);
                }
                lista[i] = x;
            }
        }

        public void Add(string elem)
        {
            lista.Add(elem);
        }

        public void Tartalom()
        {
            foreach (var elem in lista)
            {
                Console.WriteLine(elem);
            }
        }

    }
}

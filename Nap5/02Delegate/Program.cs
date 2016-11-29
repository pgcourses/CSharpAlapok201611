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

        static void Main(string[] args)
        {
            //ElsoDelegatePelda();

            MasodikDelegatePelda();
            Console.ReadLine();

        }

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
            hivasLista("ElsoUzenet");
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
                fvHivaslista(ref x);
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

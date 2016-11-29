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
            ElsoDelegatePelda();

            Console.ReadLine();

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
}

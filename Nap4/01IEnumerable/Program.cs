using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("1 kg  kenyér");
            //Console.WriteLine("10 dkg párizsi");
            //Console.WriteLine("1 l tej");
            //Console.WriteLine("1 kg liszt");
            //Console.WriteLine("1 még valami");
            //Console.WriteLine("meg még valami");
            //Console.WriteLine("és egy kis édesség");

            //foreach (var listaElem in BevasarloLista())
            //{
            //    Console.WriteLine(listaElem);
            //}

            var lista = new BejarhatoOsztaly();
            lista.Add("első");
            lista.Add("második");
            lista.Add("harmadik");
            lista.Add("negyedik");
            lista.Add("ötödik");
            lista.Add("hatodik");

            foreach (var elem in lista)
            {
                Console.WriteLine("----foreach elem: {0}", elem);
            }

            //Eredmény
            //----------------------------------------
            //    GetEnumerator
            //    MoveNext(pozicio: 0, vanMeg: True)
            //    Current(pozicio: 0, elem: első)
            //----foreach elem: első
            //    MoveNext(pozicio: 1, vanMeg: True)
            //    Current(pozicio: 1, elem: második)
            //----foreach elem: második
            //    MoveNext(pozicio: 2, vanMeg: True)
            //    Current(pozicio: 2, elem: harmadik)
            //----foreach elem: harmadik
            //    MoveNext(pozicio: 3, vanMeg: True)
            //    Current(pozicio: 3, elem: negyedik)
            //----foreach elem: negyedik
            //    MoveNext(pozicio: 4, vanMeg: True)
            //    Current(pozicio: 4, elem: ötödik)
            //----foreach elem: ötödik
            //    MoveNext(pozicio: 5, vanMeg: True)
            //    Current(pozicio: 5, elem: hatodik)
            //----foreach elem: hatodik
            //    MoveNext(pozicio: 6, vanMeg: False)

            //Ezt a kódot gyártja le a fordító
            //using (var bejaro = lista.GetEnumerator())
            //{
                //while (bejaro.MoveNext())
                //{
                //    var elem = bejaro.Current;
                //    Console.WriteLine("----foreach elem: {0}", elem);
                //    //elem feldolgozása
                //}
            //}

            Console.ReadLine();

            //var list = new List<string>();
            //var list2 = new List<Sikidom>();

        }

        private static IEnumerable<string> BevasarloLista()
        {
            yield return "1 kg  kenyér";
            yield return "10 dkg párizsi";
            yield return "1 l tej";
            yield return "1 kg liszt";
            yield return "1 még valami";
            yield return "meg még valami";
            yield return "és egy kis édesség";
        }

        class BejarhatoOsztaly : IEnumerable
        {
            List<string> lista = new List<string>();
            public void Add(string elem)
            {
                lista.Add(elem);
            }

            public IEnumerator GetEnumerator()
            {
                Console.WriteLine("    GetEnumerator");
                return new Bejaro(lista);
            }
        }

        class Bejaro : IEnumerator
        {
            private List<string> lista;
            int pozicio = -1;

            public Bejaro(List<string> lista)
            {
                this.lista = lista;
            }

            public object Current
            {
                get
                {
                    var current = lista[pozicio];
                    Console.WriteLine("    Current (pozicio: {0}, elem: {1})", pozicio, current);
                    return current;
                }
            }

            public bool MoveNext()
            {
                //pozicio = pozicio + 1;
                pozicio++;
                var vanMegElem = pozicio < lista.Count;
                Console.WriteLine("    MoveNext (pozicio: {0}, vanMeg: {1})", pozicio, vanMegElem);
                return vanMegElem;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

    }
}
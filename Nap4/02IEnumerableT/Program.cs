using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02IEnumerableT
{
    class Program
    {
        static void Main(string[] args)
        {
            var init = new Adatok[]
                            {
                                    new Adatok(nev: "Első", szam: 1),
                                    new Adatok(nev: "Második", szam: 2),
                                    new Adatok(nev: "Harmadik", szam: 3),
                                    new Adatok(nev: "Negyedik", szam: 4)
                            };

            var adatok = new BejarhatoAdatok<Adatok>(init);

            foreach (var adat in adatok)
            {
                Console.WriteLine("Név: {0}, Szám: {1}", adat.Nev, adat.Szam);
            }

            //Ha ezt futtatnám, akkor ez lenne az eredmény: 
            //Additional information: Collection was modified; enumeration operation may not execute.

            //var lista = new List<Adatok>(init);
            //foreach (var item in lista)
            //{
            //    lista.Remove(item);
            //}

            Console.ReadLine();
        }
    }

    class BejarhatoAdatok<T> : IEnumerable<T>, IEnumerator<T>
    {
        List<T> lista = new List<T>();
        int pozicio = -1;

        public BejarhatoAdatok(T[] adatok)
        {
            lista = new List<T>(adatok);
        }

        public T Current
        {
            get
            {
                return lista[pozicio];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            pozicio++;
            return pozicio < lista.Count;
        }

        public void Dispose()
        {
            //Ezt nem implementálom, mert különben a ciklusból kilépéskor
            //megszüntet engem is (én=BejarhatoAdatok) a foreach ciklus
            //throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }

    /// <summary>
    /// Ezt az osztályt fogjuk a gyűjteményben használni
    /// </summary>
    class Adatok
    {
        public Adatok(string nev, int szam)
        {
            Nev = nev;
            Szam = szam;
        }

        public string Nev { get; set; }
        public int Szam { get; set; }

    }
}

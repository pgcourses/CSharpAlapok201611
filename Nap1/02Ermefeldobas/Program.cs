using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Ermefeldobas
{
    //OO alapelvek
    // Elvonatkoztatás (Abstraction)
    // Egységbezárás (Encapsulation)
    // Modularitás (Modularity)
    // Hierarchia (Hierarchy)

    // OOP
    // Objektum
    //   1.) azonosítható (identity)
    //   2.) van állapota (state)
    //   3.) van viselkedése (behaviour)
    //
    // Osztály
    // Azonos típusú objektumok gyűjtője


    // Az osztálypéldány létrehozásáért felelős speciális függvény a konstruktor
    // ez fut le először
    // Amikor leszármaztatott osztálypéldányt hozunk létre, akkor mindig példányosodik egy ősosztály példány
    // ő fogja az ősosztály viselkedését/tulajdonságait végrehajtani

    ///      HamisErmefeldobo             ---(leszármaztatás)-->    ErmeFeldobo
    ///   |                              |                      |                              |
    ///   |                              |                      |  FeldobasEredmeny()          |   <-
    /// ->| new FeldobasEredmeny()       |                      |                              |
    ///   |                              |                      |                              |
    ///   | override HFeldobasEredmeny() | <- - - - - - - -     |  virtual HFeldobasEredmeny() |   <---------------


    class Program
    {
        static void Main(string[] args)
        {

            //Eredeti megoldás, meghívjuk az eredeti érmefeldobót
            ErmeFeldobo ermeFeldobo = new ErmeFeldobo();
            int eredmeny = ermeFeldobo.FeldobasEredmeny();
            Console.WriteLine("Az eredeti feldobás eredménye: {0}", eredmeny);
            Console.WriteLine();

            //Nézzük, hogy ugyanez működik-e hamisítottal?
            //NEM a hamisítottat hívjuk, hiszen az ErmeFeldobo felületén keresztül érjük el
            ErmeFeldobo ermeFeldobo2 = new HamisErmeFeldobo();
            int eredmeny2 = ermeFeldobo2.FeldobasEredmeny();
            Console.WriteLine("Az eredeti feldobás eredménye: {0}", eredmeny2);
            Console.WriteLine();

            //A hamísított eléréséhez rá kell hivatkoznunk
            int eredmeny3 = ((HamisErmeFeldobo)ermeFeldobo2).FeldobasEredmeny();
            Console.WriteLine("A hamisított feldobás eredménye: {0}", eredmeny3);
            Console.WriteLine();

            //A virtual/override valódi hamisítás működése
            ErmeFeldobo ermeFeldobo4 = new HamisErmeFeldobo();
            int eredmeny4 = ermeFeldobo4.HamisithatoFeldobasEredmeny();
            Console.WriteLine("A hamisított feldobás eredménye: {0}", eredmeny4);
            Console.WriteLine();

            //Az ősosztály függvényei minden leszármaztatott osztályban benne vannak.
            MegMegEgyFeldobo ermeFeldobo5 = new MegMegEgyFeldobo();
            ermeFeldobo5.PeldaALeszarmaztatottMukodesre();


            Console.ReadLine();
        }
    }

    /// <summary>
    /// Érmefeldobást szimuláló osztály
    /// </summary>
    class ErmeFeldobo
    {
        public ErmeFeldobo()
        {
            Console.WriteLine("ErmeFeldobo konstruktor");
        }


        Random generator = new Random();

        /// <summary>
        /// Feldobunk egy érmét, és az eredményét visszaadjuk
        /// Ez itt nem hamisítható
        /// </summary>
        /// <returns>0=Fej, 1=Írás</returns>
        internal int FeldobasEredmeny()
        {
            Console.WriteLine("Eredeti generátor");
            var kapottSzam = generator.Next(2);
            return kapottSzam;
        }

        /// <summary>
        /// Ezt direkt úgy hozom létre, hogy hamisítható legyen
        /// 
        /// Ha virtual függvényt hozok létre, akkor lehetőséget adok arra, hogy
        /// a leszármaztatott osztályban módosítsam a függvény működését
        /// </summary>
        /// <returns></returns>
        internal virtual int HamisithatoFeldobasEredmeny()
        {
            Console.WriteLine("Hamisítható Eredeti generátor");
            var kapottSzam = generator.Next(2);
            return kapottSzam;
        }

        /// <summary>
        /// Példa arra, hogy az ősosztály függvénye minden leszármaztatott
        /// osztályban ott van, ha new-val el nem nyomom
        /// vagy valamelyik leszármaztatott osztályban az override függvény sealed.
        /// Ekkor onnentól nem származtatható le.
        /// </summary>
        internal virtual void PeldaALeszarmaztatottMukodesre()
        {
        }

    }

    /// <summary>
    /// Leszármaztatás: a leszármaztatott osztályom mindent tud, 
    /// amit az eredeti, csak módosíthatom a működést
    /// </summary>
    class HamisErmeFeldobo : ErmeFeldobo
    {

        public HamisErmeFeldobo()
        {
            Console.WriteLine("HamisErmeFeldobo konstruktor");
        }

        /// <summary>
        /// New kulcsszó: az ősosztály függvényével azonos nevű, de ahhoz semmilyen módon nem 
        /// kapcsolódó új függvényt csinálok.
        ///
        /// Ezt így nem tudjuk meghamisítani
        /// mert a fordító azonos név esetén automatikusan new kulcsszót használ
        /// </summary>
        /// <returns></returns>
        internal new int FeldobasEredmeny()
        {
            Console.WriteLine("Hamisított generátor");
            return 1;
        }

        /// <summary>
        /// Ha virtual függvény van az eredeti ősosztályban, akkor
        /// override kulcsszóval ezt felülírhatom
        /// </summary>
        /// <returns></returns>
        internal override int HamisithatoFeldobasEredmeny()
        {
            Console.WriteLine("Hamisított eredeti generátor");
            return 1;
        }
    }

    class MegEgyFeldobo : ErmeFeldobo
    {
        internal sealed override void PeldaALeszarmaztatottMukodesre()
        {
            
        }

    }

    class MegMegEgyFeldobo : HamisErmeFeldobo
    {
        public MegMegEgyFeldobo()
        {
            Console.WriteLine("MegMegEgyFeldobo konstruktor");
        }
    }
}

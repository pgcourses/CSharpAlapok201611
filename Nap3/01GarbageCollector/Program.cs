using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//class Ugyfel { public Cim {get; set; } }
//class Cim { ~Cim() { } }

///             ROOT                                        HEAP                         finalizer queue
/// |   hívási verem változói        |         |                   ----------------------- (1)Cim         |
/// |   lokális változók             |         |   Ugyfel          |            |     |                   |
/// |   hívási paraméterei           |         |      |            |            |     |                   |
/// |   statikus property-k és mezők |         |      -->Cim  <-----            |     |                   |
/// |   finalizer queue              |         |          ~Cim()                |     |                   |
/// |   f-reachable queue            |         |                                |     |                   |
/// |                                |         |                                |     |-------------------|
/// |                                |         |                                |      
/// |                                |         |                                |       f-reachable queue
/// |                                |         |                                |     |    (2)Cim         |
/// |                                |         |                                |     |                   |
/// |                                |         |                                |     |                   |
/// |                                |         |                                |     |                   |
/// |                                |         |                                |     |                   |
/// |                                |         |                                |     |                   |
/// |                                |         |                                |     |                   |
/// ----------------------------------         ----------------------------------     ---------------------

/// Generációkat kezel
/// A heap alján indul az objektum példányok tárolása, és minden tárolással egyre feljebb kerül az a mutató,
/// ami a verem legelső szabad pozícióját tartalmazza. Mindig itt jön létre a következő objektumpéldány,
/// így a verem tetjén a legfiatalabb objektumok lesznek, amikre még nem futott a szemétgyűjtés
/// 
/// 0 (gyerek): ők vannak a heap tetején, még nem futott rájuk a szemétgyüjtő. Leggyakrabban rájuk fut a szemétgyűjtés
/// 1 (szülők): rájuk egyszer már lefutott a szemétgyüjtő, de nem takarította ki őket.
/// 2.(nagyszülők): rájuk már kétszer lefutott, de még mindig állnak. Legritkábban rájuk fut a szemétgyűjtés.

namespace _01GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            var alap = new Alap(-1);
            //var leszarmaztatott = new Leszarmaztatott();
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine(GC.GetGeneration(alap));
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine(GC.GetGeneration(alap));
            alap = null;
            //leszarmaztatott = null;
            GC.Collect();  //minden generációra gyűjtünk
            Console.WriteLine("Szemétgyűjtés lefutott");

            //Objektumok helyfoglalása
            Console.WriteLine("Foglalt memória: {0}", GC.GetTotalMemory(false));

            var lista = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                lista.Add(new string('A', 6000));
            }
            Console.WriteLine("Foglalt memória: {0}", GC.GetTotalMemory(false));

            //A két memóriafoglalás közötti különbségből tudjuk megállapítani az adott programrész memóriafoglalását.
            for (int i = 0; i < 3; i++)
            {
                var leszarmaztatott = new Leszarmaztatott(i);
                //Console.WriteLine("Ezt csak úgy beírom");
                //leszarmaztatott = null;
            }
            GC.Collect();

            //Kövessük nyomon egy hivatkozás korosítását menet közben.
            var korositando = new List<string>();
            for (int i = 0; i < 1050; i++)
            {
                Thread.Sleep(20);
                korositando.Add(new string('A', 6000));
                Console.Write(GC.GetGeneration(korositando));
            }




            Console.ReadLine();
        }
    }

    class Alap
    {
        protected int i;

        public Alap() { }

        public Alap(int i)
        {
            this.i = i;
        }

        ~Alap()
        {
            Console.WriteLine("Alap véglegesítő: {0}", i);
        }
    }

    class Leszarmaztatott : Alap
    {
        public Leszarmaztatott(int i) : base(i)
        { }

        ~Leszarmaztatott()
        {
            Console.WriteLine("Leszarmaztatott véglegesítő: {0}", i);
        }
    }

}

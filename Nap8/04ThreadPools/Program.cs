using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04ThreadPools
{

    /// <summary>
    /// Az operációs rendszer által elindított alkalmazás neve: folyamat (process)
    /// Az alkalmazáson belüli párhuzamos végrehajtási egység a szál (thread)
    /// A thread létrehozás erőforrásigényes folyamat, ennek kezelésére ThreadPool-t használ a .NET
    /// </summary>


    class Program
    {
        static readonly object _lock = new object();

        static void Main(string[] args)
        {
            //teszt1();

            //teszt2();

            teszt3();

            Console.ReadLine();
        }

        private static void teszt3()
        {
            int gyujto = 0;

            //var mre = new ManualResetEvent(false);
            WaitCallback callback = o =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;

                for (int i = 0; i < 100000; i++)
                {
                    //lock (_lock)
                    //{
                    //    gyujto += i;
                    //}

                    Interlocked.Add(ref gyujto, i);

                }
                //mre.Set();
                Console.WriteLine("+->{0} Végzett, eredmény: {1}, threadId: {2}", o, gyujto, id);
            };

            ThreadPool.QueueUserWorkItem(callback, "Egy");
            ThreadPool.QueueUserWorkItem(callback, "Kettő");
            ThreadPool.QueueUserWorkItem(callback, "Három");
            ThreadPool.QueueUserWorkItem(callback, "Négy");
            ThreadPool.QueueUserWorkItem(callback, "Öt");
            ThreadPool.QueueUserWorkItem(callback, "Hat");
            ThreadPool.QueueUserWorkItem(callback, "Hét");
            ThreadPool.QueueUserWorkItem(callback, "Nyolc");
            ThreadPool.QueueUserWorkItem(callback, "Kilenc");
            ThreadPool.QueueUserWorkItem(callback, "Tíz");
            //mre.WaitOne();
            Console.ReadLine();
            Console.WriteLine("Eredmény: {0}", gyujto);

            //lock nélkül
            //Eredmény: 292972955
            //Eredmény: -1540107552
            //Eredmény: 1305987038
            //Eredmény: -1119280394

            //lock segítségével
            //Eredmény: -1540107552
            //Eredmény: -1540107552
            //Eredmény: -1540107552
            //Eredmény: -1540107552

            //Interlocked segítségével
            //Eredmény: -1540107552
            //Eredmény: -1540107552
            //Eredmény: -1540107552
            //Eredmény: -1540107552

        }

        private static void teszt2()
        {
            var mre = new ManualResetEvent(false);

            WaitCallback callback = o =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("+->{0} Elindult, threadId: {1}", o, id);
                //Itt várakozunk egy külső jelre
                Console.WriteLine("+->{0} Várakozunk a szemaforra, threadId: {1}", o, id);
                mre.WaitOne();
                Console.WriteLine("+->{0} Végzett, threadId: {1}", o, id);
            };

            ThreadPool.QueueUserWorkItem(callback, "Egy");
            ThreadPool.QueueUserWorkItem(callback, "Kettő");
            ThreadPool.QueueUserWorkItem(callback, "Három");
            ThreadPool.QueueUserWorkItem(callback, "Négy");

            //Kézzel teszteljük a szemafort
            //Console.WriteLine("+Main: Szemafor piros");
            //Console.ReadLine();
            //mre.Set();
            //Console.WriteLine("+Main: Szemafor zöld");

            ThreadPool.QueueUserWorkItem(o => { Console.WriteLine("Öt vár"); Thread.Sleep(2000); Console.WriteLine("Öt beállít"); mre.Set(); });

        }

        private static void teszt1()
        {
            WaitCallback callback = o =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("+->{0} Elindult, threadId: {1}", o, id);
                //Ha ez rövidebb idő, pl. 1 másodperc (1000ms), akkor a thread felszabadul időben, és a következő feladat megkapja
                //ha ez ilyen hosszú, akkor egy idő után a scheduler új thread-et hoz létre, de ez időbe telik.
                Thread.Sleep(10000);
                Console.WriteLine("+->{0} Végzett, threadId: {1}", o, id);
            };


            //Processzoronként 1 thread a minimum, ez alá nem tudunk menni.
            //ThreadPool.SetMaxThreads(1, 300); // többprocesszoros gépen hatástalan
            ThreadPool.SetMaxThreads(6, 300); // többprocesszoros gépen hatástalan
            ThreadPool.SetMinThreads(6, 300);

            ThreadPool.QueueUserWorkItem(callback, "Egy");
            ThreadPool.QueueUserWorkItem(callback, "Kettő");
            ThreadPool.QueueUserWorkItem(callback, "Három");
            ThreadPool.QueueUserWorkItem(callback, "Négy");
            ThreadPool.QueueUserWorkItem(callback, "Öt");
            ThreadPool.QueueUserWorkItem(callback, "Hat");
            ThreadPool.QueueUserWorkItem(callback, "Hét");
            ThreadPool.QueueUserWorkItem(callback, "Nyolc");

        }
    }
}

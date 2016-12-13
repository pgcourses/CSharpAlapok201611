using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01TaskokOsszefuzese
{

    /// <summary>
    /// 
    ///        
    ///                          /--------> tkovetkezo1 --------\
    ///                         /                                \
    ///        tindulo -------->----------> tkovetkezo2 -------------> tlezaro
    ///                         \                                /
    ///                          \--------> tkovetkezo3 --------/
    ///                          
    /// </summary>




    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int, int> Szamolas = (name, param) =>
            {
                Console.WriteLine("+-->Elindult: {0} Param: {1}, Thread: {2}", name, param, Thread.CurrentThread.ManagedThreadId);
                int sum = 0;
                for (int i = 0; i < param; i++)
                {
                    if (i%2==0)
                    { //ha páros számolok
                        sum += i;
                    }
                    else
                    { //ha páratlan akkor várok
                        Thread.Sleep(20);
                    }
                }
                return sum;
            };

            //1. Hogy kell visszaadni eredményt taskból?
            //   A megoldás: a generikus Task<T> osztály
            //var tindulo = new Task<int>(Szamolas);
            //2. Hogy adok paramétert a task által végrehajtott feladatnak?
            //   A megoldás: a Func<T> lambda, amin belül már kedvemre tudom a paraméterezést megadni
            var tindulo = new Task<int>(() => { return Szamolas("tindulo", 15); });

            var tkovetkezo1 = tindulo.ContinueWith<int>(
                ti =>
                {
                    Thread.Sleep(1500);
                    return Szamolas("tkovetkezo1", ti.Result);
                }
            );

            var tkovetkezo2 = tindulo.ContinueWith<int>(
                ti =>
                {
                    Thread.Sleep(1000);
                    return Szamolas("tkovetkezo2", ti.Result / 2);
                }
            );

            var tkovetkezo3 = tindulo.ContinueWith<int>(
                ti =>
                {
                    Thread.Sleep(500);
                    return Szamolas("tkovetkezo3", ti.Result * 2);
                }
            );

            var tlezaro = Task<int>.Factory.ContinueWhenAll(
                new Task<int>[] { tkovetkezo1, tkovetkezo2, tkovetkezo3 }
                , tasks => 
                    {
                        int sum = 0;
                        foreach (var task in tasks)
                        {
                            sum += task.Result;
                        }
                        return Szamolas("tlezaro", sum / 10);
                    }
            );

            tindulo.Start();
            //
            //ez nem kell: tlezaro.Wait(), mert lekérdezem a Result-ot és az megvárja a végét.
            Console.WriteLine("Eredmény: {0}", tlezaro.Result);
            Console.ReadLine();
        }

        //Ezek is lehetnének lambda kifejezés helyett:
        //private static int Bevegzes(Task[] arg)
        //{
        //    throw new NotImplementedException();
        //}

        //private static int Szamolas(string name, int param)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

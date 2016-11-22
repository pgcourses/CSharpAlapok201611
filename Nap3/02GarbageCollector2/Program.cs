using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02GarbageCollector2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!Console.KeyAvailable)
            {
                //Így folyamatosan megy a szemétgyüjtő
                //var stream = new MemoryStream(100000);

                //Ezen ez nem segít!
                //using (var stream = new MemoryStream(100000)) { }

                //Így kifutunk a memóriából
                //Thread.Sleep(20);
                //var bitmap = new Bitmap(1280,1024);
                
                //Így viszont nincs memóriapróbléma
                using (var bitmap = new Bitmap(1280,1024)) { }

            }
        }
    }
}

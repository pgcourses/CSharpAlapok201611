using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01AsyncFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string filenev = "teszt.txt";
            using (var fs = new FileStream(filenev, FileMode.Create))
            {
                fs.SetLength(1000000);
            }

            //A várakozáshoz létrehozok egy kétállapotú nem beállított szemafort
            var mre = new ManualResetEvent(false);

            using (var fs = new FileStream(filenev, FileMode.Open))
            {
                int pufferMeret = 90000;
                byte[] puffer = new byte[pufferMeret];
                //A rekurzív híváshoz használunk egy lokális változót, amit létrehozunk, meghivatkozunk, beállítjuk, majd ezek után hívjuk a kódblokkot
                AsyncCallback rekurzivCallback = null; 
                AsyncCallback callback = ar=> 
                {
                    var olvasottByteok = fs.EndRead(ar);
                    if (olvasottByteok>0)
                    { //még nem végeztünk
                        Console.WriteLine("Belovasott byte-ok: {0}, pozíció: {1}", olvasottByteok, fs.Position);
                        fs.BeginRead(puffer, 0, pufferMeret, rekurzivCallback, null);

                    }
                    else
                    { //vége a munkának
                        Console.WriteLine("Az állomány végére értünk");
                        //beállítom a szemaforom
                        mre.Set();
                        Console.WriteLine("Szemafor beállítva");
                    }
                };
                rekurzivCallback = callback;
                fs.BeginRead(puffer, 0, pufferMeret, callback, null);

                Console.WriteLine("Szemaforra várunk");
                mre.WaitOne();
                Console.WriteLine("Szemafor zöld");
            }

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02AsyncFileUsingNelkul
{
    class Program
    {
        static void Main(string[] args)
        {
            string filenev = "teszt.txt";
            using (var fscreate = new FileStream(filenev, FileMode.Create))
            {
                fscreate.SetLength(1000000);
            }

            var fs = new FileStream(filenev, FileMode.Open);
            int pufferMeret = 90000;
            byte[] puffer = new byte[pufferMeret];
            AsyncCallback rekurzivCallback = null;
            AsyncCallback callback = ar =>
            {
                var olvasottByteok = fs.EndRead(ar);
                if (olvasottByteok > 0)
                { //még nem végeztünk
                    Console.WriteLine("Belovasott byte-ok: {0}, pozíció: {1}", olvasottByteok, fs.Position);
                    fs.BeginRead(puffer, 0, pufferMeret, rekurzivCallback, null);

                }
                else
                { //vége a munkának
                    Console.WriteLine("Az állomány végére értünk");
                    fs.Dispose();
                }
            };
            rekurzivCallback = callback;
            fs.BeginRead(puffer, 0, pufferMeret, callback, null);

            Console.ReadLine();
        }
    }
}

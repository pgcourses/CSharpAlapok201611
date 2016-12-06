using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01StreamekBevezetés
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = "teszt.txt";
            File.WriteAllText(filename,
                        string.Format("Ez a kiírandó tartalom. \n vagy {0} is írhatok. {0} További speciális karakterek: {1}, {2}, {3}, {4}"
                            , Environment.NewLine
                            , (char)113 //ASCII kódból karakter
                            , Convert.ToChar(115) //ugyanez másként
                            , '\u0027' //UNICODE karakter írása

                            //készítünk egy byte tömböt karakterkódokkal
                            //megadjuk az encoding-ot
                            //majd string-gé alakítjuk
                            , new string(Encoding.ASCII.GetChars(new byte[] { 35, 36 }))
                            )
                            , Encoding.UTF8);

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var pufferSize = fs.Length;
                byte[] puffer;
                puffer = new byte[pufferSize];

                var offset = 0;
                var count = 0;
                while ((count = fs.Read(puffer, offset, Math.Min((int)pufferSize, (int)fs.Length - offset)))>0)
                {
                    offset += count;
                    Console.WriteLine("Beolvasva: {0}, pozíció: {1}", count, offset);

                }
            }

            Console.ReadLine();
        }
    }
}

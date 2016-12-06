using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02StreamekStreamReader
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

            using (var fs = new FileStream(filename, FileMode.Open))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var text = sr.ReadLine();
                        Console.WriteLine(text);
                    }
                }
            }

            Console.ReadLine();

        }
    }
}

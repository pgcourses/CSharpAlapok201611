using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05Allomanykezeles
{
    class Program
    {
        static void Main(string[] args)
        {
            //File, Path, Directory osztályok

            var filename = "teszt.txt";

            //File osztállyal elérjük az operációs rendszer állományait

            //Állomány létehozása
            File.WriteAllText(filename, 
                        string.Format("Ez a kiírandó tartalom. \n vagy {0} is írhatok. {0} További speciális karakterek: {1}, {2}, {3}, {4}" 
                            ,Environment.NewLine
                            ,(char)113 //ASCII kódból karakter
                            ,Convert.ToChar(115) //ugyanez másként
                            ,'\u0027' //UNICODE karakter írása

                            //készítünk egy byte tömböt karakterkódokkal
                            //megadjuk az encoding-ot
                            //majd string-gé alakítjuk
                            ,new string(Encoding.ASCII.GetChars(new byte[] { 35, 36}))
                            )
                            ,Encoding.UTF8);

            //Meglévő állomány írása ugyanígy
            //File.AppendAllLines
            //File.AppendAllText

            //Állomány beolvasása

            //A teljes szöveg egy változóba megy
            var text = File.ReadAllText(filename);

            //soronként egy szöveges tömbbe kerül
            var text2 = File.ReadAllLines(filename);

            //byte-onként egy byte tömbbe kerül.
            var data = File.ReadAllBytes(filename);

            //inverz műveletek
            //File.WriteAllLines(filename, text2);
            //File.WriteAllBytes(filename, data);

            Console.WriteLine("Az állomány létezik: {0}", File.Exists(filename));
            Console.WriteLine("Az állomány létezik: {0}", File.Exists("ezmegvalamiuj.valamimas"));

            //var info = new FileInfo(filename);

            var info = new FileInfo("C:\\");


            Console.WriteLine(info.Attributes.ToString());

            //info.Attributes.HasFlag(FileAttributes.Directory)
            //true
            //info.Attributes.HasFlag(FileAttributes.Hidden)
            //true
            //info.Attributes.HasFlag(FileAttributes.System)
            //true

            //65558


            var dirname = Path.Combine("C:\\", "temp", "sajat", "akarmi", "barmi");
            if (!Directory.Exists(dirname))
            {
                Directory.CreateDirectory(dirname);
            }

            var tmpPath = Path.GetTempPath();
            var tmpFile = Path.GetTempFileName();

            var ext = Path.GetExtension(tmpFile);
            var name1 = Path.GetFileNameWithoutExtension(tmpFile);
            var name2 = Path.GetFileName(tmpFile);
            var name3 = Path.GetDirectoryName(tmpFile);

            Console.ReadLine();
        }
    }
}

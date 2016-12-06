using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _03SerializeDeserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            var adat = new Adatosztaly()
            {
                Egesz = int.MaxValue,
                Tizedestort = 12.728m,
                Datum = DateTime.MaxValue,
                DatumMin = DateTime.MinValue,
                Szoveg = "ÁrvíztűrőTükörfúrógép"
            };

            var filenev = "teszt.txt";

            var serializer = new XmlSerializer(typeof(Adatosztaly));

            using (var fs = new FileStream(filenev, FileMode.Create))
            {
                serializer.Serialize(fs, adat);
            }

            using (var fs = new FileStream(filenev, FileMode.Open))
            {
                var beolvasott = serializer.Deserialize(fs);
                Console.WriteLine(JsonConvert.SerializeObject(beolvasott, Formatting.Indented));
            }
            Console.ReadLine();
        }
    }

    public class Adatosztaly
    {
        public int Egesz { get; set; }
        public decimal Tizedestort { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumMin { get; set; }
        public string Szoveg { get; set; }
    }

}

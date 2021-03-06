﻿using Newtonsoft.Json;
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
                Szoveg = "ÁrvíztűrőTükörfúrógép",
                Jelszo = "NagyonTitkosJelszó",
                AlAdatOsztaly = new AlAdatOsztaly()
                {
                    Egesz = int.MinValue,
                    Tizedestort = decimal.MinValue,
                    Datum = DateTime.MaxValue,
                    DatumMin = DateTime.MinValue,
                    Szoveg = "ÁrvíztűrőTükörfúrógép"
                }
            };

            var listaadat = new ListaAdat();

            listaadat.ListaAdatok.Add(adat);
            listaadat.ListaAdatok.Add(adat);
            listaadat.ListaAdatok.Add(adat);
            listaadat.ListaAdatok.Add(adat);
            listaadat.ListaAdatok.Add(adat);
            listaadat.ListaAdatok.Add(adat);

            var filenev = "teszt.txt";

            //var serializer = new XmlSerializer(typeof(Adatosztaly));
            var serializer = new XmlSerializer(typeof(ListaAdat));
            
            using (var fs = new FileStream(filenev, FileMode.Create))
            {
                //serializer.Serialize(fs, adat);
                serializer.Serialize(fs, listaadat);
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
        [XmlElement("EzIttEgyFurcsaPropertyNev")] //Az XML állományban így szerepel
        public DateTime Datum { get; set; }
        public DateTime DatumMin { get; set; }
        public string Szoveg { get; set; }
        [XmlIgnore] //Ezzel lehet megoldani, hogy ez az adat ne szövegesítődjön
        public string Jelszo { get; set; }
        public AlAdatOsztaly AlAdatOsztaly { get; set; }
    }

    public class AlAdatOsztaly
    {
        public int Egesz { get; set; }
        public decimal Tizedestort { get; set; }
        public DateTime Datum { get; set; }
        public DateTime DatumMin { get; set; }
        public string Szoveg { get; set; }
    }

    public class ListaAdat
    {
        public ListaAdat()
        {
            ListaAdatok = new List<Adatosztaly>();
        }

        public List<Adatosztaly> ListaAdatok { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Log4Net
{
    class Program
    {
        //todo: osztálynév
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("_01Log4Net.Program");

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            //PeldaNaplo1();

            for (int i = 0; i < 10; i++)
            {
                log.Debug("Ez egy naplóüzenet a log4net-ből");
            }

            Console.ReadLine();

        }

        private static void PeldaNaplo1()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.WriteLine("Ez a hibakeresési információ: {0}", i);
            }
        }
    }
}

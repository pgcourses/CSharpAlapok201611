using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            //PeldaNaplo1();

            //Peldanaplo2();

            var r = new Random();

            while (!Console.KeyAvailable)
            {
                var level = r.Next(95);

                if (level<50)
                {
                    log.DebugFormat("Ez egy DEBUG üzenet: {0}", level);
                }

                if (level >= 50
                    && level<70)
                {
                    log.InfoFormat("Ez egy INFO üzenet: {0}", level);
                }

                if (level >= 70
                    && level < 85)
                {
                    log.WarnFormat("Ez egy WARN üzenet: {0}", level);
                }

                if (level >= 80
                    && level < 90)
                {

                    try
                    {
                        throw new ArgumentNullException();
                    }
                    catch (Exception)
                    {
                        //log.Error("Hiba történt", ex);
                        //throw;
                    }
                }

                if (level >= 90
                    && level < 95)
                {
                    log.FatalFormat("Ez egy FATAL üzenet: {0}", level);
                }

                Thread.Sleep(200);
            }


            Console.ReadLine();

        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            log.Error("Hiba történt", e.Exception);
        }

        private static void Peldanaplo2()
        {
            for (int i = 0; i < 10; i++)
            {
                log.Debug("Ez egy naplóüzenet a log4net-ből");
            }
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

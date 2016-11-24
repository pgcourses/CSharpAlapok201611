using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
///                                                              Exception
///                                                                  |
///                                                                  ˇ
///                                                           SystemException           
///                                                                  |
///                             -------------------------------------|------------------------------------------------------------
///                             |                                    |                          |                                |
///                             ˇ                                    ˇ                          ˇ                                ˇ
///                   NullReferenceException             InvalidOperationException         OutOfMemoryException            ArgumentException
///        
/// 
/// 
///                                                                                                                   ArgumentOutOfRangeException  
///                                                                                                                   
/// 
/// 
/// 
///      try
///      {
///               try
///               {
///                          try
///                          {
///                                 throw new ArgumentOutOfRangeException();
/// 
///                          }
///                          catch (InvalidOperationException)
///                          {
///                          }
///                          finally
///                          {
///                          }
///               }
///               catch (OutOfMemoryException)
///               {
///               }
///               finally
///               {
///               }
///    }
///    catch (Exception)
///    {
///    }
///    finally
///    {
///    }
/// 
/// 
/// 
/// 
/// </summary>

namespace _03Kivetelek
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;


            try
            {//1. itt végrehajtunk, ha kivétel keletkezik
             //kivételt így tudunk kiváltani:
                Console.WriteLine("try indul");
                //throw new ArgumentOutOfRangeException();

                Foprogram();

                Console.WriteLine("try vége");
            }
            catch (OutOfMemoryException) //A filterek kiértékelése felülről lefelé történik
            { }
            catch (InvalidOperationException) //lehetnek egymástól függetlenek a filterek
            { }
            catch (Exception ex) //vagy egyre feljebb a leszármaztatási fában
            {//2. ez végrehajtódik kivétel esetén, nincs kivétel, ez nem fut
                Console.WriteLine("catch indul");
                Console.WriteLine(ex.ToString());
                //vagy továbbmegyünk, 
                //vagy dobunk egy újabb kivételt
                //throw;
                Console.WriteLine("catch vége");
            }
            //catch { } //Erre nincs szükség, mert a keretrendszeren kívüle exception-okat
            //catch (RuntimeWrappedException)  //ez elkapja
            finally
            {//3. Ez minden esetben végrehajtódik, 
             //akár volt kivétel akár nem, 
             //akár dobtunk újabb kivételt akár nem
                Console.WriteLine("finally indul");

                Console.WriteLine("finally vége");
            }
            Console.WriteLine("finally után");

            Console.ReadLine();

        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Console.WriteLine("Log: {0}", e.Exception.ToString());
        }

        private static void Foprogram()
        {
            try
            {
                Console.WriteLine("Foprogram try indul");
                //throw new ArgumentOutOfRangeException();

                Alprogram();

                Console.WriteLine("Foprogram try vége");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Foprogram catch indul");
                Console.WriteLine(ex.ToString());
                //vagy továbbmegyünk, 
                //vagy dobunk egy újabb kivételt
                //1. throw ex : Újraszámozza a stack trace-t mintha innen jönne a kivétel
                //2. throw a throw helyén lévő stactrace-t írja át
                throw new Exception("Ez már a mi kivételünk", ex);

                Console.WriteLine("Foprogram catch vége");
            }
            finally
            {
                Console.WriteLine("Foprogram finally indul");

                Console.WriteLine("Foprogram finally vége");

            }
            Console.WriteLine("Foprogram finally után");
        }

        private static void Alprogram()
        {
            try
            {
                Console.WriteLine("Alprogram try indul");
                throw new AlkalmazasException();

                Console.WriteLine("Alprogram try vége");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alprogram catch indul");
                Console.WriteLine(ex.ToString());
                //vagy továbbmegyünk, 
                //vagy dobunk egy újabb kivételt
                //throw;
                throw new Exception("Ez már a mi kivételünk", ex);

                Console.WriteLine("Alprogram catch vége");
            }
            finally
            {
                Console.WriteLine("Alprogram finally indul");

                Console.WriteLine("Alprogram finally vége");

            }
            Console.WriteLine("Alprogram finally után");
        }
    }

    public class AlkalmazasException : Exception {}
    public class SajatDataSzintuException : AlkalmazasException { }
    public class SajatRepoSzintuException : SajatDataSzintuException { }
    public class SajatServiceSzintuException : SajatRepoSzintuException { }

}

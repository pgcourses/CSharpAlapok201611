using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _05AsyncMukodes
{
    class Program
    {

        static void Main(string[] args)
        {
            //Híváslista létrehozása, amin a függvények a következők:
            //paraméterként egy int és egy sztringet vár, és visszaad egy DateTime-ot
            Func<int, string, DateTime> am = (ciklusok, nev) =>
            {
                Console.WriteLine("+->{0} elindult, ciklusok: {1}", nev, ciklusok);
                for (int i = 0; i < ciklusok; i++)
                {
                    Console.WriteLine("+-->{0} ciklus: {1}", nev, i);
                    Thread.Sleep(400);
                }
                Console.WriteLine("+->{0} végzett", nev);
                return DateTime.Now;
            };

            var eredmeny0 = am(3, "Szinkron hívás");
            Console.WriteLine("+Fő szál: szinkron hívás elindult");
            Console.WriteLine("+Fő szál: szinkron hívás végzett, eredmény: {0}", eredmeny0);

            //Szinkron módon így is meg tudom hívni, ez egyenértékű az előzővel:
            //var eredmeny0 = am.Invoke(3, "Szinkron hívás");

            //Elindítani aszinkron módon a végrehajtást így tudjuk
            var ar01 = am.BeginInvoke(3, "Első példa (aszinkron indítás 01)", null, null);
            Console.WriteLine("+Fő szál: Első példa (aszinkron indítás 01) elindult");
            
            //Itt tudunk esetleg valami műveletet csinálni
            //de összehangolni elég nehéz lenne

            //Hogy lehet az aszinkron végrehajtás eredményéhez hozzáférni?
            //1. lehetőség: blokkolás
            var eredmeny01 = am.EndInvoke(ar01);
            Console.WriteLine("+Fő szál: Első példa (aszinkron indítás 01) végzett, eredmény: {0}", eredmeny01);

            //2. Pollozhatjuk a hívás végét jelző flag-et
            var ar02 = am.BeginInvoke(5, "Második példa (aszinkron indítás 02)", null, null);
            Console.WriteLine("+Fő szál: Második példa (aszinkron indítás 02) elindult");

            var wait = 0;
            while (!ar02.IsCompleted)
            {
                //Itt lehet egyéb munkát elvégezni
                Thread.Sleep(300);
                wait += 300;
                Console.WriteLine("+Fő szál: Várakozunk a második példára: {0}", wait);
            }

            var eredmeny02 = am.EndInvoke(ar02);
            Console.WriteLine("+Fő szál: Második példa (aszinkron indítás 02) végzett, eredmény: {0}", eredmeny02);

            //3. Várunk egy ideig az aszinkron végrehajtásra
            var ar03 = am.BeginInvoke(5, "Harmadik példa (aszinkron indítás 03)", null, null);
            Console.WriteLine("+Fő szál: Harmadik példa (aszinkron indítás 03) elindult");

            wait = 0;
            //Minden ciklusban várunk, amíg véget ér a feladat, de legfeljebb 300 ms-ot
            while (!ar03.AsyncWaitHandle.WaitOne(300))
            {
                //Itt lehet egyéb munkát elvégezni
                wait += 300;
                Console.WriteLine("+Fő szál: Várakozunk a harmadik példára: {0}", wait);
            }

            var eredmeny03 = am.EndInvoke(ar03);
            Console.WriteLine("+Fő szál: Harmadik példa (aszinkron indítás 03) végzett, eredmény: {0}", eredmeny03);

            //4. Több folyamat párhuzamos végrehajtása
            var ar041 = am.BeginInvoke(6, "Negyedik példa (aszinkron indítás 04-1)", null, null);
            var ar042 = am.BeginInvoke(5, "Negyedik példa (aszinkron indítás 04-2)", null, null);
            var ar043 = am.BeginInvoke(3, "Negyedik példa (aszinkron indítás 04-3)", null, null);
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-1) elindult");
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-2) elindult");
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-3) elindult");

            //Így egyszerre tetszőleges számú aszinkron végrehajtást be tudunk várni
            WaitHandle.WaitAll( new WaitHandle[] 
                        {
                            ar041.AsyncWaitHandle,
                            ar042.AsyncWaitHandle,
                            ar043.AsyncWaitHandle
                        }
            );

            var eredmeny041 = am.EndInvoke(ar041);
            var eredmeny042 = am.EndInvoke(ar042);
            var eredmeny043 = am.EndInvoke(ar043);
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-1) végzett, eredmény: {0}", eredmeny041);
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-2) végzett, eredmény: {0}", eredmeny042);
            Console.WriteLine("+Fő szál: Negyedik példa (aszinkron indítás 04-3) végzett, eredmény: {0}", eredmeny043);

            //5. Callback használata
            var ar051 = am.BeginInvoke(5, "Ötödik példa (aszinkron indítás 05-1)", MunkaVege, null);
            Console.WriteLine("+Fő szál: Ötödik példa (aszinkron indítás 05-1) elindult");

            //ugyanez lambdával
            var ar052 = am.BeginInvoke(5, "Ötödik példa (aszinkron indítás 05-2)"
                , x=> Console.WriteLine("+Callback szál: Ötödik példa (aszinkron indítás 05-2) végzett, eredményt nem ismerjük")
                , null);
            Console.WriteLine("+Fő szál: Ötödik példa (aszinkron indítás 05-2) elindult");

            //6. Callback visszatérési értékkel
            //visszatérési értékhez EndInvoke-ot kell hívnunk, ahhoz pedig kell a híváslista am változója,
            //ezért ezt átadjuk a BeginInvoke-nak
            var ar06 = am.BeginInvoke(5, "Hatodik példa (aszinkron indítás 06)", MunkaVegeEredmennyel, am);
            Console.WriteLine("+Fő szál: Hatodik példa (aszinkron indítás 06) elindult");

            //Az eredményhez való hozzáférés, AsyncState nélkül
            var ar07 = am.BeginInvoke(4, "Hetedik példa (aszinkron indítás 07)"
                ,ar => //Ez a formális paramétere a callback függvénynek, a lambda kifejezés baloldala
                {
                    var eredmeny = am.EndInvoke(ar); //A kódblokk hozzáfér a lokális változókhoz, így az am-hez is, ezért nem kell az átadásról gondoskodni
                    Console.WriteLine("+Callback szál: Hetedik példa (aszinkron indítás 07) végzett, eredmény: {0}", eredmeny);
                }
                , null);
            Console.WriteLine("+Fő szál: Hetedik példa (aszinkron indítás 07) elindult");


            //Ugyanaz, mint előbb, csak a callback-et egy változóba tesszük

            AsyncCallback callback = ar =>
                    {
                        var eredmeny = am.EndInvoke(ar);
                        Console.WriteLine("+Callback szál: Nyolcadik példa (aszinkron indítás 08) végzett, eredmény: {0}", eredmeny);
                    };

            var ar08 = am.BeginInvoke(3, "Nyolcadik példa (aszinkron indítás 08)", callback, null);

            Console.WriteLine("+Fő szál: Nyolcadik példa (aszinkron indítás 08) elindult");

            Console.ReadLine();

        }

        private static void MunkaVegeEredmennyel(IAsyncResult ar)
        {
            //a híváskor átadott 4. paraméterhez így tudunk hozzáférni:
            var hivaslista = ar.AsyncState;
            
            //Ahhoz, hogy a típus felületéhez hozzáférjünk, konvertálni kell:
            var am = (Func<int, string, DateTime>)hivaslista;

            //így már el tudjuk érni az eredményt:
            var eredmeny = am.EndInvoke(ar);
            Console.WriteLine("+Callback szál: Hatodik példa (aszinkron indítás 06) végzett, eredmény: {0}", eredmeny);

        }

        private static void MunkaVege(IAsyncResult ar)
        {
            Console.WriteLine("+Callback szál: Ötödik példa (aszinkron indítás 05-1) végzett, eredményt nem ismerjük");
        }
    }
}

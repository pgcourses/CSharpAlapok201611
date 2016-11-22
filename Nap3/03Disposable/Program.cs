using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03Disposable
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var tl = new TisztaLevego())
            {  }

            //Ez a kódblokk a try ágban megbróbálja végrehajtani
            //ami ott van
            //akár sikerül akár nem, utána a finally ágban lévő kódot
            //mindenképpen végrehajtja

            //A using-ot a fordító egy ilyen blokká fordítja
            //var tl2 = new TisztaLevego();
            //try
            //{

            //    //Itt használjuk a tl által hivatkozott példányt
            //    //elvégezzük amit kell
            //}
            //finally
            //{ //akármi történik, ez mindig lefut
            //    if (tl2 != null)
            //    {
            //        ((IDisposable)tl2).Dispose();
            //    }
            //}

        }
    }

    class TisztaLevego : IDisposable
    {
        public void Tennivalo()
        {
            //todo: végiggondolni, hogy ez így teljes megoldás-e?
            if (isDisposed==1)
            {
                throw new ObjectDisposedException(nameof(TisztaLevego));
            }
            //Tennivaló elintézése
        }

        private int myProperty;
        public int MyProperty
        {
            get
            {
                if (isDisposed == 1)
                {
                    throw new ObjectDisposedException(nameof(TisztaLevego));
                }
                //Tennivaló elintézése
                return myProperty;
            }

            set
            {
                if (isDisposed == 1)
                {
                    throw new ObjectDisposedException(nameof(TisztaLevego));
                }
                //Tennivaló elintézése
            }
        }

        //Miért kell IDisposable felületet megvalósítani?

        //1.
        //Ha IDisposable változót használunk osztályon belül, akkor 
        //(gentleman agreement) kötelesek vagyunk IDisposable felületet megvalósítani
        Stream menedzseltStream = new FileStream("testfile.txt", FileMode.Create);

        //2. nem menedzselt memóriát használunk
        IntPtr nemMenedzseltMemoria = IntPtr.Zero;

        //3. nagy méretű menedzselt memóriahasználat
        List<string> menedzseltMemoria = new List<string>();

        public TisztaLevego()
        {
            nemMenedzseltMemoria = Marshal.AllocHGlobal(1000000);
            //Mivel a szemétgyüjtő nem tud a nem menedzselt tevékenységemről 
            //szólok neki, hogy ennyivel kevesebből gazdálkodhat
            GC.AddMemoryPressure(1000000);

            for (int i = 0; i < 1000000; i++)
            {
                menedzseltMemoria.Add(new string('A', 1));
            }
        }

        ~TisztaLevego()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            //mivel a "normál üzletmenet" szerint takarítunk,
            //ezért szólunk a GC-nek, hogy a finalizerünket
            //nem kell meghívni később
            //így kivesszük a finalizer queue-ból a ránk mutató hivatkozást
            GC.SuppressFinalize(this);
        }

        //private bool isDisposed = false;
        private int isDisposed = 0;
        private void Dispose(bool dispose)
        {
            //Ez így nem szálbiztos
            //if (isDisposed)
            //{
            //    throw new ObjectDisposedException(nameof(TisztaLevego));
            //}
            //isDisposed = true;
            
            //Ezt a három lépést egy lépésben oldja meg
            //1. var old = isDisposed
            //2. isDisposed = 1
            //3. return old;
            if (Interlocked.Exchange(ref isDisposed, 1)==1)
            { //Ha ez van, akkor a Dispose már lefutott
                throw new ObjectDisposedException(nameof(TisztaLevego));
            }

            if (dispose)
            {
                //Ilyenkor még nem futott GC, 
                //tehát a menedzselt objektumokat takarítani kell

                //if (menedzseltMemoria != null)
                //{
                    menedzseltMemoria.Clear();
                //    menedzseltMemoria = null;
                //}

                //u.a.
                menedzseltStream.Dispose();
            }

            //nem menedzselt memória takarítása
            //if (nemMenedzseltMemoria != IntPtr.Zero)
            //{
                Marshal.FreeHGlobal(nemMenedzseltMemoria);
                GC.RemoveMemoryPressure(1000000);
            //    nemMenedzseltMemoria = IntPtr.Zero;
            //}

        }
    }
}

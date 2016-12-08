using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _04TaskokBevezetes
{
    class Program
    {
        static void Main(string[] args)
        {
            //task státuszok
            //teszt1();

            //task hibakezelés
            //teszt2();

            //task futás közbeni leállítása
            //teszt3();

            //több task párhuzamos leállítása
            //teszt4();

            //taszkok közötti függőségek kezelése
            teszt5();
            Console.ReadLine();

        }

        private static void teszt5()
        {
            //Ezt majd a főfeladatból hívjuk
            Task task = null;

            Action subtodo = () =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Alfeladat fut: {0}, {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
            };

            Action todo = () =>
            {
                Console.WriteLine("Feladat fut: {0}, {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
                Task.Factory.StartNew(subtodo, TaskCreationOptions.AttachedToParent); //hozzákötjük a létrehohoz
            };

            task = new Task(todo);

            Console.WriteLine("státusz: {0}", task.Status);
            task.Start();
            Console.WriteLine("státusz: {0}", task.Status);
            Thread.Sleep(100);
            Console.WriteLine("státusz: {0}", task.Status);
            Thread.Sleep(800);
            task.Wait();
            Console.WriteLine("státusz: {0}", task.Status);

            //státusz: Created
            //státusz: WaitingToRun
            //Feladat fut: 10, Running
            //státusz: WaitingForChildrenToComplete
            //Alfeladat fut: 11, WaitingForChildrenToComplete
            //státusz: RanToCompletion


        }

        private static void teszt4()
        {
            var cts = new CancellationTokenSource();

            //1. ahogy a nagykönyvben meg van írva
            Action todo1 = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    //3. lépés erőforrástakarítás ha kell
                    if (cts.Token.IsCancellationRequested)
                    { //ha igen, akkor itt el tudjuk végezni a erőforrások felszabadítását
                        Console.WriteLine("Cancel érkezett a task-ba, takarítunk");
                    }

                    //4. lépés: különleges exception dobása
                    cts.Token.ThrowIfCancellationRequested();


                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(100);
                }
            };

            //2. nem dobunk ThrowIfCancellationRequested() kérést
            Action todo2 = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    //3. lépés erőforrástakarítás ha kell
                    if (cts.Token.IsCancellationRequested)
                    { //ha igen, akkor itt el tudjuk végezni a erőforrások felszabadítását
                        Console.WriteLine("Cancel érkezett a task-ba, takarítunk");
                    }

                    //4. lépés: különleges exception dobása
                    //cts.Token.ThrowIfCancellationRequested();

                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(100);
                }
            };

            //3. nem kezelünk cancelt viszont dobunk egy exception-t
            Action todo3 = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(100);
                }
                throw new StackOverflowException();
            };


            var task1 = new Task(todo1,cts.Token); //throwIf
            var task2 = new Task(todo2, cts.Token); //nincs throwIf, ilyenkor a task státusza olyan, mintha rendben lefutott volna
            var task3 = new Task(todo1);            //throwIf, ilyenkor a task státusza olyan, mintha hibával ért volna véget: Ez nem cancel volt (The operation was canceled.)
            var task4 = new Task(todo2);            //nincs throwIf, ilyenkor a task lefut végig, és státusza olyan, mintha rendben lefutott volna
            var task5 = new Task(todo3, cts.Token); //exception, de ha nem indult el, akkor is Cancelled lesz az állapota
            var task6 = new Task(todo3);            //exception

            //Task1 státusz: Canceled
            //Task2 státusz: RanToCompletion
            //Task3 státusz: Faulted
            //Task4 státusz: RanToCompletion
            //Task5 státusz: Canceled
            //Task6 státusz: Faulted


            //task1.Start();
            //task2.Start();
            task3.Start();
            //task4.Start();
            //task5.Start();
            //task6.Start();

            try
            {
                Thread.Sleep(200);
                //5. lépés: cancel kiadása
                Console.WriteLine("Kiadjuk a Cancelt a tasknak a Mainből");
                cts.Cancel();
                //Task.WaitAll(new Task[] { task1, task2, task3, task4, task5, task6 });
                task3.Wait();
            }
            catch (AggregateException ex)
            { //6. lépés cancel kezelése
                foreach (var innerEx in ex.InnerExceptions)
                {
                    if (innerEx is TaskCanceledException)
                    {
                        Console.WriteLine("A task Cancel-lel fejeződött be");
                    }
                    else
                    {
                        Console.WriteLine("Ez nem cancel volt");
                        Console.WriteLine(innerEx.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ez egy sima exception: {0}", ex.Message);
            }

            Console.WriteLine("Task1 státusz: {0}", task1.Status);
            Console.WriteLine("Task2 státusz: {0}", task2.Status);
            Console.WriteLine("Task3 státusz: {0}", task3.Status);
            Console.WriteLine("Task4 státusz: {0}", task4.Status);
            Console.WriteLine("Task5 státusz: {0}", task5.Status);
            Console.WriteLine("Task6 státusz: {0}", task6.Status);

        }

        private static void teszt3()
        {
            //1. lépés: rádióadó
            var cts = new CancellationTokenSource();


            Action todo = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    //3. lépés erőforrástakarítás ha kell
                    if (cts.Token.IsCancellationRequested)
                    { //ha igen, akkor itt el tudjuk végezni a erőforrások felszabadítását
                        Console.WriteLine("Cancel érkezett a task-ba, takarítunk");
                    }

                    //4. lépés: különleges exception dobása
                    cts.Token.ThrowIfCancellationRequested();


                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(100);
                }
            };

            var task = new Task(todo, cts.Token); //2. lépés: adóra hangolás

            task.Start();

            try
            {
                Thread.Sleep(200);
                //5. lépés: cancel kiadása
                Console.WriteLine("Kiadjuk a Cancelt a tasknak a Mainből");
                cts.Cancel();
                task.Wait();
            }
            catch (AggregateException ex)
            { //6. lépés cancel kezelése
                foreach (var innerEx in ex.InnerExceptions)
                {
                    if (innerEx is TaskCanceledException)
                    {
                        Console.WriteLine("A task Cancel-lel fejeződött be");
                    }
                }
            }

            Console.WriteLine("Státusz: {0}", task.Status); 

        }

        private static void teszt2()
        {
            Action todo = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(100);
                }
                throw new StackOverflowException();
            };

            var task = new Task(todo);

            task.Start();
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
                //((AggregateException)ex).Flatten(); a fa struktúrájú exception láncot kiteríthetjük
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }

        private static void teszt1()
        {
            Action todo = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("i: {0}", i);
                    Thread.Sleep(500);
                }
            };


            var task = new Task(todo);

            Console.WriteLine("státusz: {0}", task.Status);
            task.Start();
            Console.WriteLine("státusz: {0}", task.Status);
            Thread.Sleep(100);
            Console.WriteLine("státusz: {0}", task.Status);
            task.Wait();
            Console.WriteLine("státusz: {0}", task.Status);

            //státusz: Created
            //státusz: WaitingToRun
            //i: 0
            //státusz: Running
            //i: 1
            //i: 2
            //i: 3
            //i: 4
            //i: 5
            //i: 6
            //i: 7
            //i: 8
            //i: 9
            //státusz: RanToCompletion

            //lehetséges státuszok
            //Created = 0,
            //WaitingForActivation = 1,
            //WaitingToRun = 2,
            //Running = 3,
            //WaitingForChildrenToComplete = 4,
            //RanToCompletion = 5,
            //Canceled = 6,
            //Faulted = 7
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //SetTimer();

            //Console.WriteLine("\nPress the Enter key to exit the application...\n");
            //Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            //Console.ReadLine();
            //timer.Stop();
            //timer.Dispose();
            //-----------------------------------------------------

            //musteri = new Queue();
            //musteri = new ConcurrentQueue<string>();
            //musteri.Enqueue("11");
            //musteri.Enqueue("22");
            //musteri.Enqueue("33");
            //musteri.Enqueue("44");
            //musteri.Enqueue("55");
            //musteri.Enqueue("66");
            //musteri.Enqueue("77");
            //musteri.Enqueue("88");
            //musteri.Enqueue("99");

            //Kontrol().Wait();
            //--------------------------------------------------
            //ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
            //AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            //concurrentBag.Add(0);
            //#region Task 1
            //Task.Run(() =>
            //{
            //    Console.WriteLine("****");
            //    for (int i = 1; i < 10; i++) 
            //        concurrentBag.Add(i);
            //    autoResetEvent.Set();
            //});
            //#endregion 
            //#region Task 2 
            //Task.Run(() =>
            //{
            //    Console.WriteLine("----");
            //    while (!concurrentBag.IsEmpty)
            //        if (concurrentBag.TryTake(out int data))
            //            Console.WriteLine(data);
            //    autoResetEvent.WaitOne();
            //});
            //#endregion
            //-------------------------------------------------------------------


            //Task t = new Task(BekleyenThred);
            //t.Start();
            //Thread.Sleep(3000);
            //reset.Reset();
            //----------------------------------------------

            AutoResetEvent res = new AutoResetEvent(false);
            StatusChecker st = new StatusChecker(3);
            Console.WriteLine("timerden evvel");
            var timer = new Timer(st.CheckStatus, res, 1000, 1000);

            Console.WriteLine("timerden sonra");
            res.WaitOne();
            Console.WriteLine("waitden sonra");
            timer.Change(0, 500);
            res.WaitOne();
            Console.WriteLine("is bitdi");
            Console.ReadLine();
            //var reset = new AutoResetEvent(false);

            //var timer = new Timer(Timer_Elapsed,reset,2000,2000);
            //Console.ReadLine();
        }





        static AutoResetEvent reset = new AutoResetEvent(false);
        static void BekleyenThred()
        {
            Console.WriteLine("gozleme basldi");
            reset.WaitOne();
            Console.WriteLine("Gozleme bitdi");
        }


        static ConcurrentQueue<string> musteri;
        static async Task Masa(string masaAdi)
        {
            while (musteri.Count > 0)
            {
                await Task.Delay(1000);
                musteri.TryDequeue(out string a);
                Console.WriteLine($"{masaAdi} - {a}");
            }
        }

        static async Task Kontrol()
        {
            var m1 = Masa("m1");
            var m2 = Masa("m2");
            await Task.WhenAll(m1, m2);
            Console.WriteLine("Masa sırası bitmiştir.");
        }


        public static System.Timers.Timer timer;

        static void SetTimer()
        {
            timer = new System.Timers.Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                         e.SignalTime);
        }

        private static void Timer_Elapsed(object sender)
        {
            Console.WriteLine(sender.ToString());
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                         DateTime.Now);
        }
    }




    class StatusChecker
    {
        private int invokeCount;
        private int maxCount;

        public StatusChecker(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Checking status {1,2}.",
                DateTime.Now.ToString("h:mm:ss.fff"),
                (++invokeCount).ToString());

            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }
}

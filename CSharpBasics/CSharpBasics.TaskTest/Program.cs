using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasics.TaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BeforeTask();

            ThreadTask();

            NowTask();

            Console.ReadKey();
        }

        #region 单线程
        static void BeforeTask()
        {
            Console.WriteLine("以前的我做饭，开始做饭...");
            Stopwatch stopwatch = Stopwatch.StartNew();//计时器
            //1.煮饭
            CookRice();
            //2.炖排骨
            Spareribs();
            //3.洗菜
            Wash();
            //4.炒茄子
            Eggplant();
            //5.凉拌菜
            ColdMix();

            Console.WriteLine("做饭完成，共计耗时：{0}秒！", stopwatch.ElapsedMilliseconds / 1000);
        }


        #region 同步方法
        private static void CookRice()
        {
            Console.WriteLine("开始【煮饭】");
            Thread.Sleep(3 * 1000);//模拟耗时
            Console.WriteLine("结束【煮饭】");
        }
        private static void Spareribs()
        {
            Console.WriteLine("开始【炖排骨】");
            Thread.Sleep(3 * 1000);//模拟耗时
            Console.WriteLine("结束【炖排骨】");
        }

        private static void Wash()
        {
            Console.WriteLine("开始【洗菜】");
            Thread.Sleep(1 * 1000);//模拟耗时
            Console.WriteLine("结束【洗菜】");
        }

        private static void Eggplant()
        {
            Console.WriteLine("开始【炒茄子】");
            Thread.Sleep(1 * 1000);//模拟耗时
            Console.WriteLine("结束【炒茄子】");
        }



        private static void ColdMix()
        {
            Console.WriteLine("开始【凉拌菜】");
            Thread.Sleep(1 * 1000);//模拟耗时
            Console.WriteLine("结束【凉拌菜】");
        }
        #endregion


        #endregion


        #region 多线程

        static void ThreadTask()
        {
            Console.WriteLine("以前的我做饭，开始做饭...");
            Stopwatch stopwatch = Stopwatch.StartNew();//计时器

            List<ManualResetEvent> list = new List<ManualResetEvent>();

            ManualResetEvent mre1 = new ManualResetEvent(false);
            Thread thread1 = new Thread((o) =>
            {
                Console.WriteLine("开始【煮饭】");
                Thread.Sleep(1 * 3000);//模拟耗时
                Console.WriteLine("结束【煮饭】");
                ((ManualResetEvent)o).Set();
            });
            list.Add(mre1);
            thread1.Start(mre1);

            ManualResetEvent mre2 = new ManualResetEvent(false);
            Thread thread2 = new Thread((o) =>
            {
                Console.WriteLine("开始【炖排骨】");
                Thread.Sleep(1 * 3000);//模拟耗时
                Console.WriteLine("结束【炖排骨】");
                ((ManualResetEvent)o).Set();
            });
            list.Add(mre2);
            thread2.Start(mre2);

            ManualResetEvent mre3 = new ManualResetEvent(false);
            Thread thread3 = new Thread((o) =>
            {
                Console.WriteLine("开始【炒茄子】");
                Thread.Sleep(1 * 1000);//模拟耗时
                Console.WriteLine("结束【炒茄子】");
                ((ManualResetEvent)o).Set();
            });
            list.Add(mre3);
            thread3.Start(mre3);

            ManualResetEvent mre4 = new ManualResetEvent(false);
            Thread thread4 = new Thread((o) =>
            {
                Console.WriteLine("开始【煮饭】");
                Thread.Sleep(1 * 1000);//模拟耗时
                Console.WriteLine("结束【煮饭】");
                ((ManualResetEvent)o).Set();
            });
            list.Add(mre4);
            thread4.Start(mre4);

            ManualResetEvent mre5 = new ManualResetEvent(false);
            Thread thread5 = new Thread((o) =>
            {
                Console.WriteLine("开始【凉拌菜】");
                Thread.Sleep(1 * 1000);//模拟耗时
                Console.WriteLine("结束【凉拌菜】");
                ((ManualResetEvent)o).Set();
            });
            list.Add(mre5);
            thread5.Start(mre5);

            WaitHandle.WaitAll(list.ToArray());
            Console.WriteLine("做饭完成，共计耗时：{0}秒！", stopwatch.ElapsedMilliseconds / 1000);
        }


        #endregion



        #region 异步方法

        static async Task NowTask()
        {
            Console.WriteLine("现在的我做饭，开始做饭...");
            Stopwatch stopwatch = Stopwatch.StartNew();//计时器
            List<Task> taskList = new List<Task>();

            //1.煮饭
            taskList.Add(NowCookRice());
            //2.炖排骨
            taskList.Add(NowSpareribs());
            //3.洗菜
            taskList.Add(NowWash());
            //4.炒茄子
            taskList.Add(NowEggplant());
            //5.凉拌菜
            taskList.Add(NowColdMix());
            await Task.WhenAll(taskList);
            Console.WriteLine("做饭完成，共计耗时：{0}秒！", stopwatch.ElapsedMilliseconds / 1000);

        }


        private static async Task NowCookRice()
        {
            Console.WriteLine("开始【煮饭】");
            await Task.Delay(3 * 1000);//模拟耗时
            Console.WriteLine("结束【煮饭】");
        }
        private static async Task NowSpareribs()
        {
            Console.WriteLine("开始【炖排骨】");
            await Task.Delay(3 * 1000);//模拟耗时
            Console.WriteLine("结束【炖排骨】");
        }

        private static async Task NowWash()
        {
            Console.WriteLine("开始【洗菜】");
            await Task.Delay(1 * 1000);//模拟耗时
            Console.WriteLine("结束【洗菜】");
        }

        private static async Task NowEggplant()
        {
            Console.WriteLine("开始【炒茄子】");
            await Task.Delay(1 * 1000);//模拟耗时
            Console.WriteLine("结束【炒茄子】");
        }



        private static async Task NowColdMix()
        {
            Console.WriteLine("开始【凉拌菜】");
            await Task.Delay(1 * 1000);//模拟耗时
            Console.WriteLine("结束【凉拌菜】");
        }
        #endregion


    }
}

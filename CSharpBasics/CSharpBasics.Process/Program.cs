using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasics.Process
{
    /// <summary>
    /// 多线程案例
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            //1.无参数多线程
            NoParamThread();

            //2.无参数 不同类 多线程
            NotEqualParamThread();

            //3.有参数 多线程
            HasParamThread();

            //通过匿名委托创建
            Thread thread_delegate = new Thread(delegate() { Console.WriteLine("我是通过匿名委托创建的线程"); });
            thread_delegate.Start();

            //通过Lambda表达式创建
            Thread thread_Lambda = new Thread(() => Console.WriteLine("我是通过Lambda表达式创建的委托"));
            thread_Lambda.Start();

            TrainTickets train = new TrainTickets();
            //创建两个线程同时访问Buy方法
            Thread t1 = new Thread(train.Buy);
            Thread t2 = new Thread(train.Buy);
            //启动线程
            t1.Start();
            t2.Start();


            Console.ReadKey();
        }



        #region 无参数
        /// <summary>
        /// 无参数，同类里面的静态方法
        /// </summary>
        static void NoParamThread()
        {
            Thread thread = new Thread(new ThreadStart(NoParamThreadMethod)); //传方法名，如果是当前类下静态方法，直接传方法名
            thread = new Thread(NoParamThreadMethod); //new ThreadStart 可以省略
            thread.Name = "无参数线程";//设置线程名称
            thread.Start();//启动线程
        }

        public static void NoParamThreadMethod()
        {
            Console.WriteLine(string.Format("无参数【{0}】线程已经启动", Thread.CurrentThread.Name));
        }



        /// <summary>
        /// 无参数，不同类里面的静态方法
        /// </summary>
        static void NotEqualParamThread()
        {
            ThreadTest test = new ThreadTest();
            Thread thread = new Thread(new ThreadStart(ThreadTest.NoParamThreadMethod)); //传方法名，如果是当前类下静态方法，直接传方法名
            thread = new Thread(ThreadTest.NoParamThreadMethod); //new ThreadStart 可以省略

            thread.Name = "ThreadStart线程";//设置线程名称
            thread.Start();//启动线程
        }
        #endregion


        static void HasParamThread()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(new ThreadTest().HasParamThreadMethod));
            thread.Name = "有参数线程";//设置线程名称
            thread.Start(DateTime.Now);//启动线程，通过Start传参
        }




    }

    public class ThreadTest
    {
        public static void NoParamThreadMethod()
        {
            Console.WriteLine(string.Format("无参数【{0}】线程已经启动，启动时间：{1}", Thread.CurrentThread.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
        }

        /// <summary>
        /// 要用object来接收，也可以使用复杂对象，进行装箱和拆箱操作，得到自己所要的参数
        /// </summary>
        /// <param name="currrentTime"></param>
        public void HasParamThreadMethod(object currrentTime)
        {
            Console.WriteLine(string.Format("有参数【{0}】线程已经启动，启动时间：{1}", Thread.CurrentThread.Name, ((DateTime)currrentTime).ToString("yyyy-MM-dd HH:mm:ss:ms")));
        }

    }

    #region 车票
    class TrainTickets
    {
        //车票剩余数量
        public int num = 1;
        private static readonly object locker = new object();
        public void Buy()
        {
            // lock (locker)
            {
                int tmp = num;
                if (tmp > 0)//判断是否还有车票，如果没有，则买票失败
                {
                    Thread.Sleep(1000);
                    num -= 1;
                    Console.WriteLine("买票成功，还剩余{0}张", num);
                }
                else
                {
                    Console.WriteLine("没票了");
                }
            }
        }
    } 
    #endregion
}

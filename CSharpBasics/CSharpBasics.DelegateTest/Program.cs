using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasics.DelegateTest
{
    class Program
    {
        //1、委托定义:修饰符 delegate 返回值类型 委托名称 (方法参数)
       private delegate int CalcDelegate(int num1, int num2);

        static void Main(string[] args)
        {
            //2、委托声明
            CalcDelegate calc1 = new CalcDelegate(Add);//传入方法名称
            //使用Invoke 调用方法
            Console.WriteLine("1+2={0}", calc1.Invoke(1, 2));

           var sum= calc1(1, 2);//调用委托方法
           Console.WriteLine("1+2={0}",sum);

            //3、委托扩展
           Console.WriteLine("1+2={0}", CalcExt(new CalcDelegate(Add), 1, 2));
           Console.WriteLine("1-2={0}", CalcExt(new CalcDelegate(Diff), 1, 2));

            //4.委托组播
            //使用+=实现委托组合,只有相同类型的委托可以组合， -=移除委托
           CalcDelegate calc2;
           CalcDelegate calc3 = new CalcDelegate(Add);
           calc2 = calc3;
           CalcDelegate calc4 = new CalcDelegate(Diff);
           calc2 += calc4;
           var difval = calc2(1, 2);//此时调用的是 Diff 方法

           calc2 -= calc4;//移除 CalcDelegate(Diff) 委托
           var addVal = calc2(1,2); //移除Diff方法，按照组合顺序，调用最后一个组合的方法，此时调用 Add 方法

            //5.内置委托

           //5.1 Action无参数调用
           Action action1 = new Action(ActionWithNoParaNoReturn);
           action1();

           //5.2 Action带参数调用
           Action<int> action2 = new Action<int>(ActionWithParam);
           action2(1);

           //5.3 使用delegate
           Action action3 = delegate { Console.WriteLine("这里是使用delegate"); };
           action3();

           //5.4  使用delegate 带参调用
           Action<int> action4 = delegate (int i) { Console.WriteLine("这里是使用delegate的委托，参数值是：{0}",i); };
           action4(1);

            // 5.5 使用匿名委托 
            Action<string> action5 = (string s) => { Console.WriteLine("这里是使用匿名委托，参数值是:{0}",s); };
            action5("1");

            // 5.6 Func 无参调用
            Func<int> func1 = new Func<int>(FunWithNoParam);
            Console.WriteLine("这是Func无参调用：{0}", func1());

            //5.7 Func 有参调用，最后一个参数表示返回值
            Func<int,int> func2 = new Func<int,int>(FunWithParam);
            Console.WriteLine("这是Func有参调用：{0}", func2(1));

            //5.4 匿名函数
            Func<int> func3 = () => { return 1; };
            Console.WriteLine("这是Func匿名函数无参调用：{0}", func3());

            // 5.5 匿名函数有参调用
            Func<int,int> func4 = (int i) => { return i; };
            Console.WriteLine("这是Func匿名函数有参调用：{0}", func4(1));

           //事件的使用
           Heater heater = new Heater();
           Alarm alarm = new Alarm();
           heater.BoilEvent += (new Alarm()).MakeAlert; //给热水器添加一个警报器，就有报警的功能
           heater.BoilEvent += Display.ShowMsg; //给热水器添加一个显示器，就有显示的功能
           heater.BoilWater(); //烧水，会自动调用注册过对象的方法

           Console.ReadKey();
        }

        #region 内置委托

        #region Action
        static void ActionWithNoParaNoReturn()
        {
            Console.WriteLine("这是无参数无返回值的Action委托");
        }

        static void ActionWithParam(int i)
        {
            Console.WriteLine("这里是有参数无返回值的委托，参数值是：{0}", i);
        }
        #endregion

        #region Func
        static int FunWithNoParam()
        {
            return 1;
        }

        static int FunWithParam(int i)
        {
            return i;
        }
        #endregion

        #endregion

        #region 计算器

        /// <summary>
        /// 委托扩展
        /// </summary>
        /// <param name="calcDelegate"></param>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
       static int CalcExt(CalcDelegate calcDelegate, int num1, int num2)
        {
            return calcDelegate(num1, num2);
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        static int Diff(int num1, int num2)
        {
            return num1 - num2;
        }
        #endregion
    }

    #region 热水器
    
    /// <summary>
    /// 热水器
    /// </summary>
    public class Heater
    {
        private int temperature;
        public delegate void BoilHandler(int param); //声明委托
        public event BoilHandler BoilEvent; //声明事件
        // 烧水
        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;
                if (temperature > 95)
                {
                    if (BoilEvent != null)
                    { //如果有对象注册
                        BoilEvent(temperature); //调用所有注册对象的方法
                    }
                }
            }
        }
    }

    /// <summary>
    /// 警报器
    /// </summary>
    public class Alarm
    {
        public void MakeAlert(int param)
        {
            Console.WriteLine("警报器：嘀嘀嘀，水已经 {0} 度了：", param);
        }
    }

    /// <summary>
    /// 显示器
    /// </summary>
    public class Display
    {
        public static void ShowMsg(int param)
        { //静态方法
            Console.WriteLine("显示器：水快烧开了，当前温度：{0}度。", param);
        }
    }
    #endregion
}

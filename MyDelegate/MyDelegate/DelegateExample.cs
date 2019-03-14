using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDelegate
{

    // 无参数无返回值委托
    public delegate void NoReturnNoParam();
    // 有参数无返回值委托
    public delegate void NoReturnWithParam(string param);
    // 无参数有返回值委托
    public delegate int WithReturnNoParam();
    // 有参数有返回值委托
    public delegate int WithReturnWithParam(string param);

    public delegate void MulticastDelegateTest();



    public class DelegateExample
    {
        // 定义事件委托，虽然这里是公开的public，但编译后会变成private
        public delegate void EventDelegate();
        // 基于委托定义事件，使用 event 关键字 
        public event EventDelegate eventExample;


        public void Show()
        {

            NoReturnNoParam noReturnNoParam = new NoReturnNoParam(new DelegateExample().NoReturnNoParamMethod);
            // 也可以去掉new，直接写方法
            //NoReturnNoParam noReturnNoParam1 = new DelegateExample().NoReturnNoParamMethod;

            // 执行委托
            noReturnNoParam.Invoke();
            // 省略Invoke版，与上面等价
            noReturnNoParam();

            // 无回调异步
            noReturnNoParam.BeginInvoke(null, null);

            // 有回调异步
            AsyncCallback asyncCallBack = new AsyncCallback(this.SendCallBack);
            IAsyncResult asyncResult = noReturnNoParam.BeginInvoke(asyncCallBack, "obj");
            noReturnNoParam.EndInvoke(asyncResult);

            // Action
            {
                Action action1 = new DelegateExample().NoReturnNoParamMethod;
                action1.Invoke();
                Action<string> action2 = new DelegateExample().NoReturnWithParamMethod;
                action2.Invoke("string Param");
            }

            // Func
            {
                Func<int> func1 = new DelegateExample().WithReturnNoParamMethod;
                int result1 = func1.Invoke();
                Func<string, int> func2 = new DelegateExample().WithReturnWithParamMethod;
                int result2 = func2.Invoke("string Param");

            }


            // 多播委托
            {
                // 多播委托
                var delegateExample = new DelegateExample();

                // 添加实例方法1
                MulticastDelegateTest mdTest = new DelegateExample().NoReturnNoParamMethod;
                // 添加实例方法2
                mdTest += delegateExample.NoReturnNoParamMethod_1;
                // 添加静态方法
                mdTest += StaticNoReturnNoParamMethod;

                foreach (MulticastDelegateTest item in mdTest.GetInvocationList())
                {
                    item.Invoke();
                }

                // 删除实例方法1
                mdTest -= new DelegateExample().NoReturnNoParamMethod;
                // 删除实例方法2
                mdTest -= delegateExample.NoReturnNoParamMethod_1;
                // 删除静态方法
                mdTest -= StaticNoReturnNoParamMethod;

                foreach (MulticastDelegateTest item in mdTest.GetInvocationList())
                {
                    item.Invoke();
                }
            }

            {




            }
        }

        // 运行事件
        public void RunEvent()
        {
            eventExample();
        }



        /// <summary>
        /// 无参数无返回值方法
        /// </summary>
        public void NoReturnNoParamMethod()
        {
            Console.WriteLine("This is NoReturnNoParam Method .");
        }

        /// <summary>
        /// 无参数无返回值方法
        /// </summary>
        public void NoReturnNoParamMethod_1()
        {
            Console.WriteLine("This is NoReturnNoParam Method 1 .");
        }

        /// <summary>
        /// 无参数无返回值静态方法
        /// </summary>
        public static void StaticNoReturnNoParamMethod()
        {
            Console.WriteLine("This is StaticNoReturnNoParam Method .");
        }

        /// <summary>
        /// 有参数无返回值
        /// </summary>
        public void NoReturnWithParamMethod(string param)
        {
            Console.WriteLine("This is NoReturnWithParam Method .");
        }

        /// <summary>
        /// 无参数有返回值
        /// </summary>
        /// <returns></returns>
        public int WithReturnNoParamMethod()
        {
            Console.WriteLine("This is WithReturnNoParam Method .");
            return 1;
        }

        /// <summary>
        /// 有参数有返回值
        /// </summary>
        /// <returns></returns>
        public int WithReturnWithParamMethod(string param)
        {
            Console.WriteLine("This is WithReturnWithParam Method .");
            return 2;
        }

        /// <summary>
        /// 异步回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallBack(IAsyncResult result)
        {
            object obj = result.AsyncState;
            Console.WriteLine($"执行了回调方法，AsyncState:{obj}");

            // to do
        }

    }

    public class ChildClass : DelegateExample
    {
        // 子类也不能使用父类的事件
        //eventExample+=;
    }
}

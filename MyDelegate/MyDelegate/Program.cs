using MyDelegate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace MyDelegate
//{
class Program
{
    static void Main(string[] args)
    {

        new DelegateExample().Show();

        // 使用事件
        var delegateExample = new DelegateExample();

        // 事件只能出现在+=、-=左边
        //delegateExample.eventExample = null;

        delegateExample.eventExample += new DelegateExample().NoReturnNoParamMethod;
        delegateExample.eventExample += new DelegateExample().NoReturnNoParamMethod_1;

        delegateExample.RunEvent();


        Console.Read();       
    }


}
//}

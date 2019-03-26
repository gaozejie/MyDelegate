using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"这里是主线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");

            // 如何启动
            {
                //// 启动线程
                //Task.Run(() => { Console.WriteLine($"通过Task启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); });
                //// 启动线程，带数据
                //var task1 = new Task(p => { Console.WriteLine($"通过Task又启动了一个线程，数据：{p}，线程ID：{Thread.CurrentThread.ManagedThreadId}"); }, "state");
                //task1.Start();

                //// 通过Task启动同步线程
                //var task2 = new Task(() => { Console.WriteLine($"通过Task启动了一个同步线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); });
                //task2.RunSynchronously();

                //// 通过线程池启动
                //Task.Factory.StartNew(() => { Console.WriteLine($"通过Task.Factory启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); });
                //// 通过线程池启动，带数据
                //Task.Factory.StartNew(p =>
                //{
                //    Console.WriteLine($"通过Task.Factory又启动了一个线程，数据：{p}，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                //}, "state");
            }

            // 简略生命周期
            {
                //    var task = new Task(() =>
                //    {
                //        Console.WriteLine($"任务执行开始，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                //        System.Threading.Thread.Sleep(1000);
                //        Console.WriteLine($"任务执行结束，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                //    });

                //    Console.WriteLine($"启动前前线程状态：{task.Status}，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                //    task.Start();
                //    Console.WriteLine($"启动后线程状态：{task.Status}，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                //    task.Wait();
                //    Console.WriteLine($"完成后线程状态：{task.Status}，线程ID：{Thread.CurrentThread.ManagedThreadId}");
            }

            // 线程等待
            {
                // Task.Wait
                {
                    //var task1 = Task.Run(() =>
                    //   {
                    //       Console.WriteLine($"通过Task启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //       Thread.Sleep(2000);
                    //   });


                    //task1.Wait(1000); // 等待1秒
                    //Console.WriteLine($"Task1完成状态：{task1.IsCompleted}，当前状态：{task1.Status}，线程ID：{Thread.CurrentThread.ManagedThreadId}");

                    //task1.Wait();       // 等待完成
                    //Console.WriteLine($"Task1完成状态：{task1.IsCompleted}，当前状态：{task1.Status}，线程ID：{Thread.CurrentThread.ManagedThreadId}");

                }

                // Task.WaitAny
                {
                    // IList<Task> taskList = new List<Task>();
                    // taskList.Add(Task.Run(() =>
                    //{
                    //    Console.WriteLine($"通过Task启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //    Thread.Sleep(2000);
                    //}));

                    // taskList.Add(Task.Run(() =>
                    //  {
                    //      Console.WriteLine($"通过Task启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //      Thread.Sleep(2000);
                    //  }));

                    // taskList.Add(Task.Run(() =>
                    // {
                    //     Console.WriteLine($"通过Task启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //     Thread.Sleep(2000);
                    // }));

                    // 等待其中一个任务完成
                    //var taskArr = taskList.ToArray();
                    //var index = Task.WaitAny(taskArr);
                    //Console.WriteLine($"有一个线程已经完成，线程实例ID：{taskArr[index].Id}");
                    //foreach (var item in taskList)
                    //{
                    //    Console.WriteLine($"WaitAny后线程状态：{item.Id}: {item.Status}");
                    //}

                    //// 等待所有任务完成
                    //Task.WaitAll(taskList.ToArray());
                    //foreach (var item in taskList)
                    //{
                    //    Console.WriteLine($"WaitAll后线程状态：{item.Id}: {item.Status}");
                    //}

                    // 等待其中一个任务完成，并返回Task，可以执行回调
                    //Task.WhenAny(taskArr).ContinueWith(p => Console.WriteLine($"有一个任务执行完了，线程ID：{Thread.CurrentThread.ManagedThreadId}"));

                    //// 等待所有任务完成，并返回Task，可以执行回调
                    //Task.WhenAll(taskArr).ContinueWith((p, m) => Console.WriteLine($"所有任务都完成了，开始ContinueWith，参数{m}，线程ID：{Thread.CurrentThread.ManagedThreadId}"), "state");

                    // 线程回调 
                    //var result = Task.Run(() =>
                    //{
                    //    Console.WriteLine($"启动一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //}).ContinueWith(p =>
                    //{
                    //    Console.WriteLine($"线程回调，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                    //    return 1;
                    //});
                    //Console.WriteLine($"ContinueWith结果：{result.Result}");
                }


            }

            // 线程取消
            {

            }

            // 线程池
            {

                IList<Task> taskList = new List<Task>();

                // 通过线程池启动
                taskList.Add(Task.Factory.StartNew(() => { Console.WriteLine($"通过Task.Factory启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); }));
                taskList.Add(Task.Factory.StartNew(() => { Console.WriteLine($"通过Task.Factory启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); }));
                taskList.Add(Task.Factory.StartNew(() => { Console.WriteLine($"通过Task.Factory启动了一个线程，线程ID：{Thread.CurrentThread.ManagedThreadId}"); }));

                var taskArr = taskList.ToArray();
                Task<int> task = Task.Factory.ContinueWhenAny(taskArr, (Task p) =>
                 {
                     Console.WriteLine($"有一个线程已经完成");
                     Console.WriteLine($"线程回调，线程ID：{Thread.CurrentThread.ManagedThreadId}");
                     return 1;
                 });
                Console.WriteLine($"ContinueWhenAny结果：{task.Result}");

                Task<int> task1 = Task.Factory.ContinueWhenAll(taskArr, p =>
               {
                   Console.WriteLine($"所有线程都完成");

                   return 2;
               });
                Console.WriteLine($"ContinueWhenAll结果：{task1.Result}");
            }

            //https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.tasks.task?view=netframework-4.7.2
            //https://www.cnblogs.com/wangchuang/p/5737188.html


            Console.WriteLine($"结束");

            Console.Read();
        }
    }
}

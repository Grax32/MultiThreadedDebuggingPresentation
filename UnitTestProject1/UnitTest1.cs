using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExerciseThreads()
        {
            var tasks = new List<Task>();

            for (var z = 0; z < 100; z++)
            {
                var task = new Task(CountStuff);

                task.Start();

                tasks.Add(task);

            }

            Task.WaitAll(tasks.ToArray());

            Debug.Print("HI");
        }

        static int x;
        static int z;

        public async static void CountStuff()
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < 10000; i++)
                {
                    x = i;
                    z = i * 2;
                }
            });
        }

        public struct CurrentStatusInfo
        {
            public int CurrentStatus { get; set; }
            public string CurrentStatusDescription { get; set; }
        }

        public static CurrentStatusInfo CurrentStatus { get; set; }

        [TestMethod]
        public void DoTheMultiPartReadTest()
        {
            var results = new List<Tuple<int, string>>(1000);

            var task = Task.Run(() =>
            {
                for (var i = 0; i < 200; i++)
                {
                    var status = CurrentStatus.CurrentStatus;
                    Thread.Sleep(5);
                    var description = CurrentStatus.CurrentStatusDescription;

                    results.Add(Tuple.Create(status, description));
                }
            });

            Parallel.For(0, 100, v =>
              {
                  Thread.Sleep(2);
                  CurrentStatus = new CurrentStatusInfo { CurrentStatus = v, CurrentStatusDescription = Convert.ToDouble(v).ChangeNumericToWords() };
              });

            task.Wait();

            Assert.IsTrue(results.All(v => Convert.ToDouble(v.Item1).ChangeNumericToWords() == v.Item2));
        }
    }
}

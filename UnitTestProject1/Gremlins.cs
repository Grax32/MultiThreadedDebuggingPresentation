using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class Gremlins
    {
        Dictionary<object, string> _answers = new Dictionary<object, string>();

        [TestMethod]
        public void DoTheGremlinTest()
        {
            var tasks = new List<Task>();
            var answer = "";

            Action<object> locateAnswer = (object state) =>
            {
                answer = state.ToString().ChangeNumericToWords();
                //Thread.Sleep(1);
                Debug.Print(state.ToString() + ":" + answer);
                _answers.Add(state, answer);
            };

            for (var z = 0; z < 100; z++)
            {
                var task = new Task(locateAnswer, z);
                tasks.Add(task);
            }

            tasks.ForEach(v => v.Start());

            Task.WaitAll(tasks.ToArray());

            // require that there be no duplicates
            Assert.AreEqual(_answers.Values.Count(), _answers.Values.Distinct().Count());
        }
    }
}


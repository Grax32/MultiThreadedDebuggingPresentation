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
        public async Task DoTheGremlinTest()
        {
            await Launch();
        }

        public async Task Launch()
        {
            var tasks = new List<Task<int>>();
            var answer = "";
            var turns = 100;

            Func<object, int> locateAnswer = (value) =>
            {
                answer = value.ToString() + ":" + value.ToString().ChangeNumericToWords();
                Debug.Print(value.ToString() + ":" + answer);
                _answers.Add(value, answer);  // to debug, set condition on breakpoint to  (!answer.StartsWith(value.ToString()))
                return (int)value;
            };




            await Task.Run(() =>
            {
                for (var z = 0; z < turns; z++)
                {
                    tasks.Add(new Task<int>(locateAnswer, z));
                }
            });

            tasks.ForEach(v => v.Start());

            int tmp;
            tasks.ForEach(v => { tmp = v.Result; });

            // require that there be no duplicates
            Assert.AreEqual(_answers.Values.Count(), _answers.Values.Distinct().Count());
            Assert.AreEqual(turns, _answers.Values.Count);
        }
    }
}


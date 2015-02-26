using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class LoopyLoop
    {
        [TestMethod]
        public void DoTheLoopyLoop1()
        {
            var counter = 0;
            Parallel.For(0, 1000, v =>
            {
                var x = counter + 1;
                counter = x;
                // counter++;
            });

            Assert.AreEqual(1000, counter);
        }
    }
}

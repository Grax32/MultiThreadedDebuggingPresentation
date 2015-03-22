using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class LoopyLoop
    {
        int counter = 0;

        [TestMethod]
        public void DoTheLoopyLoop1()
        {
            Parallel.For(0, 1000, v =>
            {
#if BROKEN
                var x = counter + 1;
                Thread.Sleep(1);
                counter = x; // use breakpoint condition of (counter + 1 != x)
                //counter++;
#else
                Interlocked.Increment(ref counter);
#endif
            });

            Assert.AreEqual(1000, counter);
        }
    }
}

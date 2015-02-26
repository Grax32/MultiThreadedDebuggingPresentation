using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class LockStockAndPeril
    {
        [TestMethod]
        public void DoTheLockStockAndPeril()
        {
            var coll = new SimpleCollection();

            Parallel.For(0, 1000, v =>
            {
                coll.Add(v);
            });

            Assert.AreEqual(1000, coll.Length);
        }
    }
}

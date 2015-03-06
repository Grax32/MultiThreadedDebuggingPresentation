using System;
using System.Linq;
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

            Parallel.For(0, 1000, v => coll.Add(v));

            Assert.AreEqual(1000, coll.Length);
            Parallel.For(0, 1000, v => Assert.IsTrue(coll.Values.Contains(v), string.Format("Value {0} is missing from array", v)));
        }
    }
}

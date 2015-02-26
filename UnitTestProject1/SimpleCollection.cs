using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class SimpleCollection
    {
        int[] values = new int[2];
        int next = -1;
        object lockObj = new object();
        object unlockObj = new object();

        public void Add(int value)
        {
            var vLength = values.Length;
            var vTest = values;
            //var id = Interlocked.Increment(ref next);
            var id = ++next;
            var newValues = new int[0];

            if (id >= values.Length)
            {
                newValues = Resize(id + 1);
            }

            values[id] = value;
        }


        private int[] Resize(int size)
        {
            lock (lockObj)
            {
                Thread.Sleep(50);
                if (size > values.Length)
                {
                    var newValues = new int[size];

                    Array.Copy(values, newValues, values.Length);

                    values = newValues;
                }
                return values;
            }
        }

        public int Length { get { return next + 1; } }
    }
}

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
        int[] _values = new int[2];
        int _next = -1;
        object _lockObj = new object();

        public void Add(int value)
        {
            var id = Interlocked.Increment(ref _next);

            var valueArray = _values;

            if (id >= _values.Length)
            {
                valueArray = Resize(id + 15);
            }

            valueArray[id] = value;  // use condition of !valueArray.equals(_values)
        }


        private int[] Resize(int size)
        {
            lock (_lockObj)
            {
                if (size > _values.Length)
                {
                    var newValues = new int[size];

                    Array.Copy(_values, newValues, _values.Length);

                    _values = newValues;
                }

                Thread.Sleep(15);

                return _values;
            }
        }

        public int Length { get { return _next + 1; } }

        public int[] Values { get { return _values; } }
    }
}

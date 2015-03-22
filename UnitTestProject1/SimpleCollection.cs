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
        readonly object _lockObj = new object();

        public void Add(int value)
        {
            var id = Interlocked.Increment(ref _next);

#if BROKEN
            var valueArray = _values;
#endif

            if (id >= _values.Length)
            {
#if BROKEN
                valueArray = Resize(id + 15);
#else
                Resize(id + 15);
#endif
            }

#if BROKEN
            valueArray[id] = value;  // use condition of !valueArray.equals(_values)
            // review the parallel watch window for variables valueArray and _values to see the current bad values
#else
            _values[id] = value;
#endif
        }

#if BROKEN
        private int[] Resize(int size)
#else
        private void Resize(int size)
#endif
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

#if BROKEN
                return _values;
#endif
            }
        }

        public int Length { get { return _next + 1; } }

        public int[] Values { get { return _values; } }
    }
}

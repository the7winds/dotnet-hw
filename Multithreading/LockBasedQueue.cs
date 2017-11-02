namespace Multithreading.LockBased
{
    using System.Threading;

    public class BlockingArrayQueue<T> : IBlockingQueue<T>
    {
        private T[] _array;
        private int _begin;
        private int _size;
        private object _guard;

        public BlockingArrayQueue(int arraySize)
        {
            _array = new T[arraySize];
            _guard = new object();
        }

        public void Clear()
        {
            lock (_guard)
            {
                _begin = 0;
                _size = 0;
            }
        }

        public T Deque()
        {
            lock (_guard)
            {
                while (_size == 0)
                {
                    Monitor.Wait(_guard);
                }

                T val = _array[_begin];
                _begin = (_begin + 1) % _array.Length;
                _size--;

                Monitor.Pulse(_guard);

                return val;
            }
        }

        public void Enque(T val)
        {
            lock (_guard)
            {
                while (_size == _array.Length)
                {
                    Monitor.Wait(_guard);
                }

                var pos = (_begin + _size) % _array.Length;
                _array[pos] = val;
                _size++;

                Monitor.Pulse(_guard);
            }
        }

        public bool TryDeque(out T val)
        {
            if (!Monitor.TryEnter(_guard))
            {
                val = default(T);
                return false;
            }

            try
            {
                if (_size == 0)
                {
                    val = default(T);
                    return false;
                }

                val = _array[_begin];
                _begin = (_begin + 1) % _array.Length;
                _size--;

                Monitor.Pulse(_guard);

                return true;
            }
            finally
            {
                Monitor.Exit(_guard);
            }
        }

        public bool TryEnque(T val)
        {
            if (!Monitor.TryEnter(_guard))
            {
                return false;
            }

            try
            {
                if (_size == _array.Length)
                {
                    return false;
                }

                var pos = (_begin + _size) % _array.Length;
                _array[pos] = val;
                _size++;

                Monitor.Pulse(_guard);

                return true;
            }
            finally
            {
                Monitor.Exit(_guard);
            }
        }
    }
}

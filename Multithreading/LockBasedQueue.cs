namespace Multithreading
{
    using System.Threading;

    public class LockBasedBlockingArrayQueue<T> : IBlockingQueue<T>
    {
        private readonly T[] _array;
        private int _begin;
        private int _size;
        private readonly object _guard;

        public LockBasedBlockingArrayQueue(int arraySize)
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

        public int Count() => _size;

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
            lock (_guard)
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
        }

        public bool TryEnque(T val)
        {
            lock (_guard)
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
        }
    }
}

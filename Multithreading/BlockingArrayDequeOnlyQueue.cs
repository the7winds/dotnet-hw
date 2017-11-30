namespace Multithreading
{
    using System.Threading;

    public class LockFreeBlockingArrayDequeOnlyQueue<T>
    {
        private readonly T[] _array;
        private QueueMeta _meta;

        private class QueueMeta
        {
            public readonly int Begin;
            public readonly int Size;

            public QueueMeta(int begin, int size)
            {
                Begin = begin;
                Size = size;
            }
        }

        public LockFreeBlockingArrayDequeOnlyQueue(int arraySize)
        {
            _array = new T[arraySize];
            _meta = new QueueMeta(0, 0);
        }

        public LockFreeBlockingArrayDequeOnlyQueue(LockFreeBlockingArrayDequeOnlyQueue<T> queue, T element)
        {
            _array = queue._array.Clone() as T[];

            var pos = (queue._meta.Begin + queue._meta.Size) % queue._array.Length;
            _array[pos] = element;

            var begin = queue._meta.Begin;
            var size = queue._meta.Size + 1;
            _meta = new QueueMeta(begin, size);
        }

        internal int Count() => _meta.Size;

        public bool IsFull()
        {
            return _meta.Size == _array.Length;
        }
        
        public bool TryDeque(out T val)
        {
            var oldMeta = _meta;

            if (oldMeta.Size == 0)
            {
                val = default(T);
                return false;
            }

            var oldValue = _array[oldMeta.Begin];
            var newBegin = (oldMeta.Begin + 1) % _array.Length;
            var newSize = oldMeta.Size - 1;
            var newMeta = new QueueMeta(newBegin, newSize);

            if (Interlocked.CompareExchange(ref _meta, newMeta, oldMeta) != oldMeta)
            {
                val = default(T);
                return false;
            }

            val = oldValue;
            return true;
        }
    }

}

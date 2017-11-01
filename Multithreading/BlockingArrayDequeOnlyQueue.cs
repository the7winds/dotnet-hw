namespace Multithreading.LockFree
{
    using System.Threading;

    public class BlockingArrayDequeOnlyQueue<T>
    {
        private T[] _array;

        private class QueueMeta
        {
            public QueueMeta(int begin, int size)
            {
                Begin = begin;
                Size = size;
            }

            public readonly int Begin;
            public readonly int Size;
        }

        private QueueMeta _meta;

        public BlockingArrayDequeOnlyQueue(int arraySize)
        {
            _array = new T[arraySize];
            _meta = new QueueMeta(0, 0);
        }

        public BlockingArrayDequeOnlyQueue(BlockingArrayDequeOnlyQueue<T> queue, T element)
        {
            _array = queue._array.Clone() as T[];

            var pos = (queue._meta.Begin + queue._meta.Size) % queue._array.Length;
            _array[pos] = element;

            var begin = queue._meta.Begin;
            var size = queue._meta.Size + 1;
            _meta = new QueueMeta(begin, size);
        }

        public bool IsFull()
        {
            return _meta.Size == _array.Length;
        }
        
        public bool TryDeque(out T val)
        {
            var old_meta = _meta;

            if (old_meta.Size == 0)
            {
                val = default(T);
                return false;
            }

            var old_value = _array[old_meta.Begin];
            var new_begin = (old_meta.Begin + 1) % _array.Length;
            var new_size = old_meta.Size - 1;
            var new_meta = new QueueMeta(new_begin, new_size);

            if (Interlocked.CompareExchange(ref _meta, new_meta, old_meta) != old_meta)
            {
                val = default(T);
                return false;
            }

            val = old_value;
            return true;
        }
    }

}

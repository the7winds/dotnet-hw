namespace Multithreading.LockFree
{
    using System.Threading;

    public class BlockingArrayQueue<T> : IBlockingQueue<T>
    {
        private readonly int _arraySize;
        private BlockingArrayDequeOnlyQueue<T> _dequeOnlyQueue;

        public BlockingArrayQueue(int arraySize)
        {
            _arraySize = arraySize;
            _dequeOnlyQueue = new BlockingArrayDequeOnlyQueue<T>(arraySize);
        }

        public void Clear()
        {
            var empty = new BlockingArrayDequeOnlyQueue<T>(_arraySize);

            while (true)
            {
                var oldQ = _dequeOnlyQueue;
                if (Interlocked.CompareExchange(ref _dequeOnlyQueue, empty, oldQ) == oldQ)
                {
                    return;
                }
            }
        }

        public T Deque()
        {
            T val;

            while (TryDeque(out val));

            return val
        }

        public void Enque(T val)
        {
            while (!TryEnque(val));
        }

        public bool TryDeque(out T val)
        {
            var oldQ = _dequeOnlyQueue;

            if (oldQ.TryDeque(out val) && _dequeOnlyQueue == oldQ)
            {
                return true;
            }

            return false;
        }

        public bool TryEnque(T val)
        {
            var oldQ = _dequeOnlyQueue;

            if (_dequeOnlyQueue.IsFull())
            {
                return false;
            }

            var newQ = new BlockingArrayDequeOnlyQueue<T>(_dequeOnlyQueue, val);
            return Interlocked.CompareExchange(ref _dequeOnlyQueue, newQ, oldQ) == oldQ;
        }
    }
}

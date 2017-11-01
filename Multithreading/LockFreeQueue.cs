namespace Multithreading.LockFree
{
    using System.Threading;

    public class BlockingArrayQueue<T> : IBlockingQueue<T>
    {
        private BlockingArrayDequeOnlyQueue<T> _dequeOnlyQueue;

        public BlockingArrayQueue(int arraySize)
        {
            _dequeOnlyQueue = new BlockingArrayDequeOnlyQueue<T>(arraySize);
        }

        public T Deque()
        {
            T var;

            while (!_dequeOnlyQueue.TryDeque(out var));

            return var;
        }

        public void Enque(T val)
        {
            while (!TryEnque(val));
        }

        public bool TryDeque(out T val)
        {
            return _dequeOnlyQueue.TryDeque(out val);
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

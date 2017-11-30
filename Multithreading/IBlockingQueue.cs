namespace Multithreading
{
    public interface IBlockingQueue<T>
    {
        void Enque(T val);
        T Deque();
        bool TryEnque(T val);
        bool TryDeque(out T val);
        int Count();
        void Clear();
    }
}

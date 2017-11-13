namespace Roguelike
{
    using System;

    internal class EventLoop
    {
        public event Action OnStart;

        public event Action<int, int> OnMove;

        public event Action OnDefault;

        public void Run()
        {
            this.OnStart?.Invoke();

            var canceled = false;
            Console.CancelKeyPress += (o, e) => canceled = true;

            while (!canceled)
            {
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        this.OnMove?.Invoke(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        this.OnMove?.Invoke(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        this.OnMove?.Invoke(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        this.OnMove?.Invoke(1, 0);
                        break;
                    default:
                        this.OnDefault?.Invoke();
                        break;
                }
            }
        }
    }
}
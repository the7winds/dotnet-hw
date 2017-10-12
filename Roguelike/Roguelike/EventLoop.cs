namespace Roguelike
{
    using System;
    using System.Collections.Generic;

    internal class EventLoop
    {
        public event Action OnStart;
        public event Action OnLeft;
        public event Action OnRight;
        public event Action OnUp;
        public event Action OnDown;

        public void Run()
        {
            var handlers = new Dictionary<ConsoleKey, Action>
            {
                { ConsoleKey.UpArrow, OnUp },
                { ConsoleKey.DownArrow, OnDown },
                { ConsoleKey.LeftArrow, OnLeft },
                { ConsoleKey.RightArrow, OnRight }
            };

            OnStart.Invoke();

            while (true)
            {
                var key = Console.ReadKey().Key;

                if (handlers.ContainsKey(key))
                {
                    handlers[key].Invoke();
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }
    }
}
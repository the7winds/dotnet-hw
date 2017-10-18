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

        public event Action OnDefault;

        public void Run()
        {
            var handlers = new Dictionary<ConsoleKey, Action>
            {
                { ConsoleKey.UpArrow, this.OnUp },
                { ConsoleKey.DownArrow, this.OnDown },
                { ConsoleKey.LeftArrow, this.OnLeft },
                { ConsoleKey.RightArrow, this.OnRight }
            };

            this.OnStart.Invoke();

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
                else
                {
                    this.OnDefault.Invoke();
                }
            }
        }
    }
}
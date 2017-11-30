namespace Multithreading.Philosophers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class Fork
    {
        public readonly int Id;

        public Fork(int id)
        {
            Id = id;
        }
    }

    public class Philosopher
    {
        private int _id;
        private Fork _left;
        private Fork _right;
        private bool _leftFirst;
        private int _dinnerCounter;

        public Philosopher(int id, Fork left, Fork right, bool leftFirst)
        {
            _id = id;
            _left = left;
            _right = right;
            _leftFirst = leftFirst;
        }

        public void Live()
        {
            while (true)
            {
                Console.WriteLine($"Philosopher #{_id} is hungry!");

                Fork first = _leftFirst ? _left : _right;
                Fork second = _leftFirst ? _right : _left;

                lock (first)
                {
                    Console.WriteLine($"Philosopher #{_id} has taken fork #{first.Id}");

                    lock (second)
                    {
                        Console.WriteLine($"Philosopher #{_id} has taken fork #{first.Id}");
                        Console.WriteLine($"Philosopher #{_id} is having dinner");
                        Thread.Sleep(1000);
                    }

                    Console.WriteLine($"Philosopher #{_id} has put fork #{second.Id}");
                }
                Console.WriteLine($"Philosopher #{_id} has put fork #{first.Id}");

                _dinnerCounter++;

                Console.WriteLine($"Philosopher #{_id} is thinking");
                Thread.Sleep(1000);
            }
        }

        public int GetDinnerCounter() => _dinnerCounter;
    }

    public class DinningRoom
    {
        private Fork[] _forks;

        private Philosopher[] _philosophers;

        public DinningRoom(int n)
        {
            _forks = new Fork[n];
            _philosophers = new Philosopher[n];

            for (var i = 0; i < n; i++)
            {
                _forks[i] = new Fork(i);
            }

            for (var i = 0; i < n - 1; i++)
            {
                _philosophers[i] = new Philosopher(i, _forks[i], _forks[i + 1], true);
            }

            _philosophers[n - 1] = new Philosopher(n - 1, _forks[n - 1], _forks[0], false);
        }

        public void Run(int timeout)
        {
            var threads = new LinkedList<Thread>();

            foreach (var philosopher in _philosophers)
            {
                var thread = new Thread(() => philosopher.Live());
                threads.AddLast(thread);
                thread.Start();
            }

            Thread.Sleep(timeout);

            foreach (var thread in threads)
            {
                thread.Interrupt();
                thread.Join();
            }
        }

        public int[] GetDinnerCounters() => _philosophers.Select(p => p.GetDinnerCounter()).ToArray();
    }
}

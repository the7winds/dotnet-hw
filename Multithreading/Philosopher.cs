namespace Multithreading.Philosophers
{
    using System;
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
        private Fork _first;
        private Fork _second;

        public Philosopher(int id, Fork first, Fork second)
        {
            _id = id;
            _first = first;
            _second = second;
        }

        public void Live()
        {
            while (true)
            {
                Console.WriteLine($"Philosopher #{_id} is hungry!");

                lock (_first)
                {
                    Console.WriteLine($"Philosopher #{_id} has taken fork #{_first.Id}");

                    lock (_second)
                    {
                        Console.WriteLine($"Philosopher #{_id} has taken fork #{_second.Id}");
                        Console.WriteLine($"Philosopher #{_id} is having dinner");
                        Thread.Sleep(1000);
                    }

                    Console.WriteLine($"Philosopher #{_id} has put fork #{_second.Id}");
                }
                Console.WriteLine($"Philosopher #{_id} has put fork #{_first.Id}");

                Console.WriteLine($"Philosopher #{_id} is thinking");
                Thread.Sleep(1000);
            }
        }
    }

    public class DinningRoom
    {
        private Fork[] _forks;

        public Philosopher[] Philosophers;

        public DinningRoom(int n)
        {
            _forks = new Fork[n];
            Philosophers = new Philosopher[n];

            for (var i = 0; i < n; i++) {
                _forks[i] = new Fork(i);
            }

            for (var i = 0; i < n-1; i++)
            {
                Philosophers[i] = new Philosopher(i, _forks[i], _forks[i + 1]);
            }

            Philosophers[n - 1] = new Philosopher(n - 1, _forks[0], _forks[n - 1]);
        }
    }
}

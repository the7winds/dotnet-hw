namespace PrimeNumbers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public enum STATE
    {
        WAITING,
        RUNNING,
        FINISHED,
        CANCELED
    }

    public class TaskPrime
    {
        private STATE _state;
        public event Action StateChanged;
        public STATE State
        {
            get => _state;
            set
            {
                _state = value;
                StateChanged?.Invoke();
            }
        }

        private float _progress;
        public event Action ProgressChanged;
        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                ProgressChanged?.Invoke();
            }
        }

        public readonly int Bound;

        public int Counter { get; set; }

        private CancellationTokenSource _cancellationTokenSource;

        public TaskPrime(int bound)
        {
            State = STATE.WAITING;
            Bound = bound;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            State = STATE.CANCELED;
        }

        public Task<bool> IsPrime(int n) => Task.Run(() =>
        {
            for (int i = 2; i * i <= n; ++i)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        });

        public async void Count()
        {
            try
            {
                State = STATE.RUNNING;

                for (int i = 2; i < Bound; ++i)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    if (await IsPrime(i))
                    {
                        Counter++;
                    }

                    Progress = (float)100 * i / Bound;
                }

                State = STATE.FINISHED;
            }
            catch (OperationCanceledException)
            {
                State = STATE.CANCELED;
            }
        }
    }
}

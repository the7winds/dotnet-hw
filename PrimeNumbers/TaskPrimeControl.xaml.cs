namespace PrimeNumbers
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskPrimeControl : UserControl, INotifyPropertyChanged
    {
        public TaskPrimeControl()
        {
            InitializeComponent();
        }

        private TaskPrime _taskPrime;
        public TaskPrime TaskPrime
        {
            get => _taskPrime;
            set
            {
                _taskPrime = value;
                _taskPrime.ProgressChanged += () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
                _taskPrime.StateChanged += () => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StateOrResult"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string StateOrResult
        {
            get
            {
                switch (TaskPrime.State)
                {
                    case STATE.WAITING:
                        return "waiting thread";
                    case STATE.RUNNING:
                        return "running";
                    case STATE.FINISHED:
                        Cancel.IsEnabled = false;
                        return $"{TaskPrime.Counter}";
                    case STATE.CANCELED:
                        return "canceld";
                    default:
                        return "impossible state";
                }
            }
        }

        public float Progress => TaskPrime.Progress;

        public string TaskBound => TaskPrime.Bound.ToString();

        public void CancelClick(object sender, RoutedEventArgs e)
        {
            Cancel.IsEnabled = false;
            TaskPrime?.Cancel();
        }
    }
}

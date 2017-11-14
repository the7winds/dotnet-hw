namespace PrimeNumbers
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputText.Text, out int bound))
            {
                var taskControl = new TaskPrimeControl();
                taskControl.DataContext = taskControl;
                taskControl.TaskPrime = new TaskPrime(bound);
                taskControl.TaskPrime.Count();
                TasksList.Items.Add(taskControl);
            }
        }
    }
}

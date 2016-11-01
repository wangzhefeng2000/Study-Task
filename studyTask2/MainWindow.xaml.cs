using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace studyTask2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new ViewModel();
            DataContext = _viewModel;
        }

        private CancellationTokenSource cts = new CancellationTokenSource();
        private void StartClick(object sender, RoutedEventArgs e)
        {
            StartTask();
        }

        private void EndClick(object sender, RoutedEventArgs e)
        {
            EndTask();
        }

        async void StartTask()
        {
            StartBtn.IsEnabled = false;
            EndBtn.IsEnabled = true;
           
            

             var progress = new Progress<ViewModel>();
            progress.ProgressChanged += (s, e) =>
            {
                _viewModel.Step = e.Step ;
                _viewModel.Message = e.Message;
            };
            await MyTaskAsync(progress, cts);

            StartBtn.IsEnabled = true;
            EndBtn.IsEnabled = false;
        }

        private static async Task MyTaskAsync(IProgress<ViewModel> progress, CancellationTokenSource cts)
        {
            Func<Task> taskFunc = () => Task.Run(() =>
            {
                for (var i = 1; i <= 500 && !cts.IsCancellationRequested; i++)
                {
                    Thread.Sleep( i);
                    progress.Report(new ViewModel { Step = i / 5, Message = "Message " + i });
                }
            });

            await taskFunc();
        }


        private void EndTask()
        {
            cts.Cancel();
            MessageBox.Show("Canceled!");
        }
    }
}

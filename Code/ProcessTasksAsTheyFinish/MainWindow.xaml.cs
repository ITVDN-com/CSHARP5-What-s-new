using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Net.Http;


using System.Threading;


namespace ProcessTasksAsTheyFinish
{
    public partial class MainWindow
    {
        CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        async void StartButtonClick(object sender, RoutedEventArgs e)
        {
            resultsTextBox.Clear();

            cts = new CancellationTokenSource();

            try
            {
                await AccessTheWebAsync(cts.Token);
                resultsTextBox.Text += "\r\nDownloads complete.";
            }
            catch (OperationCanceledException)
            {
                resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\nDownloads failed.\r\n";
            }

            cts = null;
        }

        void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        async Task AccessTheWebAsync(CancellationToken ct)
        {
            var client = new HttpClient();

            IEnumerable<string> urlList = SetUpURLList();

            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURL(url, client, ct);

            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            while (downloadTasks.Count > 0)
            {
                    Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

                    downloadTasks.Remove(firstFinishedTask);

                    int length = firstFinishedTask.Result;

                    resultsTextBox.Text += String.Format("\r\nLength of the download:  {0}", length);
            }
        }

        private IEnumerable<string> SetUpURLList()
        {
            var urls = new List<string> 
            { 
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290136.aspx",
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx"
            };
            return urls;
        }

        async Task<int> ProcessURL(string url, HttpClient client, CancellationToken ct)
        {
            HttpResponseMessage response = await client.GetAsync(url, ct);

            
         
            //var taskDebug = task.ContinueWith((task1) =>  Debug.WriteLine("URL:{0}, Thread:{1}",url,Thread.CurrentThread.ManagedThreadId));
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();
           // Debug.WriteLine("URL:{0}, Thread:{1}",url,Thread.CurrentThread.ManagedThreadId);

            return urlContents.Length;
        }
    }
}
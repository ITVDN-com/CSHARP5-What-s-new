using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;


namespace AsyncWalkthrough_HttpClient
{
    public class GetAllPingsPartialResult
    {
        public IList<string> Pings;
        public int Count;
    }

    public partial class MainWindow
    {
        // ***Declare a System.Threading.CancellationTokenSource.
        CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartButtonClick(object sender, RoutedEventArgs e)
        {
            resultsTextBox.Clear();

            cts = new CancellationTokenSource();
            // cts.CancelAfter(5000);
            try
            {
                Task sumTask = SumPageSizesAsync(cts.Token);

                //Do some other stuff...
                //....

                //Wait until sumTask is Finished...
                await sumTask;
            }
            catch (OperationCanceledException)
            {
                resultsTextBox.Text += "\r\nDownload canceled.\r\n";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\nDownload failed.\r\n";
            }

            resultsTextBox.Text += "\r\nControl returned to startButton_Click.\r\n";
        }


        private async Task SumPageSizesAsync(CancellationToken ct, IProgress<GetAllPingsPartialResult> progress)
        {
            // Declare an HttpClient object.
            var client = new HttpClient();

            // Make a list of web addresses.
            IEnumerable<string> urlList = SetUpURLList();

            var total = 0;

            foreach (var url in urlList)
            {
                // GetByteArrayAsync returns a task. At completion, the task
                // produces a byte array.
                HttpResponseMessage response = await client.GetAsync(url, ct);
                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();
                        if (progress != null) progress.Report(new GetAllPingsPartialResult() { Pings = new ReadOnlyCollection<string>(results), Count = results.Count });
                DisplayResults(url, urlContents);

                // Update the total.
                total += urlContents.Length;
            }

            // Display the total count for all of the websites.
            resultsTextBox.Text +=
                string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }


        private IEnumerable<string> SetUpURLList()
        {
            var urls = new List<string> 
            { 
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.com",
                "http://msdn.microsoft.com/en-us/library/hh290136.aspx",
                "http://msdn.microsoft.com/en-us/library/ee256749.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290138.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290140.aspx",
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx"
            };
            return urls;
        }


        private void DisplayResults(string url, byte[] content)
        {
            // Display the length of each website. The string format 
            // is designed to be used with a monospaced font, such as
            // Lucida Console or Global Monospace.
            var bytes = content.Length;
            // Strip off the "http://".
            var displayURL = url.Replace("http://", "");
            resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }
    }
}

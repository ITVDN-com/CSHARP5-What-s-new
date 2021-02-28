using System.Windows;
using System.Net;
using System.Threading;

namespace AsyncFetch
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetButtonClick(object sender, RoutedEventArgs e)
        {
            dataTextBox.Text += "Beginning download\n";
            var sync = SynchronizationContext.Current;
            var req = (HttpWebRequest) WebRequest.Create("http://www.microsoft.com");
            req.Method = "GET";
            req.BeginGetResponse(
                asyncResult =>
                {
                    var resp = (HttpWebResponse) req.EndGetResponse(asyncResult);
                    string headersText = resp.Headers.ToString();
                    sync.Post(
                        delegate
                        {
                            dataTextBox.Text += headersText;
                        },
                        null);
                },
                null);
            dataTextBox.Text += "Download started async\n";
        }
      }
}

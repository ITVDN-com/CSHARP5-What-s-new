using System.Windows;
using System.Net;

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
            var req = (HttpWebRequest)WebRequest.Create("http://www.google.com");
            req.Method = "GET";
            req.BeginGetResponse(
                asyncResult =>
                {
                    var resp = (HttpWebResponse)req.EndGetResponse(asyncResult);
                    string headersText = resp.Headers.ToString();
                    dataTextBox.Text += headersText;
                },
                null);
            dataTextBox.Text += "Download started async\n";
        }
    }
}

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
            dataTextBox.Text = "Starting sync download\n";

            var req = (HttpWebRequest) WebRequest.Create("http://www.microsoft.com/");
            req.Timeout = 2000;
            req.Method = "GET";

            try
            {
                var resp = (HttpWebResponse) req.GetResponse();
                dataTextBox.Text += "Sync completed\n";
                string headersText = resp.Headers.ToString();
                dataTextBox.Text += headersText;
            }
            catch (WebException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}

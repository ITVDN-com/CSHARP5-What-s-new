using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Net;

namespace SyncWalkthrough
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            resultsTextBox.Clear();
            SumPageSizes();
            resultsTextBox.Text += "\r\nControl returned to startButton_Click.";
        }

        private void SumPageSizes()
        {
            IEnumerable<string> urlList = SetUpURLList();

            var total = 0;
            foreach (var url in urlList)
            {
                byte[] urlContents = GetURLContents(url);
                DisplayResults(url, urlContents);

                total += urlContents.Length;
            }

            resultsTextBox.Text +=
                string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }


        private IEnumerable<string> SetUpURLList()
        {
            var urls = new List<string> 
            { 
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com",
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


        private byte[] GetURLContents(string url)
        {

            var content = new MemoryStream();

            var webReq = (HttpWebRequest)WebRequest.Create(url);

            using (var response = webReq.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        responseStream.CopyTo(content);
                    }
                }

            return content.ToArray();
        }


        private void DisplayResults(string url, byte[] content)
        {
            var bytes = content.Length;
            var displayURL = url.Replace("http://", "");
            resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);
        }
    }
}

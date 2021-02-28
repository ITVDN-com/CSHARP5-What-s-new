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

        async void GetButtonClick(object sender, RoutedEventArgs e)
        {
            using (var w = new WebClient())
            {
                string txt = await w.DownloadStringTaskAsync("http://www.microsoft.com/");
                dataTextBox.Text += txt;
            }
        }

        void DoDownload()
        {
            using (var w = new WebClient())
            {
                string txt = w.DownloadString("http://www.micros1oft.com/");
                dataTextBox.Text += txt;
            }
        }
    }
}

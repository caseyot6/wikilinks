using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Net.Http;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly HttpClient client = new HttpClient();
        private string wikiURL = "http://en.wikipedia.org/w/api.php?action=query&list=search&srsearch=";
        private string urlEnd = "&format=json";
        public MainWindow()
        {
            InitializeComponent();
        }


        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Current.Text = Search.Text;
                Search.Text = "";
                Links.Text = "";

                SearchWikipedia(Search.Text);
            }
            

            

        }

        private async void SearchWikipedia(String title)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(wikiURL+title+urlEnd);
                Console.WriteLine("wikiURL + title + urlEnd");
                response.EnsureSuccessStatusCode();

                Links.Text = await response.Content.ReadAsStringAsync();
               
                

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("Problem Encountered");
            }


        }
    }

    


}

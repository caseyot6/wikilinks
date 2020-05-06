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
using System.Text.Json;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly HttpClient client = new HttpClient();
        private string wikiURL = "http://en.wikipedia.org/w/api.php?action=query&titles=";
        private string urlEnd = "&prop=links&pllimit=max&format=json";
        public MainWindow()
        {
            InitializeComponent();
        }


        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
                SearchWikipedia(Search.Text);
            }
            

            

        }

        private async void SearchWikipedia(String title)
        {
            try
            {
                Current.Text = title;
                Search.Text = "";

                HttpResponseMessage response = await client.GetAsync(wikiURL+title+urlEnd);
                response.EnsureSuccessStatusCode();

                string a = await response.Content.ReadAsStringAsync();
                JsonDocument b = JsonDocument.Parse(a);

                JsonElement pages = b.RootElement.GetProperty("query").GetProperty("pages");


                List<string> wikiTitles = new List<String>();


                int count = 0;



                foreach (JsonProperty k in pages.EnumerateObject())
                {
                    count += 1;
                    

                    JsonElement links = pages.GetProperty(k.Name).GetProperty("links");

                    foreach (JsonElement l in links.EnumerateArray())
                    {

                        
                        wikiTitles.Add(l.GetProperty("title").ToString());

                    }
                    
                    if (count > 0)
                    {
                        
                        break;
                    }

                }

                Links.Items.Clear();

                foreach(string name in wikiTitles)
                {
                    //Links.AppendText(name + "\n");
                    Links.Items.Add(name.ToString());

                }

               

            }
            catch
            {
                Console.WriteLine("Problem Encountered");
            }


        }

        private void Links_MouseClick(object sender, MouseButtonEventArgs e)
        {
            foreach(string i in Links.Items)
            {
                if(Links.Items.GetItemAt(Links.SelectedIndex).ToString().Equals(i))
                {
                    SearchWikipedia(i);
                }
            }
        }
    }

    


}

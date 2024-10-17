using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Travel_Manager
{
    public partial class TravelForm : Form
    {
        private WeatherForm weatherForm = new WeatherForm();
        private List<CityCountryMapping> cityCountryMappings;
        public TravelForm()
        {
            InitializeComponent();
            InitializeCity();
            PopulateDropdown();
        }


        private void InitializeCity()
        {
            cityCountryMappings = new List<CityCountryMapping>
            {
                new CityCountryMapping("New York", "us"),
                new CityCountryMapping("Los Angeles", "us"),
                new CityCountryMapping("Chicago", "us"),
                new CityCountryMapping("Houston", "us"),
                new CityCountryMapping("Paris", "france"),
                new CityCountryMapping("Lyon", "france"),
                new CityCountryMapping("Marseille", "france"),
                new CityCountryMapping("London", "great_britan"),
                new CityCountryMapping("Manchester", "great_britan"),
                new CityCountryMapping("Birmingham", "great_britan"),
                new CityCountryMapping("Tokyo", "japan"),
                new CityCountryMapping("Osaka", "japan"),
                new CityCountryMapping("Kyoto", "japan"),
                new CityCountryMapping("Sydney", "australlia"),
                new CityCountryMapping("Melbourne", "australlia"),
                new CityCountryMapping("Brisbane", "australlia"),
                new CityCountryMapping("Toronto", "canada"),
                new CityCountryMapping("Vancouver", "canada"),
                new CityCountryMapping("Montreal", "canada"),
                new CityCountryMapping("Berlin", "germany"),
                new CityCountryMapping("Munich", "germany"),
                new CityCountryMapping("Frankfurt", "germany"),
                new CityCountryMapping("Madrid", "spain"),
                new CityCountryMapping("Barcelona", "spain"),
                new CityCountryMapping("Valencia", "spain"),
                new CityCountryMapping("Rome", "italy"),
                new CityCountryMapping("Milan", "italy"),
                new CityCountryMapping("Florence", "italy"),
                new CityCountryMapping("Beijing", "china"),
                new CityCountryMapping("Shanghai", "china"),
                new CityCountryMapping("Hong Kong", "hong_kong"),
                new CityCountryMapping("Rio de Janeiro", "brazil"),
                new CityCountryMapping("São Paulo", "brazil"),
                new CityCountryMapping("Buenos Aires", "argentina"),
                new CityCountryMapping("Santiago", "chile"),
                new CityCountryMapping("Lima", "peru"),
                new CityCountryMapping("Moscow", "russia"),
                new CityCountryMapping("Saint Petersburg", "russia"),
                new CityCountryMapping("Istanbul", "türkiye"),
                new CityCountryMapping("Cairo", "south_africa"),
                new CityCountryMapping("Cape Town", "south_africa"),
                new CityCountryMapping("Johannesburg", "south_africa"),
                new CityCountryMapping("New Delhi", "india"),
                new CityCountryMapping("Mumbai", "india"),
                new CityCountryMapping("Bangkok", "thailand"),
                new CityCountryMapping("Seoul", "korea"),
                new CityCountryMapping("Taipei", "taiwan"),
                new CityCountryMapping("Kuala Lumpur", "malaysia"),
                new CityCountryMapping("Singapore", "singapore"),
            };
        }

        private void PopulateDropdown()
        {
            comboBoxCity.DataSource = cityCountryMappings;
        }

        private async void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gather News Result
            var selectedCity = comboBoxCity.SelectedItem as CityCountryMapping;

            if (selectedCity == null)
            {
                MessageBox.Show("Please select a city.");
                return;
            }

            string country = selectedCity.CountryCode;

            // Gather news articles
            var news = new News();
            List<Article> articles = await news.NewsResult(country);

            newscontainerpanel.Controls.Clear();
            if (articles == null || articles.Count == 0)
            {
                Label noArticlesLabel = new Label
                {
                    Text = "No news articles available for this country.",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                noArticlesLabel.Dock = DockStyle.Fill;
                newscontainerpanel.Controls.Add(noArticlesLabel);
                return;
            }

            // Display the articles
            for (int i = 0; i < (trackBar1.Value); i++)
            {
                var article = articles[i];
                Panel newsPanel = CreateNewsPanel(article);
                newsPanel.Dock = DockStyle.Top;
                newscontainerpanel.Controls.Add(newsPanel);
                newsPanel.BringToFront();
            }

            // Update weather information when a city is selected
            await UpdateWeatherInformation();
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        //Local Information Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (!infoSubMenu.Visible)
            {
                infoSubMenu.Visible = true;
            } else
            {
                infoSubMenu.Visible = false;
            }
        }

        private Panel CreateNewsPanel(Article article)
        {
            Panel panel = new Panel();
            panel.Size = new Size(newscontainerpanel.Width, 484);
            if (!string.IsNullOrEmpty(article.urlToImage))
            {
                panel.BackgroundImage = Image.FromStream(new System.Net.WebClient().OpenRead(article.urlToImage));
                panel.BackgroundImageLayout = ImageLayout.Stretch;
            }

            Panel overlayPanel = new Panel
            {
                BackColor = Color.FromArgb(128, 0, 0, 0),
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(overlayPanel);

            Label titleLabel = new Label
            {
                Text = article.title,
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                MaximumSize = new Size(panel.Width - 20, 0)
            };
            titleLabel.Location = new Point(10, 10);
            overlayPanel.Controls.Add(titleLabel);

            Label descriptionLabel = new Label
            {
                Text = article.description,
                Font = new Font("Arial", 15),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true,
                MaximumSize = new Size(panel.Width - 20, 0)
            };
            descriptionLabel.Location = new Point(10, titleLabel.Bottom + 5);
            overlayPanel.Controls.Add(descriptionLabel);

            // Add a LinkLabel for the article URL
            if (!string.IsNullOrEmpty(article.url))
            {
                LinkLabel urlLabel = new LinkLabel
                {
                    Text = "Read more",
                    Font = new Font("Arial", 14),
                    LinkColor = Color.LightBlue,
                    AutoSize = true,
                    BackColor = Color.Transparent
                };
                urlLabel.Location = new Point(10, descriptionLabel.Bottom + 10);
                urlLabel.Links.Add(0, urlLabel.Text.Length, article.url);
                urlLabel.LinkClicked += (sender, e) =>
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = e.Link.LinkData.ToString(),
                        UseShellExecute = true
                    });
                };
                overlayPanel.Controls.Add(urlLabel);
            }

            return panel;
        }


        private async Task UpdateWeatherInformation()
        {
            var citySelection = comboBoxCity.SelectedItem as CityCountryMapping;
            if (citySelection == null)
            {
                MessageBox.Show("Please select a city.");
                return;
            }

            string city = citySelection.CityName;
            var weather = new Weather();
            var wR = await weather.GetCurrentWeather(city);

            if (weatherForm.IsDisposed)
            {
                weatherForm = new WeatherForm();
                weatherForm.TopLevel = false;
                weatherForm.FormBorderStyle = FormBorderStyle.None;
                weatherForm.Dock = DockStyle.Fill;
            }
            weatherForm.UpdateWeatherInfo(
                wR.location.name,
                Math.Round(wR.current.temp_c, 1),
                wR.current.condition.text,
                Math.Round(wR.current.wind_kph, 1),
                wR.current.humidity
            );

            weatherForm.Show();
            weatherForm.BringToFront();
        }

        //Weather Button
        private async void button4_Click(object sender, EventArgs e)
        {
            if (weatherForm.IsDisposed)
            {
                weatherForm = new WeatherForm();
            }
            await UpdateWeatherInformation();
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value <= 1)
            {
                label2.Text = "1 Article";
            } else
            {
                label2.Text = $"{trackBar1.Value} Articles";
            }
            
        }

        //News Button
        private void button3_Click(object sender, EventArgs e)
        {
            
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void newscontainerpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void weatherPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}


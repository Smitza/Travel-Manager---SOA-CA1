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
        private List<CityCountryMapping> cityCountryMappings;
        public TravelForm()
        {
            InitializeComponent();
            InitializeCity();
            PopulateDropdown();
            newscontainerpanel.Show();
        }


        private void InitializeCity()
        {
            cityCountryMappings = new List<CityCountryMapping>
            {
                new CityCountryMapping("New York", "us"),
                new CityCountryMapping("Paris", "fr"),
                new CityCountryMapping("London", "gb"),
                new CityCountryMapping("Tokyo", "jp"),
                new CityCountryMapping("Sydney", "au")
            };
        }

        private void PopulateDropdown()
        {
            comboBoxCity.DataSource = cityCountryMappings;
        }

        private async void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.Refresh();

            var selectedCity = comboBoxCity.SelectedItem as CityCountryMapping;

            if (selectedCity == null)
            {
                MessageBox.Show("Please select a city.");
                return;
            }

            string country = selectedCity.CountryCode;
            string city = selectedCity.CityName;

            // Gather News Result
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

            for (int i = 0; i < 4; i++)
            {
                var article = articles[i];
                Panel newsPanel = CreateNewsPanel(article);
                newsPanel.Dock = DockStyle.Top;
                newscontainerpanel.Controls.Add(newsPanel);
                newsPanel.BringToFront();
            }

            // Gather Weather Result
            var weather = new Weather();
            List<string> weatherDetails = await weather.GetCurrentWeather(city);
            dataGridView2.DataSource = weatherDetails
                .Select(detail => new { WeatherDetail = detail })
                .ToList();
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        //Local Information Button
        private void button2_Click(object sender, EventArgs e)
        {
            if(!infoSubMenu.Visible)
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

            return panel;
        }

        //Weather Button
        private void button4_Click(object sender, EventArgs e)
        {
            if (!weatherPanel.Visible)
            {
                newscontainerpanel.Hide();
                newscontainerpanel.SendToBack();
                weatherPanel.Show();
                weatherPanel.BringToFront();
            }
        }

        //News Button
        private void button3_Click(object sender, EventArgs e)
        {
            if (!newscontainerpanel.Visible)
            {
                newscontainerpanel.Show();
                newscontainerpanel.BringToFront();
                weatherPanel.Hide();
                weatherPanel.SendToBack();
            }
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
    }
}

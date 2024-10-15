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


        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
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
            dataGridView1.DataSource = articles;
            textBox1.Text = news.GetNewsApiUrl(country);

            // Gather Weather Result
            var weather = new Weather();
            List<string> weatherDetails = await weather.GetCurrentWeather(city);
            dataGridView2.DataSource = weatherDetails
                .Select(detail => new { WeatherDetail = detail })
                .ToList();

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

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

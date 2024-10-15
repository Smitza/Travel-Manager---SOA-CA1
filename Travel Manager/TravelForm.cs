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
        public TravelForm()
        {
            InitializeComponent();
            dataGridView2.AutoGenerateColumns = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private async void button1_Click(object sender, EventArgs e)
        {
            // Gather News Result
            string country = textBox1.Text;
            var news = new News();
            List<Article> articles = await news.NewsResult(country);
            dataGridView1.DataSource = articles;

            // Gather Weather Result
            var weather = new Weather();
            List<Weather.WeatherForcast> weatherForcasts = await weather.GetCurrentWeather(country);

            //Flatten the Weather Data
            var flattenedForecast = weatherForcasts.Select(w => new
            {
                LocationName = w.location.name,
                Region = w.location.region,
                Country = w.location.country,
                TemperatureC = w.current.temp_c,
                TemperatureF = w.current.temp_f,
                Condition = w.current.condition.text,
                WindSpeedKph = w.current.wind_kph,
                Humidity = w.current.humidity,
                FeelsLikeC = w.current.feelslike_c,
                FeelsLikeF = w.current.feelslike_f
            }).ToList();

            // Assign the weather forecasts list to dataGridView2
            dataGridView2.DataSource = flattenedForecast;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
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
    }
}

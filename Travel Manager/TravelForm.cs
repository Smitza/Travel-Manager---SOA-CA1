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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private async void button1_Click(object sender, EventArgs e)
        {

            //Gather News Result
            string country = textBox1.Text;
            var news = new News();
            List<Article> articles = await news.NewsResult(country);

            dataGridView1.DataSource = articles;

            //Gather Weather Result
            Weather weatherService = new Weather();
            WeatherResponse weather = await weatherService.GetCurrentWeather(country);

            dataGridView2.DataSource = weather;
        }

        private  void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

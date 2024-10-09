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
            string country = textBox1.Text;
            var news = new News();
            List<Article> articles = await news.NewsResult(country);

            dataGridView1.DataSource = articles;
        }

        private  void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

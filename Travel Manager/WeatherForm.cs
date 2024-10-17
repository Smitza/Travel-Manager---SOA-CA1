using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Travel_Manager
{
    public partial class WeatherForm : Form
    {
        public WeatherForm()
        {
            InitializeComponent();
            this.FormClosing += closedForm;
        }

        private void closedForm(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the closing event
            this.Hide(); // Hide the form instead
        }

        public void UpdateWeatherInfo(string location, double temp, string condition, double windSpeed, int humidity)
        {
            label1.Text = location;
            label2.Text = $"{temp}°C";
            label3.Text = $"Condition: {condition}";
            label4.Text = $"Wind Speed: {windSpeed} KPH";
            label5.Text = $"Humidity: {humidity}%";
        }

        private void WeatherForm_Load(object sender, EventArgs e)
        {

        }

        private void weatherIcon_Click(object sender, EventArgs e)
        {

        }
    }
}

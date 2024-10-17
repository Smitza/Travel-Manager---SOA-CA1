using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            label1.Parent = panel1;
            label1.BackColor = Color.Transparent;
            label3.Parent = panel1;
            label3.BackColor = Color.Transparent;
            label4.Parent = panel1;
            label4.BackColor = Color.Transparent;
            label5.Parent = panel1;
            label5.BackColor = Color.Transparent;

        }

        public void updateWeatherIcon(string condition)
        {
            string path = @"C:\Users\barry\Documents\MusicBot\MusicBot\Travel-Manager---SOA-CA1\Travel Manager\Media\WeatherIcons";
            string iconFile = "sun.png";
            string bgFile = "sunbg.png";

            switch (condition.ToLower())
            {
                case "sunny":
                    iconFile = "sun.png";
                    bgFile = "sunbg.png";
                    break;
                case "partly cloudy":
                case "patchy light rain with thunder":
                    iconFile = "rainsun.png";
                    bgFile = "rainbg.png";
                    break;
                case "cloudy":
                case "overcast":
                    iconFile = "clouds.png";
                    break;
                case "mist":
                case "fog":
                case "freezing fog":
                case "patchy light drizzle":
                case "light drizzle":
                case "freezing drizzle":
                case "heavy freezing drizzle":
                    iconFile = "rainwind.png";
                    bgFile = "rainbg.png";
                    break;
                case "patchy rain possible":
                case "light rain":
                case "moderate rain at times":
                case "moderate rain":
                case "light rain shower":
                    iconFile = "rain.png";
                    bgFile = "rainbg.png";
                    break;
                case "heavy rain at times":
                case "heavy rain":
                case "moderate or heavy rain shower":
                case "torrential rain shower":
                    iconFile = "heavyrain.png";
                    bgFile = "rainbg.png";
                    break;
                case "light freezing rain":
                case "moderate or heavy freezing rain":
                case "light sleet":
                case "moderate or heavy sleet":
                case "light sleet showers":
                case "moderate or heavy sleet showers":
                    iconFile = "snowcloud.png";
                    bgFile = "rainbg.png";
                    break;
                case "patchy light snow":
                case "light snow":
                case "patchy moderate snow":
                case "moderate snow":
                case "light snow showers":
                case "moderate or heavy snow showers":
                case "patchy heavy snow":
                case "heavy snow":
                case "blowing snow":
                case "blizzard":
                    iconFile = "snow.png";
                    bgFile = "rainbg.png";
                    break;
                default:
                    iconFile = "sun.png";
                    bgFile = "sunbg.png";
                    break;
            }

            string bgPath = Path.Combine(path, bgFile);
            string iconPath = Path.Combine(path, iconFile);
            if (File.Exists(iconPath))
            {
                weatherIcon.ImageLocation = iconPath;
                panel1.BackgroundImage = Image.FromFile(bgPath);
            }
            else
            {
                weatherIcon.Hide();
                Console.WriteLine("Weather Images didn't load! " + iconPath);
            }
        }

        private void closedForm(object sender, FormClosingEventArgs e)
        {
            //When X button is pressed, instead of completly removing the form, it is instead hidden.
            e.Cancel = true;
            this.Hide();
        }

        public void UpdateWeatherInfo(string location, double temp, string condition, double windSpeed, int humidity)
        {
            label1.Text = location;
            label2.Text = $"{temp}°C";
            label3.Text = $"Condition: {condition}";
            label4.Text = $"Wind Speed: {windSpeed} KPH";
            label5.Text = $"Humidity: {humidity}%";
            updateWeatherIcon(condition);
        }

        private void WeatherForm_Load(object sender, EventArgs e)
        {

        }

        private void weatherIcon_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
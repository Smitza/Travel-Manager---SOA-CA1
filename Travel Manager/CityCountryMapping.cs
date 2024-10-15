using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Manager
{
    //Since the NewsApi and WeatherApi require different parameters, this class binds both the city and country together. 
    public class CityCountryMapping
    {
        public string CityName { get; set; }
        public string CountryCode { get; set; }

        public CityCountryMapping(string cityName, string countryCode)
        {
            CityName = cityName;
            CountryCode = countryCode;
        }
        public override string ToString()
        {
            return CityName;  // To display city names in the ComboBox
        }
    }
}

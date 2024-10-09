using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Travel_Manager
{
    public class News
    {
        static readonly string NEWS_URL = "https://newsapi.org/v2/top-headlines?";
        //Remove later
        static string API_KEY = "e82c5c5f920247d8b31b3eb9f5279e8f";

        var client = new RestClient(NEWS_URL);
    }
}

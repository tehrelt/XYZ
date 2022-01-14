using System;
using System.Net;
using System.Net.Http;

namespace XYZConsole
{
    internal class Program
    {
        private const string DATA_URL = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main(string[] args)
        {
            var client = new HttpClient();
            var response = client.GetAsync(DATA_URL).Result;
            var contentCSV = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }
    }
}

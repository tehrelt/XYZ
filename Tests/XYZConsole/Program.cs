using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace XYZConsole
{
    internal class Program
    {
        private const string DATA_URL = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private const string SIRUS_URL = @"https://sirus.su/statistic/online/#/";
        static void Main(string[] args)
        {
            //var client = new HttpClient();
            //var response = client.GetAsync(DATA_URL).Result;
            //var contentCSV = response.Content.ReadAsStringAsync().Result;

            //foreach (var dataLine in GetDataLines())
            //{
            //    Console.WriteLine(dataLine);
            //}

            //var dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var russiaData = GetData()
                .First(e => e.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine
            (
                string.Join
                (
                    "\r\n",
                    GetDates().Zip(russiaData.Counts, (date, count) => $"{date:dd.mm.yyyy} - {count}")
                )
            );

            Console.ReadLine();
        }
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(DATA_URL, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        //private static async Task<Stream> GetDataStream()
        //{
        //    var client = new HttpClient { Timeout = new TimeSpan(0, 0, 30)};
        //    try
        //    {
        //        var response = await client.GetAsync(SIRUS_URL, HttpCompletionOption.ResponseHeadersRead);

        //    }
        //    return await response.Content.ReadAsStreamAsync();
        //}

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line
                    .Replace("Korea,", "Korea -")
                    .Replace("Bonaire,", "Bonaire -")
                    .Replace("Saint Helena,", "Saint Helena -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach(var line in lines)
            {
                var province = line[0].Trim();
                var countryName = line[1].Trim(' ', '"');
                var counts = line.Skip(4).Select(int.Parse).ToArray();

                yield return (countryName, province, counts);
            }
        }
    }
}

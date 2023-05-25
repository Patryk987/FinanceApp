using System;
using System.Net;
using Newtonsoft.Json;

namespace FinanceApp.Services
{
    class ConvertCurrencyToPln
    {
        private static dynamic GetApi(string table = "a")
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString($"http://api.nbp.pl/api/exchangerates/tables/{table}/last?format=json");
                dynamic data = JsonConvert.DeserializeObject(json);
                return data[0].rates;
            }
        }

        public static decimal GetConvertPrice(string currency, decimal price)
        {
            dynamic table = ConvertCurrencyToPln.GetApi();
            decimal convertPrice = price;

            foreach (var value in table)
            {

                if (currency.ToString() == value.code.ToString())
                {
                    convertPrice = value.mid * price;
                    break;
                }
            }
            Console.WriteLine(convertPrice);
            return convertPrice;
        }
    }

}
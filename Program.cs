using System.Net;
using System.Text.Json;
namespace PriceBTCSpace
{
    partial class Program
    {
        static void Main()
        {
            GetPrecioBTC();
        }

        private static void GetPrecioBTC()
        {
            var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            string responseBody = objReader.ReadToEnd();
                            Root price = JsonSerializer.Deserialize<Root>(responseBody);

                            string header = @"
╔╗ ┬┌┬┐╔═╗┌─┐┬┌┐┌  ╔═╗┬─┐┬┌─┐┌─┐  ╦┌┐┌┌┬┐┌─┐─┐ ┬
╠╩╗│ │ ║  │ │││││  ╠═╝├┬┘││  ├┤   ║│││ ││├┤ ┌┴┬┘
╚═╝┴ ┴ ╚═╝└─┘┴┘└┘  ╩  ┴└─┴└─┘└─┘  ╩┘└┘─┴┘└─┘┴ └─";
                            Console.WriteLine(header);
                            Console.WriteLine("EUR RATE: {0}", price.bpi.EUR.rate);
                            Console.WriteLine("GBP RATE: {0}", price.bpi.GBP.rate);
                            Console.WriteLine("USD RATE: {0}", price.bpi.USD.rate);
                            Console.WriteLine("====== GBP DETAILS =====");
                            Console.WriteLine("Codigo: {0}", price.bpi.GBP.code);
                            Console.WriteLine("Simbolo: {0}", price.bpi.GBP.symbol);
                            Console.WriteLine("Rate: {0}", price.bpi.GBP.rate);
                            Console.WriteLine("Descripcion: {0}", price.bpi.GBP.description);
                            Console.ResetColor();
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
        }

    }
}


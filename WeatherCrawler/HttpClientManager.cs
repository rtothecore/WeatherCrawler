using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeatherCrawler
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    // https://docs.microsoft.com/ko-kr/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    class HttpClientManager
    {
        //private static readonly HttpClient client = new HttpClient();
        static string URL = null;
        static string UrlParameters = null;

        public HttpClientManager(string url, string urlParameters)
        {
            // http://maps.googleapis.com/maps/api/geocode/json
            URL = url;
            // ?sensor=false&language=ko&address=제주특별자치도%20제주시%20이도이동
            // UrlParameters = HttpUtility.UrlEncode(urlParameters);
            UrlParameters = urlParameters;

            // RunAsync().GetAwaiter().GetResult();

            Task t = new Task(DownloadPageAsync);
            t.Start();
            Console.WriteLine("Downloading page...");
            Console.ReadLine();
        }

        private static async void DownloadPageAsync()
        {
            string page = URL + UrlParameters;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    GoogleMapsResult gmr = await content.ReadAsAsync<GoogleMapsResult>();
                    Console.WriteLine("Address:" + gmr.results[0].formatted_address +
                                      ", LatLng:(" + gmr.results[0].geometry.location.lat + ", " + gmr.results[0].geometry.location.lng + ")");
                }
            }
        }

        /*
        static async Task<Uri> CreateProductAsync(Product product)
        {
            // HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            client.Timeout = new TimeSpan(0, 0, 5);
            HttpResponseMessage response = await client.PostAsJsonAsync(UrlParameters, product).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tCategory: {product.Category}");
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            // client.BaseAddress = new Uri("http://localhost:64195/");
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };

                var url = await CreateProductAsync(product).ConfigureAwait(false);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Price = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        */
    }
}

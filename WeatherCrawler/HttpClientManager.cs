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
        public HttpClientManager(string url, string urlParameters)
        {
            // http://maps.googleapis.com/maps/api/geocode/json
            // ?sensor=false&language=ko&address=제주특별자치도%20제주시%20이도이동            
            Task t = new Task(delegate { GetGPSLocationAsync(url, urlParameters); });   // http://codingcoding.tistory.com/415
            t.Start();
        }

        private static async void GetGPSLocationAsync(string url, string urlParameters)
        {
            string page = url + urlParameters;
            Console.WriteLine(page);

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
    }
}

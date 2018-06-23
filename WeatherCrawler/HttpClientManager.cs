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
    class HttpClientManager
    {
        public HttpClientManager(string url, string urlParameters, string address)
        {
            // http://maps.googleapis.com/maps/api/geocode/json
            // ?sensor=false&language=ko&address=제주특별자치도%20제주시%20이도이동            
            RunTask(url, urlParameters, address);
        }

        private async void RunTask(string url, string urlParameters, string address)
        {
            // https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/concepts/async/
            GoogleMapsResult gmr = await GetGPSLocationAsync(url, urlParameters);

            NxNyManager nnm = new NxNyManager();
            Coords nxny = nnm.Dfs_xy_conv("toXY", gmr.results[0].geometry.location.lat, gmr.results[0].geometry.location.lng);
            Console.WriteLine("Address:" + address +
                              ", LatLng:(" + gmr.results[0].geometry.location.lat + ", " + gmr.results[0].geometry.location.lng + ")" +
                              ", NxNy:(" + nxny.x + ", " + nxny.y + ")"
                              );
            FwjournalIniManager fiManager = new FwjournalIniManager();
            fiManager.WriteAddress(address, nxny.x, nxny.y);
        }

        private static async Task<GoogleMapsResult> GetGPSLocationAsync(string url, string urlParameters)
        {
            string page = url + urlParameters;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    return await content.ReadAsAsync<GoogleMapsResult>();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    static class UtilManager
    {
        static UtilManager()
        {

        }

        static public string TrimAddress(string address)
        {
            string[] splitedAddress = address.Split(' ');
            return splitedAddress[0] + " " + splitedAddress[1] + " " + splitedAddress[2];
        }

        static public string GetMaxAddressIndex(List<string> addressindexes)
        {
            List<int> addressIndexVals = new List<int>();
            for(int i = 0; i < addressindexes.Count; i++)
            {
                string tmpIndex = addressindexes[i].Replace("f", "");
                addressIndexVals.Add(Int32.Parse(tmpIndex));
            }
            int maxAddressVal = addressIndexVals.Max();

            return "f" + (maxAddressVal + 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCrawler
{
    public class Address_components
    {
        public string long_name;
        public string short_name;
        public List<string> types;
    }

    public class LatLng
    {
        public double lat;
        public double lng;
    }

    public class Bounds
    {
        public LatLng northeast;
        public LatLng southwest;
    }

    public class Geometry
    {
        public Bounds bounds;
        public LatLng location;
        public string location_type;
        public Bounds viewport;
    }

    public class Results
    {
        public List<Address_components> address_components;
        public string formatted_address;
        public Geometry geometry;
        public string place_id;
        public List<string> types;
    }

    public class GoogleMapsResult
    {
        public List<Results> results;
        public string status;
    }
}

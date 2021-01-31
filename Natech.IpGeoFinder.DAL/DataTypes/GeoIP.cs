using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.DAL.DataTypes
{
    public class GeoIP
    {
            public string ip { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public string region_code { get; set; }
            public string region_name { get; set; }
            public string city { get; set; }
            public string zip_code { get; set; }
            public string time_zone { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int metro_code { get; set; }
 
        //public string Ip { get; set; }
        //public string CountryCode { get; set; }
        //public string CountryName { get; set; }
        //public string RegionCode { get; set; }
        //public string RegionName { get; set; }
        //public string City { get; set; }
        //public string ZipCode { get; set; }
        //public string TimeZone { get; set; }
        //public decimal Latitude { get; set; }
        //public decimal Longitude { get; set; }
        //public int MetroCode { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Natech.IpGeoFinder.Api.Model
{
    //BatchDetails
    public class GeoIpModel
    {
        [Required]
        public string Ip { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string TimeZone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}

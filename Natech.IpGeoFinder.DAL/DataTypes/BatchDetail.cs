using System;
using System.Collections.Generic;

#nullable disable

namespace Natech.IpGeoFinder.DAL.DataTypes
{
    public partial class BatchDetail
    {
        public long Id { get; set; }
        public Guid BatchId { get; set; }
        public string Ip { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string TimeZone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? FetchedDateTime { get; set; }

        public virtual Batch Batch { get; set; }
    }
}

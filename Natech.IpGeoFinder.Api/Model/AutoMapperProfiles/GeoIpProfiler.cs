using AutoMapper;
using Natech.IpGeoFinder.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Natech.IpGeoFinder.Api.Model.AutoMapperProfiles
{
    public class GeoIpProfiler : Profile
    {
        public GeoIpProfiler()
        {
            CreateMap<GeoIP, GeoIpModel>()
                .ForMember(m => m.CountryName, d => d.MapFrom(s => s.country_name))
                .ForMember(m => m.CountryCode , d => d.MapFrom(s => s.country_code))
                .ForMember(m => m.TimeZone, d => d.MapFrom(s => s.time_zone));

            
            CreateMap<Batch, BatchModel>();
            CreateMap<BatchDetail, BatchDetailsModel>(); 
            CreateMap<Status, StatusModel>();
            CreateMap<BatchDetail, GeoIP>();
            CreateMap<GeoIP, BatchDetail>()
                .ForMember(m => m.CountryName, d => d.MapFrom(s => s.country_name))
                .ForMember(m => m.CountryCode, d => d.MapFrom(s => s.country_code))
                .ForMember(m => m.TimeZone, d => d.MapFrom(s => s.time_zone));


        }
    }
}

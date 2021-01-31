using Natech.IpGeoFinder.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.DAL.Interfaces
{
    public interface IGeoIpRepository
    {
        /// <summary>
        /// Getting The Location of an IP address from the freeGeoIP service
        /// </summary>
        /// <param name="iP">IP Address</param>
        /// <returns></returns>
        Task<GeoIP> GetGeo(string iP);

        /// <summary>
        /// Getting The Location of all IPs addresses from the freeGeoIP service
        /// </summary>
        /// <param name="iPs">List of IP Addresses</param>
        /// <returns></returns>
        Task<List<GeoIP>> GetGeos(List<string> iPs);
    }
}

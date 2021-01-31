using Natech.IpGeoFinder.DAL.DataTypes;
using Natech.IpGeoFinder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.DAL.Repositories
{
    public class GeoIpRepository : IGeoIpRepository
    {


        public Task<GeoIP> GetGeo(string iP)
        {
            return CallFreeGeoIp(iP);
        }

        public Task<List<GeoIP>> GetGeos(List<string> iPs)
        {
            //get batchid
            //save on batch details
            //run async
            //return the link for batch details
            throw new NotImplementedException();
        }

        private static async Task<GeoIP> CallFreeGeoIp(string iP)
        {
            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://freegeoip.app/");

            var request = new HttpRequestMessage(HttpMethod.Get, $"/json/{ iP }");
            using var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GeoIP>();
            }
            else
            {
                return null;
            }

        }
    }
}

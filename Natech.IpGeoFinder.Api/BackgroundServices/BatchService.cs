using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Natech.IpGeoFinder.DAL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.Api.BackgroundServices
{
    public class BatchService : BackgroundService
    {
        private readonly BatchProcessingChannel _batchProcessingChannel;
        private readonly IServiceProvider _serviceProvider;
        //private readonly IMapper _mapper;

        public BatchService(BatchProcessingChannel boundedMessageChannel, IServiceProvider serviceProvider)
        {
            _batchProcessingChannel = boundedMessageChannel;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await foreach (var batchId in _batchProcessingChannel.ReadAllAsync())
            {
                using var scope = _serviceProvider.CreateScope();
                var repositoryDB = scope.ServiceProvider.GetRequiredService<IGeoIpDbRepository>();
                var repository = scope.ServiceProvider.GetRequiredService<IGeoIpRepository>();


                foreach (var batchDetail in await repositoryDB.GetBatchDetailsById(batchId))
                {
                    var result = repository.GetGeo(batchDetail.Ip).Result;
                    batchDetail.CountryName = result.country_name;
                    batchDetail.CountryCode = result.country_code;
                    batchDetail.TimeZone = result.time_zone;
                    batchDetail.Latitude = decimal.Parse(result.latitude.ToString());
                    batchDetail.Longitude = decimal.Parse(result.longitude.ToString());
                    batchDetail.FetchedDateTime = DateTime.Now;


                    //Κανονικα θα το εβγαζα εξω απο το foreach αλλα το θυσιαζω για να βλεπω το proggress απο την ΒΔ.
                    await repositoryDB.SaveChangesAsync();
                }

            }


        }
    }
}

using AutoMapper;
using Natech.IpGeoFinder.Api.Model;
using Natech.IpGeoFinder.DAL.DataTypes;
using Natech.IpGeoFinder.DAL.Interfaces;
using Natech.IpGeoFinder.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.Api.Utilities
{
    public class BatchProcessor
    {
        private readonly IMapper _mapper;
        private readonly IGeoIpRepository _repository;
        private readonly IGeoIpDbRepository _repositoryDB;

        public BatchProcessor(IMapper mapper, IGeoIpRepository repository, IGeoIpDbRepository repositoryDB)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryDB = repositoryDB;
        }

        public async Task<Guid> ProccessIps(BatchIpsModel batchIps)
        {

            Batch batch = new Batch()
            {
                InsertionDateTime = DateTime.Now,
                StatusId = 1
            };

            _repositoryDB.Add(batch);

            foreach (var batchIp in batchIps.Ip)
            {

                var result = _repository.GetGeo(batchIp).Result;
                var r = _mapper.Map<BatchDetail>(result);
                r.FetchedDateTime = DateTime.Now;
                batch.BatchDetails.Add(r);

                //Κανονικα θα το εβγαζα εξω απο το foreach αλλα το θυσιαζω για να βλεπω το proggress απο την ΒΔ.
                await _repositoryDB.SaveChangesAsync();
            }

            return batch.Id;

        }

        public string ProgressReport(Guid batchId)
        {
            return _repositoryDB.GetProgressMsgById(batchId);
        }

    }
}

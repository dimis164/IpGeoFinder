using AutoMapper;
using Natech.IpGeoFinder.Api.Model;
using Natech.IpGeoFinder.DAL.DataTypes;
using Natech.IpGeoFinder.DAL.Interfaces;
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
                Id = Guid.NewGuid(),
                InsertionDateTime = DateTime.Now,
                StatusId = 1
            };

            _repositoryDB.Add(batch);

            foreach (var ip in batchIps.Ip)
            {
                batch.BatchDetails.Add(new BatchDetail { Ip = ip });
            }


            await _repositoryDB.SaveChangesAsync();



            return batch.Id;

        }

        public string ProgressReport(Guid batchId)
        {
            return _repositoryDB.GetProgressMsgById(batchId);
        }

    }
}

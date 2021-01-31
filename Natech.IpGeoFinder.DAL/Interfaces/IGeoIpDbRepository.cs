using Natech.IpGeoFinder.DAL.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.DAL.Interfaces
{
    public interface IGeoIpDbRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Batch
        Batch GetBatchById(Guid batchId);
        bool ChangeBatchStatusById(Guid batchId,Status status);

        //BatchDetails
        Task<List<BatchDetail>> GetBatchDetailsById(Guid batchId);
        string GetProgressMsgById(Guid batchId);
        Task<bool> ProccessBatchById(Guid batchId);
    }
}

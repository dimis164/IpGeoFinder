using Microsoft.EntityFrameworkCore;
using Natech.IpGeoFinder.DAL.DataTypes;
using Natech.IpGeoFinder.DAL.Interfaces;
using Natech.IpGeoFinder.DAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Natech.IpGeoFinder.DAL.Repositories
{


    public class GeoIpDbRepository : IGeoIpDbRepository
    {
        private readonly GeoIpDBContext _context;

        public GeoIpDbRepository(GeoIpDBContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool ChangeBatchStatusById(Guid batchId, Status status)
        {
            try
            {
                var b = _context.Batches.Where(o => o.Id == batchId).FirstOrDefault();
                b.Status = status;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<BatchDetail>> GetBatchDetailsById(Guid batchId)
        {
            return await _context.BatchDetails.Where(o => o.BatchId == batchId).ToListAsync();
        }

        public string GetProgressMsgById(Guid batchId)
        {
            var batchList = _context.BatchDetails.Where(o => o.BatchId == batchId).ToList();
            int completedCalls = batchList.Where(o => o.FetchedDateTime is not null).Count();
            DateTime maxTime = batchList.Where(o => o.FetchedDateTime is not null).Select(o => o.FetchedDateTime).Max().ToNonNullable();
            DateTime minTime = batchList.Where(o => o.FetchedDateTime is not null).Select(o => o.FetchedDateTime).Min().ToNonNullable();

            var totalSeconds = maxTime.Subtract(minTime).TotalSeconds;


            var msg = $"Completed ip searches from freeGeoIP: { completedCalls }/{ batchList.Count }. {Environment.NewLine}" +
                      $"Seconds per search: { totalSeconds / completedCalls }.{ Environment.NewLine }";

            if (completedCalls != batchList.Count)
            {
                msg += $"Finish Estimation: { DateTime.Now.AddSeconds((totalSeconds / completedCalls) * (batchList.Count - completedCalls))}";
            }


            //var msg = completedCalls.ToString();
            //msg += "/";
            //msg += batchList.Count;

            return msg;
        }

        public Task<bool> ProccessBatchById(Guid batchId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Batch GetBatchById(Guid batchId)
        {
            return _context.Batches.Where(o => o.Id == batchId).FirstOrDefault();
        }
    }
}

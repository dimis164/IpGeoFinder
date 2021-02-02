using System;
using System.Collections.Generic;

namespace Natech.IpGeoFinder.Api.Model
{
    public class BatchModel
    {

        public Guid Id { get; set; }
        public DateTime? InsertionDateTime { get; set; }
        public short? StatusId { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual ICollection<BatchDetailsModel> BatchDetails { get; set; }

    }
}

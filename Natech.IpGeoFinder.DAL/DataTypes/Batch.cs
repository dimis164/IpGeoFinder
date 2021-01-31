using System;
using System.Collections.Generic;

#nullable disable

namespace Natech.IpGeoFinder.DAL.DataTypes
{
    public partial class Batch
    {
        public Batch()
        {
            BatchDetails = new HashSet<BatchDetail>();
        }

        public Guid Id { get; set; }
        public DateTime? InsertionDateTime { get; set; }
        public short? StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<BatchDetail> BatchDetails { get; set; }
    }
}

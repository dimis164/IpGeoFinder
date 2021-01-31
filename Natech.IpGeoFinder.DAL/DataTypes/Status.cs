using System;
using System.Collections.Generic;

#nullable disable

namespace Natech.IpGeoFinder.DAL.DataTypes
{
    public partial class Status
    {
        public Status()
        {
            Batches = new HashSet<Batch>();
        }

        public short Id { get; set; }
        public string Literal { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}

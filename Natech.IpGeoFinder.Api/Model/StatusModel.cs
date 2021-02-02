using System.Collections.Generic;

namespace Natech.IpGeoFinder.Api.Model
{
    public class StatusModel
    {
        public short Id { get; set; }
        public string Literal { get; set; }

        public virtual ICollection<BatchModel> Batches { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AutomobileWebsite.Models.Models
{
    public partial class Dealership
    {
        public Dealership()
        {
            DealershipAddresses = new HashSet<DealershipAddress>();
        }

        public int DealershipId { get; set; }
        public string DealershipName { get; set; }
        public string WebsiteUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<DealershipAddress> DealershipAddresses { get; set; }
    }
}

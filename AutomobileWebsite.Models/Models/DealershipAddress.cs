using System;
using System.Collections.Generic;

namespace AutomobileWebsite.Models.Models
{
    public partial class DealershipAddress
    {
        public DealershipAddress()
        {
            Cars = new HashSet<Car>();
        }

        public int DealershipAddressId { get; set; }
        public int DealershipId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Dealership Dealership { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

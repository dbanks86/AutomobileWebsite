using System;
using System.Collections.Generic;

namespace AutomobileWebsite.Models.Models
{
    public partial class Car
    {
        public int CarId { get; set; }
        public short Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public bool? IsActive { get; set; }
        public int DealershipAddressId { get; set; }

        public virtual DealershipAddress DealershipAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AutomobileWebsite.Models.Models
{
    public partial class State
    {
        public State()
        {
            DealershipAddresses = new HashSet<DealershipAddress>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }

        public virtual ICollection<DealershipAddress> DealershipAddresses { get; set; }
    }
}

using System;

namespace Iwannago.Models
{
    public class FHVTripRecord
    {
        public string Dispatching_base_num { get; set; }
        public DateTime Pickup_DateTime { get; set; }
        public DateTime DropOff_datetime { get; set; }
        public string PUlocationID { get; set; }
        public string DOlocationID { get; set; }
        public string SR_Flag { get; set; }
    }
}

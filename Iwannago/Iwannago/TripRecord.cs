using System;
using System.Collections.Generic;
using System.Text;

namespace Iwannago
{
    public class TripRecord
    {
        //"Dispatching_base_num","Pickup_DateTime","DropOff_datetime","PUlocationID","DOlocationID","SR_Flag"
        public string Dispatching_base_num { get; set; }
        public DateTime Pickup_DateTime { get; set; }
        public DateTime DropOff_datetime { get; set; }
        public string PUlocationID { get; set; }
        public string DOlocationID { get; set; }
        public string SR_Flag { get; set; }
    }
}

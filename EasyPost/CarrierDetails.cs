using System;

namespace EasyPost{
    public class CarrierDetails : Resource{
        public string service { set; get; }
        public string container_type { set; get; }
        public DateTime? est_delivery_date_local { set; get; }
        public DateTime? est_delivery_time_local { set; get; }
    }
}

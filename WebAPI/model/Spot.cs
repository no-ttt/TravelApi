using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.model
{
    public class Spot
    {
        
        public int OID { get; set; }

        public string Name { get; set; } = string.Empty;

        

        public int Type { get; set; }

        public string CName { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

    }
    public class Spot_type
    {

        public int OID { get; set; }

        public string Name { get; set; } = string.Empty;



        public int Type { get; set; }

        public string CName { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

    }
    public class Spot_city
    {

        public int OID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Type { get; set; }

        public string CName { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

    }
    public class Spot_detail
    {

        public int OID { get; set; }

        public string Name { get; set; }

        public string Des { get; set; }

        public string addr { get; set; }

        public string phone { get; set; }

        public string url { get; set; }

        public string Geohash { get; set; }

        public int MyProperty { get; set; }

        public int Type { get; set; }

        public string PictureUrl { get; set; }

        public string TravelInfo { get; set; }

        public string Remarks { get; set; }

        public string Ticket { get; set; }

        public string Parking { get; set; }

        public string Opentime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


    }

}

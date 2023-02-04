﻿namespace MvcWeb.Models
{
    public class AddressCard
    {
        public string Street {get; set; }
        public string Suite {get; set; }
        public string City {get; set; }
        public string State {get; set; }
        public string ZipCode {get; set; }
        public AppCardGeo Geo {get; set; }

        public class AppCardGeo
        {
            public double Lat {get; set; }
            public double Lng {get; set; }
        }
    }
}

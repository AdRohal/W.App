namespace WeatherApp.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Geolocation
    {
        public string Status { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Region { get; set; }

        public string RegionName { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Timezone { get; set; }

        public string Isp { get; set; }

        public string Org { get; set; }

        public string As { get; set; }

        public string Query { get; set; }
    }
}

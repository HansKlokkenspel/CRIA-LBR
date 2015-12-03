using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string location, string country = "Netherlands")
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Message = "Undetermined failure while looking up coordinates"
            };

            var gmapsKey = Startup.Configuration["AppSettings:GmapsKey"];
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?components=locality:|administrative_area:{location}&key={gmapsKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["results"][0]["geometry"];

            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var coords = resources["location"];
                result.Latitude = (double)coords["lat"];
                result.Longitude = (double)coords["lng"];
                result.Success = true;
                result.Message = "Success";
            }

            return result;
        }
    }
}

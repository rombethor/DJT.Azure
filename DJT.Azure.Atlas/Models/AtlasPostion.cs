using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    public class AtlasPosition
    {
        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("lon")]
        public decimal Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    public class AtlasResult
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("entityType")]
        public string? EntityType { get; set; }

        [JsonPropertyName("address")]
        public AtlasAddress? Address { get; set; }

        [JsonPropertyName("position")]
        public AtlasPosition? Position { get; set; }
    }
}

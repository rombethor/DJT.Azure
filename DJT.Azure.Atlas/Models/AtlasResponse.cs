using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    public class AtlasResponse
    {
        [JsonPropertyName("results")]
        public List<AtlasResult> Results { get; set; } = new List<AtlasResult>();
    }
}

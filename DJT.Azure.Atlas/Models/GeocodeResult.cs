using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    /// <summary>
    /// Result from reverse lookup (geocode)
    /// </summary>
    public class GeocodeResult
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public AtlasAddress Address { get; set; } = new AtlasAddress();

        /// <summary>
        /// String position, "lat,lon"
        /// </summary>
        [JsonPropertyName("position")]
        public string Position { get; set; } = string.Empty;
    }
}

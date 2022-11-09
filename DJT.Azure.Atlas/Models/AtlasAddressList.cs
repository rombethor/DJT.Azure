using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    /// <summary>
    /// Address list, as returned by reverse lookup
    /// </summary>
    public class AtlasAddressList
    {
        /// <summary>
        /// List of address results
        /// </summary>
        [JsonPropertyName("addresses")]
        public IEnumerable<AtlasResult> Addresses { get; set; } = Array.Empty<AtlasResult>();
    }
}

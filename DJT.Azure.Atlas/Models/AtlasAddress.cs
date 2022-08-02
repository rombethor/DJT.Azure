using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJT.Azure.Atlas.Models
{
    public class AtlasAddress
    {
        [JsonPropertyName("streetNumber")]
        public string? StreetNumber { get; set; }

        [JsonPropertyName("streetName")]
        public string? StreetName { get; set; }

        [JsonPropertyName("municipalitySubdivision")]
        public string? MunicipalitySubdivision { get; set; }

        [JsonPropertyName("municipality")]
        public string? Municipality { get; set; }

        [JsonPropertyName("countrySecondarySubdivision")]
        public string? CountrySecondarySubdivision { get; set; }

        [JsonPropertyName("countrySubdivision")]
        public string? CountrySubdivision { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("countryCode")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("countryCodeISO3")]
        public string? CountryCodeISO3 { get; set; }

        [JsonPropertyName("freeFormAddress")]
        public string? FreeFormAddress { get; set; }

        [JsonPropertyName("localName")]
        public string? LocalName { get; set; }
    }
}

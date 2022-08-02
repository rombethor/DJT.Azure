﻿using DJT.Azure.Atlas.Models;
using System.Text.Json;

namespace DJT.Azure.Atlas
{
    /// <summary>
    /// Calls for Azure Maps
    /// </summary>
    public class Atlas
    {
        
        private string? secretKey = null;
        public string Secret { set { secretKey = value; } }

        private static Atlas? _atlas;
        /// <summary>
        /// Retrieve the singleton Atlas instance.
        /// <see cref="Secret"/> must be set before calling this!
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Atlas GetInstance()
        {
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentException("Secret not set in Atlas");
            if (_atlas == null)
                _atlas = new Atlas(secretKey);
            return _atlas;
        }

        /// <summary>
        /// Construct this helper with the secret key
        /// </summary>
        /// <param name="SecretKey"></param>
        private Atlas(string SecretKey)
        {
            secretKey = SecretKey;
        }

        /// <summary>
        /// Get the coordinates of a postcode area in the given country
        /// </summary>
        /// <param name="postcode"></param>
        /// <param name="countryCode"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<AtlasPosition?> FindCoordinatesForPostcode(string postcode, string countryCode, int limit = 1)
        {
            HttpClient client;
            client = new HttpClient();

            var url = $"https:" + $"//atlas.microsoft.com/search/fuzzy/json?api-version=1.0&subscription-key={secretKey}&idxSet=Geo&query={postcode}&countrySet={countryCode}&limit={limit}";

            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                string rawdata = await result.Content.ReadAsStringAsync();
                AtlasResponse? atlasResponse = JsonSerializer.Deserialize<AtlasResponse>(rawdata);
                if (atlasResponse == null)
                    return null;
                return atlasResponse.Results.FirstOrDefault()?.Position;
            }
            else return null;
        }

        /// <summary>
        /// Locate an address from the number, road name and postcode in a given country
        /// </summary>
        /// <param name="extNumber"></param>
        /// <param name="road"></param>
        /// <param name="postcode"></param>
        /// <param name="countryCode"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<AtlasResult>> FindAddress(string extNumber, string road, string postcode, string countryCode, int limit = 1)
        {
            HttpClient client = new HttpClient();

            var addr = $"{extNumber} {road}, {postcode}";
            var url = "https://" + $"atlas.microsoft.com/search/fuzzy/json?api-version=1.0&subscription-key={secretKey}&query={addr}&countrySet={countryCode}&limit={limit}&idxSet=PAD";

            var result = await client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                string rawdata = await result.Content.ReadAsStringAsync();
                AtlasResponse? atlasResponse = JsonSerializer.Deserialize<AtlasResponse>(rawdata);
                return atlasResponse?.Results ?? new List<AtlasResult>();
            }
            else return new List<AtlasResult>();
        }



    }
}
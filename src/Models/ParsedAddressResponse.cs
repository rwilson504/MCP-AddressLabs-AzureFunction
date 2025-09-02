using System.Text.Json.Serialization;

namespace AddressLabsMCP.Models
{
    public class ParsedAddressResponse
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("street_suffix")]
        public string StreetSuffix { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state_fips")]
        public string StateFips { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("intersection")]
        public bool Intersection { get; set; }

        [JsonPropertyName("delivery")]
        public Delivery Delivery { get; set; }

        [JsonPropertyName("input")]
        public string Input { get; set; }
    }

    public class Delivery
    {
        [JsonPropertyName("address_line")]
        public string AddressLine { get; set; }

        [JsonPropertyName("last_line")]
        public string LastLine { get; set; }
    }
}
namespace CSAppFE.Common.Models
{
    using Newtonsoft.Json;

    public class Client
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cuit")]
        public long Cuit { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Cuit}";
        }
    }
}

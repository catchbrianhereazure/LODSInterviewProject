namespace LODSInterviewProject.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Organization
    {
        public User User { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}

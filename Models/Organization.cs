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

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "users")]
        public IList<LODSInterviewProject.Models.User> Users { get; set; }

        public Organization()
        {
            Users = new List<LODSInterviewProject.Models.User>(0);
        }
    }
}

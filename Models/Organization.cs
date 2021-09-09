namespace LODSInterviewProject.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class Organization
    {
        public User User { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Organization Name"), MaxLength(30)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter a Description for Organization"), MaxLength(1024)]
        public string Description { get; set; }
    }
}

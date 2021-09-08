namespace LODSInterviewProject.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "organizationid")]
        public string OrganizationId { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter First Name"), MaxLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Last Name"), MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }
    }
}

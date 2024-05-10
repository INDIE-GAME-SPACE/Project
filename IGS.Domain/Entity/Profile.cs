using System.ComponentModel.DataAnnotations;

namespace IGS.Domain.Entity
{
    public class Profile
    {
        [Key]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Country { get; set; }

        public string? URL { get; set; }

        public string? GitHubLink { get; set; }
    }
}

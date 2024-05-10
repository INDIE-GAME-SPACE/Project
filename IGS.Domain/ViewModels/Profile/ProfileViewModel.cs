namespace IGS.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public string Name { get; set; }

        public bool ICreator { get; set; }

        public bool IUser { get; set; }

        public string? Description { get; set; }

        public string? Country { get; set; }

        public string? URL { get; set; }

        public string? GitHubLink { get; set; }
    }
}

using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using IGS.Domain.Response;
using IGS.Domain.ViewModels.Profile;

namespace IGS.Service.Implementations
{
	public class SettingsService
	{
		private readonly IUserRepository _userRepository;
		private readonly IProfileRepository _profileRepository;

		public SettingsService(IUserRepository userRepository, IProfileRepository profileRepository)
		{
			_userRepository = userRepository;
			_profileRepository = profileRepository;
		}

		public async Task<BaseResponse<ProfileViewModel>> GetSettingsModel(string login)
		{
			try
			{
				Profile profile = await _profileRepository.GetByName(login);
				User user = await _userRepository.GetByLogin(login);
				ProfileViewModel profileViewModel = new ProfileViewModel()
				{
					Name = profile.Name,
					ICreator = user.Role.Equals("Creator"),
					IUser = user.Role.Equals("User"),
					Description = profile.Description,
					Country = profile.Country,
					URL = profile.URL,
					GitHubLink = profile.GitHubLink,
				};

				return new BaseResponse<ProfileViewModel>()
				{
					Description = "Профиль получен успешно",
					StatusCode = Domain.Enums.StatusCode.OperationSuccess,
					Data = profileViewModel
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<ProfileViewModel>()
				{
					Description = ex.Message,
					StatusCode = Domain.Enums.StatusCode.NotFoundException,
				};
			}
		}

		public async Task<BaseResponse<ProfileViewModel>> SaveProfile(string name, ProfileViewModel profileModel)
		{
			Profile profile = await _profileRepository.GetByName(name);
			profile.Name = profileModel.Name;
			profile.Description = profileModel.Description;
			profile.Country = profileModel.Country;
			profile.URL = profileModel.URL;
			profile.GitHubLink = profileModel.GitHubLink;

			bool result = await _profileRepository.SaveProfile(profile);
			Domain.Enums.StatusCode code = result ? Domain.Enums.StatusCode.OperationSuccess : Domain.Enums.StatusCode.AddElementError;
			return new BaseResponse<ProfileViewModel>()
			{
				StatusCode = code,
			};
		}
	}
}

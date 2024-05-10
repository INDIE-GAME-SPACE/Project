using IGS.Domain.Response;
using IGS.Domain.ViewModels.Profile;
using IGS.Domain.ViewModels.Settings;
using IGS.Service.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class SettingsController : Controller
    {
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService) => _settingsService = settingsService;

        [HttpPost]
        public async Task<IActionResult> SaveProfile(ProfileViewModel model)
        {
            BaseResponse<ProfileViewModel> response = await _settingsService.SaveProfile(User.Identity.Name, model);
            SettingsViewModel settingsViewModel = new SettingsViewModel(0);
            return View(settingsViewModel);
        }

        public IActionResult Settings(int componentId)
        {
            SettingsViewModel model = new SettingsViewModel(componentId);
            return View(model);
        }
    }
}
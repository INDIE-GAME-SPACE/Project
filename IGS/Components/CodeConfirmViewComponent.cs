using Microsoft.AspNetCore.Mvc;

namespace IGS.Components
{
    public class CodeConfirmViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

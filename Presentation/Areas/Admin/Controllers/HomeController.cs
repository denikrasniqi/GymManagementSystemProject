using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Areas.Admin.Models.DashboardViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    //[Authorize(Roles = RoleConstants.Admin)]
    public class HomeController : Controller
    {
        private IOptions<RequestLocalizationOptions> _options;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository userRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IEntryRepository entryRepository;

        public HomeController(IHttpContextAccessor httpContextAccessor, IOptions<RequestLocalizationOptions> options,IUserRepository userRepo, IMemberRepository memberRepo, IEntryRepository entryRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
            this.userRepository = userRepo;
            this.memberRepository = memberRepo;
            this.entryRepository = entryRepo;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            model.NrUsers = userRepository.Count();
            model.NrMembers = memberRepository.Count();
            model.NrEntries = entryRepository.Count();
            return View(model);
            
        }

        public IActionResult SetLanguage(string culture)
        {
            try
            {
                var cultureItems = _options.Value.SupportedUICultures!.Select(x => x.Name).ToArray();
                if (cultureItems.Any(x => x.Equals(culture)))
                {
                    Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, Secure = _httpContextAccessor.HttpContext!.Request.IsHttps });
                }
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

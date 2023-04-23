using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class EntriesController: Controller
    {
        protected readonly IEntryRepository _entryRepo;

        public EntriesController(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }
        public IActionResult Index()
        {
            var modelView = _entryRepo.GetEntries();
            return View();
        }
    }
}

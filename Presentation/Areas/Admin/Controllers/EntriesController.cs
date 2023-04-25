using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Implementations;
using GymManagementSystem.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.EntryViewModel;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    //[Authorize(Roles = RoleConstants.Admin)]
    public class EntriesController: Controller
    {
        protected readonly IEntryRepository _entryRepo;
        public EntriesController(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }
        public IActionResult Index()
        {
            //var modelView = _entryRepo.GetEntries();
            return View();
        }
        [HttpGet]
        public IActionResult GetEntriesJson()
        {
            var entries = _entryRepo.GetEntries();
            try
            {
                var result = entries.Select(x => new
                {
                    id = x.Id,
                    userid = x.UserId,
                    isentry = x.IsEntry,
                    date = x.Date,
                });

                return new JsonResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Implementations;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.EntrysViewModel;
using Presentation.Areas.Admin.Models.EntryViewModel;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    //[Authorize(Roles = RoleConstants.Admin)]
    public class EntriesController: Controller
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IUserRepository _userRepository;
        public EntriesController(IEntryRepository entryRepo, IUserRepository userRepository)
        {
            _entryRepo = entryRepo;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            //var modelView = _entryRepo.GetEntries();
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.Title = "Add Entry";
            // add the code below to disable the ExitDate input when adding a new entry
            ViewBag.IsAddMode = true;
            var model = new EntriesViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAsync(EntriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Id))
                {

                    DateTime startDate = DateTime.Now;
                    var entry = new Entry
                    {
                        UserId = model.UserId!,
                        Name = model.Name!,
                        Surname = model.Surname!,
                        IsEntry = true,
                        EntryDate = model.EntryDate.GetValueOrDefault(),
                        //IsExit = model.ExitDate.HasValue ? true : (bool?)null,
                        //ExitDate = model.ExitDate.GetValueOrDefault(),
                    };
                    _entryRepo.Add(entry);
                    _entryRepo.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {

                    var entry = _entryRepo.GetByStringId(model.Id);

                    if (entry != null)
                    {
                        entry.Name = model.Name;
                        entry.Surname = model.Surname;
                        entry.IsExit= true;
                        entry.ExitDate = model.ExitDate.GetValueOrDefault();
                        

                        _entryRepo.Update(entry);
                        _entryRepo.SaveChanges();

                        return RedirectToAction("Index");

                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Title = "Add Exit";
            ViewBag.IsEditMode = true;
            // add the code below to disable the ExitDate input when adding a new entry
            ViewBag.IsAddMode = false;
            Entry? entry = _entryRepo.GetByStringId(id);
            if (entry != null)
            {
                var model = new EntriesViewModel()
                {
                    Id = id,
                    Name = entry.Name!,
                    Surname = entry.Surname!,
                    IsExit = false,
                };

                return View("Add", model);
            }
            return RedirectToAction("Index");
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
                    name = x.Name,
                    surname = x.Surname,
                    isentry = x.IsEntry,
                    entrydate = x.EntryDate,
                    isexit = x.IsExit,
                    exitdate = x.ExitDate,
                });

                return new JsonResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("getMemberByName")]
        public IActionResult GetMemberByName(string name)
        {
            if (name is null)
                return BadRequest();

            //get member
            var result = _entryRepo.GetMember(name);

            //return not found on user if null
            if (result is null)
                return NotFound();

            return Json(result);
        }
    }
}

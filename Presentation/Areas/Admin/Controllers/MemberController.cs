using Arch.EntityFrameworkCore.Internal;
using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Implementations;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Data.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.MembersViewModel;
using System.Linq;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;

        public MemberController(IMemberRepository memberRepository, IUserRepository userRepository)
        {
            _memberRepository = memberRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model = new MemberViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAsync(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    var member = new Antaresimi { UserId = model.UserId!,Name =model.Name! ,Surname = model.Surname! , StartDate = Convert.ToDateTime(model.StartDate), Statusi = model.Statusi, Qmimi = Convert.ToInt32(model.Qmimi), Antaresimi1 = model.Antaresimi };
                    _memberRepository.Add(member);
                    _memberRepository.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet("getUserByName")]
        public IActionResult GetUserByName(string name)
        {
            if (name is null)
                return BadRequest();

            //get member
            var result = _memberRepository.GetMember(name);

            //return not found on user if null
            if (result is null)
                return NotFound();

            return Json(result);
        }

        [HttpGet]

        public IActionResult GetMembersJson()
        {
            var members = _memberRepository.GetAll();
            try
            {
                var result = members.Select(x => new
                {
                    id = x.Id,
                    userid = x.UserId,
                    name= x.Name,
                    surname= x.Surname,
                    startdate = x.StartDate,
                    status = x.Statusi,
                    price = x.Qmimi,
                    membership = x.Antaresimi1
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

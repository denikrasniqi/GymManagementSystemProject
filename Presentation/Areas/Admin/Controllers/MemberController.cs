using Arch.EntityFrameworkCore.Internal;
using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Implementations;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Data.Identity;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models.EntryViewModel;
using Presentation.Areas.Admin.Models.UsersViewModels;
using System.Data;
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

                    DateTime startDate = DateTime.Now;
                    var member = new Antaresimi { UserId = model.UserId!,Name =model.Name! ,Surname = model.Surname! ,
                        StartDate = startDate, Statusi = model.Statusi, Qmimi = Convert.ToInt32(model.Qmimi),
                        Antaresimi1 = model.Antaresimi };
                    _memberRepository.Add(member);
                    _memberRepository.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {

                    var member = _memberRepository.GetByStringId(model.Id);

                    if (member != null)
                    {
                        member.Name = model.Name;
                        member.Surname = model.Surname;
                        member.StartDate = (DateTime)model.StartDate;
                        member.Statusi = model.Statusi;
                        member.Antaresimi1 = model.Antaresimi;
                        member.Qmimi = (int)model.Qmimi;

                        _memberRepository.Update(member);
                        _memberRepository.SaveChanges();

                        return RedirectToAction("Index");

                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Antaresimi? member = _memberRepository.GetByStringId(id);
            if (member != null)
            {
                var model = new MemberViewModel()
                {
                    Id = id,
                    Name = member.Name!,
                    Surname = member.Surname!,
                    StartDate= member.StartDate,
                    Statusi = member.Statusi,
                    Antaresimi = member.Antaresimi1,
                    Qmimi = member.Qmimi
                };

                return View("Add", model);
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
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var member = _memberRepository.GetByStringId(id);
                if (member != null)
                {
                    _memberRepository.Remove(member);
                    _memberRepository.SaveChanges();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

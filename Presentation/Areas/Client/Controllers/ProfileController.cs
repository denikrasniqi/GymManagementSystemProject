using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Implementations;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers.Interfaces;
using Presentation.Areas.Admin.Models.UsersViewModels;
using Presentation.Areas.Client.Models.ProfileViewModels;

namespace Presentation.Areas.Client.Controllers
{
    [Area(AreasConstants.Client)]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEntryRepository _entryRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ILogger _logger;
        //private readonly IFileHelper _fileHelper;
        public ProfileController(IEntryRepository entryRepo, IUserRepository userRepository, IMemberRepository memberRepo, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager/*, IFileHelper fileHelper*/)
        {
            _userManager = userManager;
            _entryRepo = entryRepo;
            _userRepo = userRepository;
            _memberRepo = memberRepo;
            //_fileHelper = fileHelper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Account(string? id)
        {
            var model = new ProfileViewModels();
            if (id != null)
            {
                AspNetUser? user = _userRepo.GetByStringId(id);
                if (user != null)
                {
                    model = new ProfileViewModels()
                    {
                        Id = id,
                        Password = "",
                        ConfirmPassword = "",
                        Email = user.Email!,
                        Name = user.Name!,
                        PhoneNumber = user.PhoneNumber,
                        Surname = user.Surname!,
                    };

                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Account(ProfileViewModels model)
        {
            if (ModelState.IsValid)
            {
                string userId = "";
                if (string.IsNullOrEmpty(model.Id))
                {
                    var user = new ApplicationUser
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        UserName = model.Email,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        userId = user.Id;
                        _logger.LogInformation("User created a new account with password.");
                        var roleResult = await _userManager.AddToRoleAsync(user, "Client");
                        if (roleResult.Succeeded)
                        {
                            _logger.LogInformation($"User created with role Client");
                        }
                    }
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        userId = user.Id;
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;

                        var editResult = await _userManager.UpdateAsync(user);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

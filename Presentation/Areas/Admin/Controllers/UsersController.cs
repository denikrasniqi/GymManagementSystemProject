using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymManagementSystem.App.Constants;
using System.Linq.Expressions;
using Presentation.Areas.Admin.Models.UsersViewModels;

namespace Presantation.Areas.Admin.Controllers
{

    [Area(AreasConstants.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolesRepository rolesRepository;
        private readonly ILogger _logger;
        private readonly ISelectListService selectListService;

        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IRolesRepository rolesRepository, ILogger<UsersController> logger)
        {
            this.userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.rolesRepository = rolesRepository;
            _logger = logger;
            this.selectListService = selectListService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            AspNetUser? user = userRepository.GetByStringId(id);
            if (user != null)
            {
                var model = new UserViewModel()
                {
                    Id = id,
                    Password = "",
                    ConfirmPassword = "",
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed,
                    Name = user.Name!,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    RoleId = rolesRepository.GetByUserId(user.Id)!.Id,
                    Surname = user.Surname!,
                };

                model.Roles = new SelectList(selectListService.GetRolesKeysValues(), "SKey", "Value", model.RoleId);

                return View("Add", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
       
        public async Task<IActionResult> AddAsync(UserViewModel model)//Method for adding new user
        {
            if(ModelState.IsValid)
            {
                string userId = "";
                if (string.IsNullOrEmpty(model.Id))
                {
                    var user = new ApplicationUser { Name = model.Name, Surname = model.Surname, UserName = model.Email, Email = model.Email, EmailConfirmed = model.EmailConfirmed, PhoneNumber = model.PhoneNumber, PhoneNumberConfirmed = model.PhoneNumberConfirmed };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        userId = user.Id;
                        var selectedRole = rolesRepository.GetByStringId(model.RoleId);
                        if (selectedRole != null)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, selectedRole.Name);
                            if (roleResult.Succeeded)
                            {
                                _logger.LogInformation($"User created with role {selectedRole.Name}");
                            }
                        }

                        _logger.LogInformation("User created a new account with password.");
                    }
                    
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        //user.Email = model.Email;
                        user.EmailConfirmed = model.EmailConfirmed;
                        user.PhoneNumber = model.PhoneNumber;
                        user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;

                        var editResult = await _userManager.UpdateAsync(user);

                        if (editResult.Succeeded)
                        {
                            var currentRole = rolesRepository.GetByUserId(user.Id);
                            if (currentRole != null && currentRole.Id != model.RoleId)
                            {
                                var result = await _userManager.RemoveFromRoleAsync(user, currentRole.Name);
                                if (result.Succeeded)
                                {
                                    var selectedRole = rolesRepository.GetByStringId(model.RoleId);
                                    if (selectedRole != null)
                                    {
                                        await _userManager.AddToRoleAsync(user, selectedRole.Name);
                                    }
                                }
                            }
                        }
                    }
                }


            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetUsersJson()
        {
            var users = userRepository.GetAllWithRoles();
            //users.ForEach(x => x.PasswordHash = userRepository.GetProfilePicturePath(x.Id, (int)ThumbnailsEnum.Grid));
            try
            {
                var result = users.Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    surname = x.Surname,
                    email = x.Email,
                    EmailConfirmed = x.EmailConfirmed,
                    role = x.Roles.FirstOrDefault()!.Name
                });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
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

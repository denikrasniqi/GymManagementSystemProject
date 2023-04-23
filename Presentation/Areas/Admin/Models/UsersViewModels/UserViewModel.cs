using GymManagementSystem.App.Constants;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Areas.Admin.Models.UsersViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }
        public string RoleId { get; set; } = null!;
        
        public SelectList? Roles { get; set; }
        //Relationships
        //public List<Antaresimi>? AntarId { get; set; }
        //[ForeignKey("AntarId")]
        //public Antaresimi Antaresimi { get; set; }
    }

}

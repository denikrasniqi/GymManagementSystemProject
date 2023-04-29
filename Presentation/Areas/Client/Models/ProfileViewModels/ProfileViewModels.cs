using GymManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Client.Models.ProfileViewModels { 
    public class ProfileViewModels
    {
        public string? Id { get; set; }
 
        public string? Name { get; set; }
      
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }





   
    }
}

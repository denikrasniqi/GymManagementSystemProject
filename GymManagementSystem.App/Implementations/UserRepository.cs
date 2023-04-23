using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Context;
using GymManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.App.Implementations
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        protected readonly GymSystemDBContext _gymSystemDbContext;
        public UserRepository(GymSystemDBContext gymSystemDbContext) : base(gymSystemDbContext)
        {
            _gymSystemDbContext = gymSystemDbContext;
        }

        public AspNetUser? GetByStringId(string id)
        {
            return _gymSystemDbContext.AspNetUsers.Where(x=> x.Id == id).FirstOrDefault();
        }

        public List<AspNetUser> GetAllWithRoles()
        {
            return _gymSystemDbContext.AspNetUsers.Include(x => x.Roles).ToList();
        }

        
    }
}

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
    public class RolesRepository : Repository<AspNetRole>, IRolesRepository
    {
        protected readonly GymSystemDBContext _gymSystemlDbContext;

        public RolesRepository(GymSystemDBContext context) : base(context)
        {
            _gymSystemlDbContext = context;
        }

        public AspNetRole? GetByStringId(string id)
        {
            return _gymSystemlDbContext.AspNetRoles.FirstOrDefault(x => x.Id == id);
        }

        public AspNetRole? GetByUserId(string userId)
        {
            return _gymSystemlDbContext.AspNetUsers.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId)?.Roles.FirstOrDefault();
        }
    }
}

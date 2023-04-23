using GymManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.App.Interfaces
{
    public interface IRolesRepository : IRepository<AspNetRole>
    {
        AspNetRole? GetByUserId(string userId);
        AspNetRole? GetByStringId(string id);
    }
}

using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Models.KeyValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.App.Implementations
{
    public class SelectListService : ISelectListService
    {
        private readonly IRolesRepository rolesRepository;
        private readonly IUserRepository userRepository;

        public SelectListService(IRolesRepository rolesRepository, IUserRepository userRepository)
        {
            this.rolesRepository = rolesRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<KeyValueItem> GetRolesKeysValues()
        {
            try
            {
                var roles = rolesRepository.GetAll().ToList();
                var result = roles.Select(role => new KeyValueItem()
                {
                    SKey = role.Id,
                    Value = role.Name ?? ""
                });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<KeyValueItem> GetTypesKeysValues()
        {
            try
            {
                var types = userRepository.GetAll().ToList();
                var result = types.Select(type => new KeyValueItem()
                {
                    Value = type.Name ?? "",
                });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

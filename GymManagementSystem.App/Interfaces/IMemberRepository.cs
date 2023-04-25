using GymManagementSystem.Data.Entities;
using GymManagementSystem.Models;

namespace GymManagementSystem.App.Interfaces
{
    public interface IMemberRepository : IRepository<Antaresimi>
    {
        List<MembersViewModel> GetAllMembers();
        AspNetUser GetMember(string name);

        Antaresimi? GetByStringId(string id);

    }
}

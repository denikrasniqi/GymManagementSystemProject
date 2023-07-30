using Arch.EntityFrameworkCore;
using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Context;
using GymManagementSystem.Data.Entities;
using GymManagementSystem.Models;


namespace GymManagementSystem.App.Implementations
{
    public class MemberRepository : Repository<Antaresimi>, IMemberRepository
    {
        protected readonly GymSystemDBContext _gymSystemDbContext;
        public MemberRepository(GymSystemDBContext gymSystemDbContext) : base(gymSystemDbContext)
        {
            _gymSystemDbContext = gymSystemDbContext;
        }


        public List<Antaresimi> GetAllMembers()
        {
            var members = _gymSystemDbContext.Antaresimis.Include(x=>x).ToList();
            return new List<Antaresimi>();
        }
        /// <summary>
        /// GetMember by Name
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public AspNetUser GetMember(string name)
        {
            var user = _gymSystemDbContext.AspNetUsers.Where(x => x.Name == name).FirstOrDefault();
            return user!;
        }
        public Antaresimi? GetByStringId(string id)
        {
            return _gymSystemDbContext.Antaresimis.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();
        }

        public Antaresimi? GetByStringUserId(string id)
        {
            return _gymSystemDbContext.Antaresimis.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();
        }

        public int GetMemberIdByUserId(string userId)
        {
            var member = _gymSystemDbContext.Antaresimis.Where(x => x.UserId == userId).FirstOrDefault();
            if(member == null)
                return 0;

            return member.Id;
        }
    }
}

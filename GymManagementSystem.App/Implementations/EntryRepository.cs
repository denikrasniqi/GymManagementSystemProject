using GymManagementSystem.App.Interfaces;
using GymManagementSystem.Data.Context;
using GymManagementSystem.Data.Entities;

namespace GymManagementSystem.App.Implementations
{
    public class EntryRepository : Repository<Entry> ,IEntryRepository
    {
        protected readonly GymSystemDBContext _gymSystemDbContext;

        public EntryRepository(GymSystemDBContext gymSystemDbContext) : base(gymSystemDbContext)
        {
            _gymSystemDbContext = gymSystemDbContext;
        }

        /// <summary>
        /// Gets list of entries
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Entry> GetEntries()
        {
            return _gymSystemDbContext.Entries.ToList();
        }
        public bool SetEntryForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool SetOutForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Antaresimi GetMember(string name)
        {
            var entry = _gymSystemDbContext.Antaresimis.Where(x => x.Name == name).FirstOrDefault();
            return entry!;
        }
        public Entry? GetByStringId(string id)
        {
            return _gymSystemDbContext.Entries.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();
        }
    }
}

using GymManagementSystem.Data.Entities;

namespace GymManagementSystem.App.Interfaces
{
    public interface IEntryRepository
    {
        //Get list of entries for user
        public List<Entry> GetEntries();
        //Set entry for user id
        public bool SetEntryForUser(string userId);

        //Set out for user id
        public bool SetOutForUser(string userId);

        

    }
}

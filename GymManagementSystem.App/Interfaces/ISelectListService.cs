using GymManagementSystem.Models.KeyValues;

namespace GymManagementSystem.App.Interfaces
{
    public interface ISelectListService
    {
        IEnumerable<KeyValueItem> GetRolesKeysValues();
        IEnumerable<KeyValueItem> GetTypesKeysValues();
    }
}

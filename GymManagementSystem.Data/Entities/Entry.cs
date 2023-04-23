using System;
using System.Collections.Generic;

namespace GymManagementSystem.Data.Entities
{
    public partial class Entry
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public bool IsEntry { get; set; }
        public DateTime Date { get; set; }
    }
}

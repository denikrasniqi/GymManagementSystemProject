using System;
using System.Collections.Generic;

namespace GymManagementSystem.Data.Entities
{
    public partial class Antaresimi
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime StartDate { get; set; }
        public string Statusi { get; set; } = null!;
        public int Qmimi { get; set; }
        public string Antaresimi1 { get; set; } = null!;

        public virtual AspNetUser User { get; set; } = null!;
    }
}

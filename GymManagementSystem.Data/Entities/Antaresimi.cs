using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data.Entities
{
    [Table("Antaresimi")]
    public partial class Antaresimi
    {
        [Key]
        public int Id { get; set; }
        [StringLength(450)]
        public string UserId { get; set; } = null!;
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Surname { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [StringLength(50)]
        public string Statusi { get; set; } = null!;
        public int Qmimi { get; set; }
        [Column("Antaresimi")]
        [StringLength(50)]
        public string Antaresimi1 { get; set; } = null!;
    }
}

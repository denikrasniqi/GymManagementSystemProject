using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data.Entities
{
    [Table("Hyrjet")]
    public partial class Hyrjet
    {
        [Key]
        public int Id { get; set; }
        public int AntariId { get; set; }
        [StringLength(10)]
        public string NrHyrjeve { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime KohaHyrjes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime KohaDaljes { get; set; }

        [ForeignKey("AntariId")]
        [InverseProperty("Hyrjets")]
        public virtual Antaresimi Antari { get; set; } = null!;
    }
}

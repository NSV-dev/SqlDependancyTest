using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("gender")]
    public partial class Gender
    {
        public Gender()
        {
            Personalities = new HashSet<Personality>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("gender")]
        public string Gender1 { get; set; }

        [InverseProperty(nameof(Personality.Gender))]
        public virtual ICollection<Personality> Personalities { get; set; }
    }
}

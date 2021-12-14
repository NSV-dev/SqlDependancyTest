using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("admin")]
    public partial class Admin
    {
        public Admin()
        {
            Companies = new HashSet<Company>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("login")]
        public string Login { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Column("roleID")]
        public int RoleId { get; set; }
        [Column("personalityID")]
        public int PersonalityId { get; set; }

        [ForeignKey(nameof(PersonalityId))]
        [InverseProperty("Admins")]
        public virtual Personality Personality { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Admins")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(Company.Admin))]
        public virtual ICollection<Company> Companies { get; set; }
    }
}

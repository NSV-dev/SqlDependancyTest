using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("emp")]
    public partial class Emp
    {
        public Emp()
        {
            Tasks = new HashSet<Task>();
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
        [Column("personalityID")]
        public int PersonalityId { get; set; }
        [Column("roleID")]
        public int RoleId { get; set; }
        [Column("companyID")]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Emps")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(PersonalityId))]
        [InverseProperty("Emps")]
        public virtual Personality Personality { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Emps")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(Task.Emp))]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

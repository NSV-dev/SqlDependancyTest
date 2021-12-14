using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("personality")]
    public partial class Personality
    {
        public Personality()
        {
            Admins = new HashSet<Admin>();
            Emps = new HashSet<Emp>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("firstname")]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname")]
        public string Lastname { get; set; }
        [Column("middlename")]
        public string Middlename { get; set; }
        [Column("genderID")]
        public int GenderId { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("phone")]
        public string Phone { get; set; }
        [Column("dateofbirth", TypeName = "date")]
        public DateTime Dateofbirth { get; set; }
        [Required]
        [Column("address")]
        public string Address { get; set; }
        [Column("info")]
        public string Info { get; set; }

        [ForeignKey(nameof(GenderId))]
        [InverseProperty("Personalities")]
        public virtual Gender Gender { get; set; }
        [InverseProperty(nameof(Admin.Personality))]
        public virtual ICollection<Admin> Admins { get; set; }
        [InverseProperty(nameof(Emp.Personality))]
        public virtual ICollection<Emp> Emps { get; set; }
    }
}

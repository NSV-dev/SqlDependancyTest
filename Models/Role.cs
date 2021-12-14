using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("roles")]
    public partial class Role
    {
        public Role()
        {
            Admins = new HashSet<Admin>();
            Emps = new HashSet<Emp>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("role")]
        public string Role1 { get; set; }

        [InverseProperty(nameof(Admin.Role))]
        public virtual ICollection<Admin> Admins { get; set; }
        [InverseProperty(nameof(Emp.Role))]
        public virtual ICollection<Emp> Emps { get; set; }
    }
}

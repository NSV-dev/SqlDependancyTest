using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("company")]
    public partial class Company
    {
        public Company()
        {
            Emps = new HashSet<Emp>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("compname")]
        public string Compname { get; set; }
        [Column("adminID")]
        public int AdminId { get; set; }
        [Column("code")]
        public int Code { get; set; }

        [ForeignKey(nameof(AdminId))]
        [InverseProperty("Companies")]
        public virtual Admin Admin { get; set; }
        [InverseProperty(nameof(Emp.Company))]
        public virtual ICollection<Emp> Emps { get; set; }
    }
}

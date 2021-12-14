using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("tasks")]
    public partial class Task
    {
        public Task()
        {
            Reports = new HashSet<Report>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("taskname")]
        public string Taskname { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("empID")]
        public int EmpId { get; set; }
        [Column("expired")]
        public bool? Expired { get; set; }
        [Column("verification")]
        public bool? Verification { get; set; }

        [ForeignKey(nameof(EmpId))]
        [InverseProperty("Tasks")]
        public virtual Emp Emp { get; set; }
        [InverseProperty(nameof(Report.Task))]
        public virtual ICollection<Report> Reports { get; set; }
    }
}

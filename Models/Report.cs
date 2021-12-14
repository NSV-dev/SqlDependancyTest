using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SqlDependancyTest.Models
{
    [Table("reports")]
    public partial class Report
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("taskID")]
        public int TaskId { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [ForeignKey(nameof(TaskId))]
        [InverseProperty("Reports")]
        public virtual Task Task { get; set; }
    }
}

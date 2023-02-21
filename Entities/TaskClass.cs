using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Entities
{
    [Table(Name = "TASKS")]
    public class TaskClass
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
        [Column]
        public string? Name { get; set; }
        [Column]
        public string? Description { get; set; }
        [Column (Name = "DateOnly")]
        public DateTime Date { get; set; }
        [Column]
        public string? issuedby { get; set; }
        [Column]
        public string? Task_Status { get; set; }

        public TaskClass() { }

        public TaskClass(int Id, string Name, string Description, DateTime Date, string issuedby, string Task_Status)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Date = Date;
            this.issuedby = issuedby;
            this.Task_Status = Task_Status;
        }

        public TaskClass(string Name, string Description, DateTime Date, string issuedby, string Task_Status)
        {
            this.Name = Name;
            this.Description = Description;
            this.Date = Date;
            this.issuedby = issuedby;
            this.Task_Status = Task_Status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Entities
{
    [Table(Name = "Stay_loged")]
    public class Stayloged
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
        [Column]
        public string? User_Name { get; set; }

        public Stayloged() { }

    }
}

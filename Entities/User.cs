using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp4.Entities
{
    [Table(Name = "USERS")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
        [Column]
        public string? User_Name { get; set; } = null;
        [Column]
        public string? User_Email { get; set; }
        [Column]
        public string? User_Password { get; set; }
        [Column]
        public byte[]? User_Avatar { get; set; } = null;
        [Column]
        public string? User_Status { get; set; }

        public User() { }

        public User(int id, string? user_Name, string? user_Email, string? user_Password, byte[]? user_Avatar, string? user_Status)
        {
            Id = id;
            User_Name = user_Name;
            User_Email = user_Email;
            User_Password = user_Password;
            User_Avatar = user_Avatar;
            User_Status = user_Status;
        }

        public User(string? user_Name, string? user_Email, string? user_Password, byte[]? user_Avatar, string? user_Status)
        {
            User_Name = user_Name;
            User_Email = user_Email;
            User_Password = user_Password;
            User_Avatar = user_Avatar;
            User_Status = user_Status;
        }
    }
}

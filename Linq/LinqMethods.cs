using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp4.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp4.Linq
{
    public static class LinqMethods
    {
        public static string connectionstring = "Data Source = AZURE-GRIPPEN; Initial Catalog = USERS_TRELLO ;Trusted_Connection=True; TrustServerCertificate = True ";

        public static void additem(User g)
        {
            DataContext db = new DataContext(connectionstring);
            db.GetTable<User>().InsertOnSubmit(g);
            db.SubmitChanges();
        }

        public static User? selectuser(string username, string password)
        {
            DataContext db = new DataContext(connectionstring);
            var list = db.GetTable<User>().ToList<User>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].User_Name  == username && list[i].User_Password == password)
                    return list[i];
            }
            return null;
        }

        public static User? selectuserOnlyName()
        {
            try
            {
                DataContext db = new DataContext(connectionstring);
                var getname = db.GetTable<Stayloged>().ToList<Stayloged>();
                string? username = getname[0].User_Name;
                var list = db.GetTable<User>().ToList<User>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].User_Name == username)
                        return list[i];
                }
                return null;
            }
            catch { return null; }
           
        }

        public static void initimage(ref BitmapImage img, byte[] data)
        {
            img.BeginInit();
            img.StreamSource = new MemoryStream(data);
            img.EndInit();
        }
    }
}

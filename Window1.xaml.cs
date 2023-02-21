using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp4.Linq;
using WpfApp4.Entities;
using System.Drawing;
using System.IO;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public BitmapImage bitmapImage { get; set; } = new BitmapImage();
        public static Window1? w1;
        public Window1()
        {
            this.DataContext = this;
            InitializeComponent();
            Select_Status.SelectedIndex = 0;
            SelectEmail.SelectedIndex = 0;
            User_Password_Text.Visibility = Visibility.Hidden;
            Sign_up_canvas.Visibility = Visibility.Hidden;
            User_Password_Text_login.Visibility = Visibility.Hidden;
            w1 = this;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void Signup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mg = new MainWindow();
                string useremail = "";
                string userstatus = "";
                switch (SelectEmail.SelectedIndex)
                {
                    case 0:
                        useremail = UserEmail.Text + "@gmail.com";
                        break;
                    case 1:
                        useremail = UserEmail.Text + "@inbox.com";
                        break;
                    case 2:
                        useremail = UserEmail.Text + "@yahoo.com";
                        break;
                }
                switch (Select_Status.SelectedIndex)
                {
                    case 0:
                        userstatus = "Developer";
                        break;
                    case 1:
                        userstatus = "Tester";
                        break;
                    case 2:
                        userstatus = "Teamlead";
                        break;
                }
                //byte[] data = File.ReadAllBytes("C:\\Users\\Zero\\Pictures\\BlackScreen.png");
                User temp_u = new User(UserName.Text, useremail, User_Password.Password, null, userstatus);
                MainWindow.mn.get_current_user = temp_u;
                if (Staylogedxs.IsChecked == true)
                {
                    DataContext db = new DataContext(LinqMethods.connectionstring);
                    var t = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in t)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    Stayloged stayloged = new Stayloged();
                    stayloged.User_Name = MainWindow.mn.get_current_user.User_Name;
                    db.GetTable<Stayloged>().InsertOnSubmit(stayloged);
                    db.SubmitChanges();
                }
                else
                {
                    DataContext db = new DataContext(LinqMethods.connectionstring);
                    var t = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in t)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
                }
                LinqMethods.additem(temp_u);
                mg.Show();
                Close();
            }
            catch { MessageBox.Show("Something went wrong, try again"); }
           
        }


        private void UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserEmail.Text = UserEmail.Text.ToLower().Trim();
            UserEmail.SelectionStart = UserEmail.Text.Length;

        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Select_Status.IsDropDownOpen == true)
                {
                    Select_Status.IsDropDownOpen = false;
                }
                else
                {
                    Select_Status.IsDropDownOpen = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectEmail.IsDropDownOpen == true)
                {
                    SelectEmail.IsDropDownOpen = false;
                }
                else
                {
                    SelectEmail.IsDropDownOpen = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool isvisible1 = false;
        bool isvisible2 = false;
        private void HideShow_Click(object sender, RoutedEventArgs e)
        {
            if (!isvisible1)
            {
                User_Password.Visibility = Visibility.Hidden;
                User_Password_Text.Text = User_Password.Password;
                User_Password_Text.Visibility = Visibility.Visible;
                isvisible1 = true;
                HideShowIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.EyeSlash;
            }
            else
            {
                User_Password_Text.Visibility = Visibility.Hidden;
                User_Password.Password = User_Password_Text.Text;
                User_Password.Visibility = Visibility.Visible;
                isvisible1 = false;
                HideShowIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Eye;
            }
        }

        private void CloseEditProductWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Staylogedx.IsChecked == false)
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);
                var t = db.GetTable<Stayloged>().ToList<Stayloged>();
                foreach (var temp in t)
                {
                    db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                }
                db.SubmitChanges();
            }
            Close();
        }

        private void HideShow_login_Click(object sender, RoutedEventArgs e)
        {
            if (!isvisible2)
            {
                User_Password_login.Visibility = Visibility.Hidden;
                User_Password_Text_login.Text = User_Password_login.Password;
                User_Password_Text_login.Visibility = Visibility.Visible;
                isvisible2 = true;
                HideShowIcon_login.Icon = FontAwesome.WPF.FontAwesomeIcon.EyeSlash;
            }
            else
            {
                User_Password_Text_login.Visibility = Visibility.Hidden;
                User_Password_login.Password = User_Password_Text_login.Text;
                User_Password_login.Visibility = Visibility.Visible;
                isvisible2 = false;
                HideShowIcon_login.Icon = FontAwesome.WPF.FontAwesomeIcon.Eye;
            }
        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mg = new MainWindow();

            MainWindow.mn.get_current_user = LinqMethods.selectuser(UserName_login.Text, User_Password_login.Password);
            if (MainWindow.mn.get_current_user != null)
            {
                if (Staylogedx.IsChecked == true)
                {
                    DataContext db = new DataContext(LinqMethods.connectionstring);
                    var t = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in t)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
                    Stayloged stayloged = new Stayloged();
                    stayloged.User_Name = MainWindow.mn.get_current_user.User_Name;
                    db.GetTable<Stayloged>().InsertOnSubmit(stayloged);
                    db.SubmitChanges();
                }
                else
                {
                    DataContext db = new DataContext(LinqMethods.connectionstring);
                    var t = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in t)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
                }
                mg.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock? textBlock = sender as TextBlock;
            textBlock.Foreground = System.Windows.Media.Brushes.White;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock? textBlock = sender as TextBlock;
            textBlock.Foreground = System.Windows.Media.Brushes.Magenta;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock? textBlock = sender as TextBlock;
            textBlock.Foreground = System.Windows.Media.Brushes.Cyan;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Login.Visibility = Visibility.Hidden;
            Sign_up_canvas.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            Login.Visibility = Visibility.Visible;
            Sign_up_canvas.Visibility = Visibility.Hidden;
        }

        private void Sign_up_canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UserName.Text = "";

            User_Password.Password = "";
            if (Staylogedx.IsChecked == false)
            {
                UserName_login.Text = "";
                User_Password_login.Password = "";
                User_Password_Text_login.Text = "";
                Staylogedx.IsChecked = false;
                isvisible2 = false;
                HideShowIcon_login.Icon = FontAwesome.WPF.FontAwesomeIcon.Eye;
            }

            User_Password_Text.Text = "";

            UserEmail.Text = "";
            Select_Status.SelectedIndex = 0;
            SelectEmail.SelectedIndex = 0;
            isvisible1 = false;
            
            HideShowIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Eye;
            
            Staylogedxs.IsChecked = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                User? temp = LinqMethods.selectuserOnlyName();
                if (temp != null && !MainWindow.wasopened)
                {
                    Login.Visibility = Visibility.Hidden;
                    MainWindow mg = new MainWindow();
                    MainWindow.mn.get_current_user = temp;
                    mg.Show();
                    Close();
                }
                else if (temp != null && MainWindow.wasopened)
                {
                    UserName_login.Text = MainWindow.mn.get_current_user.User_Name;
                    User_Password_login.Password = MainWindow.mn.get_current_user.User_Password;
                    User_Password_Text_login.Text = MainWindow.mn.get_current_user.User_Password;
                    Staylogedx.IsChecked = true;
                }
            }
            catch { }
            

        }
    }

}


using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Threading;
using WpfApp4.Entities;
using WpfApp4.Linq;

namespace WpfApp4
{

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public static MainWindow? mn;
        User? current_user = null;
        public static bool wasopened = false;
        public User? get_current_user { get { return current_user; } set { current_user = value; } } 
        public BitmapImage bitmapImage { get; set; } = new BitmapImage();
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            timer.Tick += tick_event;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            dir = dir.Parent?.Parent?.Parent;
            Account_Canvas.Visibility = Visibility.Hidden;
            Menu_Drop_Canvas.Visibility = Visibility.Hidden;
            Manage_Account_Canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
            Discover_Canvas_Tester.Visibility = Visibility.Hidden;
            Discover_Canvas_Developer.Visibility = Visibility.Hidden;
            Default_Canvas.Visibility = Visibility.Hidden;
            Add_Task_Canvas.Visibility = Visibility.Hidden;
            Tester_work_canvas.Visibility = Visibility.Hidden;
            Teamlead_work_canvas.Visibility = Visibility.Hidden;
            this.Closing += closing_event;
            wasopened = true;
            mn = this;

            DataContext db = new DataContext(LinqMethods.connectionstring);
            var t = db.GetTable<TaskClass>().ToList<TaskClass>();
            ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_review = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_completed = new ObservableCollection<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Task_Status == "issued")
                    tp.Add(t[i]);
                if (t[i].Task_Status == "review")
                    tp_review.Add(t[i]);
                if (t[i].Task_Status == "completed")
                    tp_completed.Add(t[i]);
            }
            Issued_tasks.ItemsSource = tp;
            Tasks_in_progress.ItemsSource = tp;
            Tasks_under_review.ItemsSource = tp_review;
            Finished_Tasks.ItemsSource = tp_completed;
            Tasks_under_review_Tester.ItemsSource = tp_review;
            Finished_Tasks_Tester.ItemsSource = tp_completed;
            Finished_Tasks_Developer.ItemsSource = tp_completed;
            Tasks_under_review_Developer.ItemsSource = tp;

        }

        private void closing_event(object? sender, CancelEventArgs e)
        {
            try
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);

                if (db.GetTable<Stayloged>().ToList<Stayloged>().Count == 1)
                {
                    var ts = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in ts)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
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
            }
            catch { }
        }

        private void tick_event(object? sender, EventArgs e)
        {
            Time_Label.Content = DateTime.Now.ToLongTimeString();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if(Menu_Drop_Canvas.Visibility == Visibility.Visible)
            {
                Menu_Drop_Canvas.Visibility = Visibility.Hidden;
            }
            else
            {
                Menu_Drop_Canvas.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(Account_Canvas.Visibility == Visibility.Hidden)
                Account_Canvas.Visibility = Visibility.Visible;
            else
                Account_Canvas.Visibility = Visibility.Hidden;

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);
                
                if (db.GetTable<Stayloged>().ToList<Stayloged>().Count == 1)
                {
                    var ts = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in ts)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
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
                Window1 wn = new Window1();
                wn.Show();
                Close();
            }
            catch (Exception ex)
            {
               
            }
           

        }

        Border? imageborder = new Border();

        private void border_Initialized_1(object sender, EventArgs e)
        {
            imageborder = sender as Border;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new System.IO.MemoryStream(current_user.User_Avatar);
                bitmapImage.EndInit();
            }
            catch
            {
                bitmapImage.UriSource = new Uri(dir.FullName + "\\Resources\\Avatar.png");
                bitmapImage.EndInit();
            }
            
           
            User_Email.ToolTip = current_user.User_Email;
            User_Name.Content = current_user.User_Name;
            UserTypeLabel.Content = current_user.User_Status;
            M_Email.Content = current_user.User_Email;
            M_Status.Content = current_user.User_Status;
            M_UserName.Content = current_user.User_Name;
            int k = 0;
            for (int i = 0; i < current_user.User_Email.Length; i++)
            {
                if (current_user.User_Email[i] == '@')
                {
                    k = i - 1;
                    break;
                }

            }
            if (k < 6)
            {
                User_Email.Content = current_user.User_Email;
            }
            else
            {
                string s = "";
                k = 0;
                for (int i = 0; i < current_user.User_Email.Length; i++)
                {
                    if (current_user.User_Email[i] == '@')
                    { 
                        k = i - 1;
                        break;
                    }
                        
                }
                for (int i = 0; i < k-2; i++)
                {
                    s += current_user.User_Email[i];
                }
                s += "...";
                for (int i = k+1; i < current_user.User_Email.Length; i++)
                {
                    s += current_user.User_Email[i];
                }
                User_Email.Content = s;
            }
            Discover_Current_User.Content = "Learn more " + current_user.User_Status;
            switch (current_user.User_Status)
            {
                case "Developer":
                    Tester_work_canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Developer_work_canvas.Visibility = Visibility.Visible;
                    break;
                case "Tester":
                    Tester_work_canvas.Visibility = Visibility.Visible;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Developer_work_canvas.Visibility = Visibility.Hidden;
                    break;
                case "Teamlead":
                    Tester_work_canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Visible;
                    Developer_work_canvas.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Closeapp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);
               
                if (db.GetTable<Stayloged>().ToList<Stayloged>().Count == 1)
                {
                    var ts = db.GetTable<Stayloged>().ToList<Stayloged>();
                    foreach (var temp in ts)
                    {
                        db.GetTable<Stayloged>().DeleteOnSubmit(temp);
                    }
                    db.SubmitChanges();
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
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void Manage_Account_Click(object sender, RoutedEventArgs e)
        {
            Manage_Account_Canvas.Visibility = Visibility.Visible;
            Account_Canvas.Visibility = Visibility.Hidden;
            Menu_Drop_Canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
            Discover_Canvas_Tester.Visibility = Visibility.Hidden;
            Discover_Canvas_Developer.Visibility = Visibility.Hidden;
            Default_Canvas.Visibility = Visibility.Hidden;
            Add_Task_Canvas.Visibility = Visibility.Hidden;
            Tester_work_canvas.Visibility = Visibility.Hidden;
            Teamlead_work_canvas.Visibility = Visibility.Hidden;

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(Update_Status.IsDropDownOpen)
            {
                Update_Status.IsDropDownOpen = false;
            }
            else
            {
                Update_Status.IsDropDownOpen = true;
            }
        }

        private void Apply_new_Status_Click(object sender, RoutedEventArgs e)
        {
            if(Update_Status.SelectedIndex != -1)
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);
                var temp_users = db.GetTable<User>().ToList<User>();
                for (int i = 0; i < temp_users.Count; i++)
                {
                    if (temp_users[i].Id == current_user.Id)
                    {
                        switch (Update_Status.SelectedIndex)
                        {
                            case 0:
                                temp_users[i].User_Status = "Developer";
                                current_user.User_Status = "Developer";
                                User_Email.ToolTip = current_user.User_Email;
                                User_Name.Content = current_user.User_Name;
                                UserTypeLabel.Content = current_user.User_Status;
                                M_Email.Content = current_user.User_Email;
                                M_Status.Content = current_user.User_Status;
                                M_UserName.Content = current_user.User_Name;
                                Discover_Current_User.Content = "Learn more " + current_user.User_Status;
                                break;
                            case 1:
                                temp_users[i].User_Status = "Tester";
                                current_user.User_Status = "Tester";
                                User_Email.ToolTip = current_user.User_Email;
                                User_Name.Content = current_user.User_Name;
                                UserTypeLabel.Content = current_user.User_Status;
                                M_Email.Content = current_user.User_Email;
                                M_Status.Content = current_user.User_Status;
                                M_UserName.Content = current_user.User_Name;
                                Discover_Current_User.Content = "Learn more " + current_user.User_Status;
                                break;
                            case 2:
                                temp_users[i].User_Status = "Teamlead";
                                current_user.User_Status = "Teamlead";
                                User_Email.ToolTip = current_user.User_Email;
                                User_Name.Content = current_user.User_Name;
                                UserTypeLabel.Content = current_user.User_Status;
                                M_Email.Content = current_user.User_Email;
                                M_Status.Content = current_user.User_Status;
                                M_UserName.Content = current_user.User_Name;
                                Discover_Current_User.Content = "Learn more " + current_user.User_Status;
                                break;
                        }
                        db.SubmitChanges();
                    }
                }
            }
            
        }

        private void ChangeImageButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImageButton.BorderBrush = Brushes.Lime;

            imgIconx.Foreground = Brushes.Lime;
        }

        private void ChangeImageButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImageButton.BorderBrush = Brushes.Cyan;

            imgIconx.Foreground = Brushes.Cyan;
        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "png; jpg|*.png; *.jpg";
                ofd.ShowDialog();
                current_user.User_Avatar = File.ReadAllBytes(ofd.FileName);
                BitmapImage bp = new BitmapImage();
                bp.BeginInit();
                bp.StreamSource = new MemoryStream(current_user.User_Avatar);
                bp.EndInit();
                texts.Background = new ImageBrush(bp);
                smallaccimg.Background = new ImageBrush(bp);
                mediumaccimg.Background = new ImageBrush(bp);
                DataContext db = new DataContext(LinqMethods.connectionstring);
                var temp_users = db.GetTable<User>().ToList<User>();
                for (int x = 0; x < temp_users.Count; x++)
                {
                    if (temp_users[x].Id == current_user.Id)
                    {
                        temp_users[x].User_Avatar = current_user.User_Avatar;
                        db.SubmitChanges();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ChangeUserNameTxt.Text.Trim().Length > 1)
            {
                DataContext db = new DataContext(LinqMethods.connectionstring);
                var temp_users = db.GetTable<User>().ToList<User>();
                int k = 0;
                for (int i = 0; i < temp_users.Count; i++)
                {
                    if (temp_users[i].User_Name == ChangeUserNameTxt.Text)
                    {
                        ChangeUserNameTxt.Text = "";
                        ChangeUserNameTxt.BorderBrush = Brushes.Red;
                        k = 1;
                        break;
                    }
                }
                if (k == 0)
                {
                    for (int x = 0; x < temp_users.Count; x++)
                    {
                        if (temp_users[x].Id == current_user.Id)
                        {
                            
                            temp_users[x].User_Name = ChangeUserNameTxt.Text;
                            current_user.User_Name = ChangeUserNameTxt.Text;
                            User_Email.ToolTip = current_user.User_Email;
                            User_Name.Content = current_user.User_Name;
                            UserTypeLabel.Content = current_user.User_Status;
                            M_Email.Content = current_user.User_Email;
                            M_Status.Content = current_user.User_Status;
                            M_UserName.Content = current_user.User_Name;

                            k = 0;
                            for (int i = 0; i < current_user.User_Email.Length; i++)
                            {
                                if (current_user.User_Email[i] == '@')
                                {
                                    k = i - 1;
                                    break;
                                }

                            }
                            if (k < 6)
                            {
                                User_Email.Content = current_user.User_Email;
                            }
                            else
                            {
                                string s = "";
                                k = 0;
                                for (int i = 0; i < current_user.User_Email.Length; i++)
                                {
                                    if (current_user.User_Email[i] == '@')
                                    {
                                        k = i - 1;
                                        break;
                                    }

                                }
                                for (int i = 0; i < k - 2; i++)
                                {
                                    s += current_user.User_Email[i];
                                }
                                s += "...";
                                for (int i = k + 1; i < current_user.User_Email.Length; i++)
                                {
                                    s += current_user.User_Email[i];
                                }
                                User_Email.Content = s;
                            }
                            Discover_Current_User.Content = "Learn more " + current_user.User_Status;
                            break;
                        }
                    }
                    db.SubmitChanges();
                }
            }
            else
            {
                ChangeUserNameTxt.Text = "";
                ChangeUserNameTxt.BorderBrush = Brushes.Red;
            }
            
        }

        private void ChangeUserNameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeUserNameTxt.BorderBrush = Brushes.Cyan;
        }

        private void Home_Page_Click(object sender, RoutedEventArgs e)
        {
            Manage_Account_Canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
            Discover_Canvas_Tester.Visibility = Visibility.Hidden;
            Discover_Canvas_Developer.Visibility = Visibility.Hidden;
            Add_Task_Canvas.Visibility = Visibility.Hidden;


            switch (current_user.User_Status)
            {
                case "Developer":
                    Tester_work_canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Developer_work_canvas.Visibility = Visibility.Visible;
                    break;
                case "Tester":
                    Tester_work_canvas.Visibility = Visibility.Visible;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Developer_work_canvas.Visibility = Visibility.Hidden;
                    break;
                case "Teamlead":
                    Tester_work_canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Visible;
                    Developer_work_canvas.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Discover_Teamlead_Click(object sender, RoutedEventArgs e)
        {
            Manage_Account_Canvas.Visibility = Visibility.Hidden;
            Teamlead_work_canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Visible;
            Discover_Canvas_Tester.Visibility = Visibility.Hidden;
            Discover_Canvas_Developer.Visibility = Visibility.Hidden;
            Add_Task_Canvas.Visibility = Visibility.Hidden;
        }

        private void Discover_Tester_Click(object sender, RoutedEventArgs e)
        {
            Manage_Account_Canvas.Visibility = Visibility.Hidden;
            Teamlead_work_canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
            Discover_Canvas_Tester.Visibility = Visibility.Visible;
            Discover_Canvas_Developer.Visibility = Visibility.Hidden;
            Add_Task_Canvas.Visibility = Visibility.Hidden;
        }

        private void Discover_Developer_Click(object sender, RoutedEventArgs e)
        {
            Manage_Account_Canvas.Visibility = Visibility.Hidden;
            Teamlead_work_canvas.Visibility = Visibility.Hidden;
            Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
            Discover_Canvas_Tester.Visibility = Visibility.Hidden;
            Discover_Canvas_Developer.Visibility = Visibility.Visible;
            Add_Task_Canvas.Visibility = Visibility.Hidden;
        }

        private void Discover_Current_User_Click(object sender, RoutedEventArgs e)
        {
            switch(current_user.User_Status)
            {
                case "Developer":
                    Manage_Account_Canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
                    Discover_Canvas_Tester.Visibility = Visibility.Hidden;
                    Discover_Canvas_Developer.Visibility = Visibility.Visible;
                    Add_Task_Canvas.Visibility = Visibility.Hidden;
                    break;
                case "Tester":
                    Manage_Account_Canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Discover_Canvas_Teamlead.Visibility = Visibility.Hidden;
                    Discover_Canvas_Tester.Visibility = Visibility.Visible;
                    Discover_Canvas_Developer.Visibility = Visibility.Hidden;
                    Add_Task_Canvas.Visibility = Visibility.Hidden;
                    break;
                case "Teamlead":
                    Manage_Account_Canvas.Visibility = Visibility.Hidden;
                    Teamlead_work_canvas.Visibility = Visibility.Hidden;
                    Discover_Canvas_Teamlead.Visibility = Visibility.Visible;
                    Discover_Canvas_Tester.Visibility = Visibility.Hidden;
                    Discover_Canvas_Developer.Visibility = Visibility.Hidden;
                    Add_Task_Canvas.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Issue_task_button_Click(object sender, RoutedEventArgs e)
        {
            Add_Task_Canvas.Visibility = Visibility.Visible;
            View_Model_Task_Date.Content = DateTime.Now.ToShortDateString();
            View_Model_Task_issuedby.Content = current_user.User_Name;
        }

        private void Add_Task_Name_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            View_Model_Task_Name.Content = Add_Task_Name_txt.Text;
        }
        string s = "";
        TextBox? temp = new TextBox();
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            temp = sender as TextBox;
            s = temp.Text;
            View_Model_Task_Description.Text = s;
        }


        private void Add_Task_button_Click(object sender, RoutedEventArgs e)
        {
            if(View_Model_Task_Name.Content != null && View_Model_Task_Description.Text != null && View_Model_Task_Name.Content.ToString().Trim().Length > 0 && View_Model_Task_Description.Text.Trim().Length > 0)
            {
                try
                {
                    DataContext db = new DataContext(LinqMethods.connectionstring);
                    db.GetTable<TaskClass>().InsertOnSubmit(new TaskClass(View_Model_Task_Name.Content.ToString(), View_Model_Task_Description.Text, DateTime.Now, View_Model_Task_issuedby.Content.ToString(), "issued"));
                    db.SubmitChanges();
                    var t = db.GetTable<TaskClass>().ToList<TaskClass>();
                    ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
                    for (int i = 0; i < t.Count; i++)
                    {
                        if (t[i].Task_Status == "issued")
                            tp.Add(t[i]);
                    }
                    Issued_tasks.ItemsSource = tp;
                    Tasks_in_progress.ItemsSource = tp;
                    MessageBox.Show("Task was issued successfully");
                    Add_Task_Name_txt.Text = "";
                    Add_Task_desc_txt.Text = "";
                    Add_Task_Canvas.Visibility = Visibility.Hidden;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show("Pls enter description and task name first");
            }
            
        }

        private void Teamlead_work_canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataContext db = new DataContext(LinqMethods.connectionstring);
            var t = db.GetTable<TaskClass>().ToList<TaskClass>();
            ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_review = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_completed = new ObservableCollection<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Task_Status == "issued")
                    tp.Add(t[i]);
                if (t[i].Task_Status == "review")
                    tp_review.Add(t[i]);
                if (t[i].Task_Status == "completed")
                    tp_completed.Add(t[i]);
            }
            Issued_tasks.ItemsSource = tp;
            Tasks_in_progress.ItemsSource = tp;
            Tasks_under_review.ItemsSource = tp_review;
            Finished_Tasks.ItemsSource = tp_completed;
            Tasks_under_review_Tester.ItemsSource = tp_review;
            Finished_Tasks_Tester.ItemsSource = tp_completed;
            Finished_Tasks_Developer.ItemsSource = tp_completed;
            Tasks_under_review_Developer.ItemsSource = tp;
        }

        private void Add_Task_desc_txt_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            View_Model_Task_Description.Text = Add_Task_desc_txt.Text;
        }


        private void AproveButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            DataContext db = new DataContext(LinqMethods.connectionstring);
            var t = db.GetTable<TaskClass>().ToList<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Id == Convert.ToInt32(b.Content))
                {
                    t[i].Task_Status = "completed";
                    db.SubmitChanges();
                    break;
                }
            }
            t = db.GetTable<TaskClass>().ToList<TaskClass>();
            ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_review = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_completed = new ObservableCollection<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Task_Status == "issued")
                    tp.Add(t[i]);
                if (t[i].Task_Status == "review")
                    tp_review.Add(t[i]);
                if (t[i].Task_Status == "completed")
                    tp_completed.Add(t[i]);
            }
            Issued_tasks.ItemsSource = tp;
            Tasks_in_progress.ItemsSource = tp;
            Tasks_under_review.ItemsSource = tp_review;
            Finished_Tasks.ItemsSource = tp_completed;
            Tasks_under_review_Tester.ItemsSource = tp_review;
            Finished_Tasks_Tester.ItemsSource = tp_completed;
            Finished_Tasks_Developer.ItemsSource = tp_completed;
            Tasks_under_review_Developer.ItemsSource = tp;
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            DataContext db = new DataContext(LinqMethods.connectionstring);
            var t = db.GetTable<TaskClass>().ToList<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Id == Convert.ToInt32(b.Content))
                {
                    t[i].Task_Status = "issued";
                    db.SubmitChanges();
                    break;
                }
            }
            t = db.GetTable<TaskClass>().ToList<TaskClass>();
            ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_review = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_completed = new ObservableCollection<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Task_Status == "issued")
                    tp.Add(t[i]);
                if (t[i].Task_Status == "review")
                    tp_review.Add(t[i]);
                if (t[i].Task_Status == "completed")
                    tp_completed.Add(t[i]);
            }
            Issued_tasks.ItemsSource = tp;
            Tasks_in_progress.ItemsSource = tp;
            Tasks_under_review.ItemsSource = tp_review;
            Finished_Tasks.ItemsSource = tp_completed;
            Tasks_under_review_Tester.ItemsSource = tp_review;
            Finished_Tasks_Tester.ItemsSource = tp_completed;
            Finished_Tasks_Developer.ItemsSource = tp_completed;
            Tasks_under_review_Developer.ItemsSource = tp;
        }

        private void AproveButton_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            DataContext db = new DataContext(LinqMethods.connectionstring);
            var t = db.GetTable<TaskClass>().ToList<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Id == Convert.ToInt32(b.Content))
                {
                    t[i].Task_Status = "review";
                    db.SubmitChanges();
                    break;
                }
            }
            t = db.GetTable<TaskClass>().ToList<TaskClass>();
            ObservableCollection<TaskClass> tp = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_review = new ObservableCollection<TaskClass>();
            ObservableCollection<TaskClass> tp_completed = new ObservableCollection<TaskClass>();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Task_Status == "issued")
                    tp.Add(t[i]);
                if (t[i].Task_Status == "review")
                    tp_review.Add(t[i]);
                if (t[i].Task_Status == "completed")
                    tp_completed.Add(t[i]);
            }
            Issued_tasks.ItemsSource = tp;
            Tasks_in_progress.ItemsSource = tp;
            Tasks_under_review.ItemsSource = tp_review;
            Finished_Tasks.ItemsSource = tp_completed;
            Tasks_under_review_Tester.ItemsSource = tp_review;
            Finished_Tasks_Tester.ItemsSource = tp_completed;
            Finished_Tasks_Developer.ItemsSource = tp_completed;
            Tasks_under_review_Developer.ItemsSource = tp;
        }
    }
}

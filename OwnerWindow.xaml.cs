using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using Clinic_FrontEnd.Pages;

namespace Clinic_FrontEnd
{
    public partial class OwnerWindow : Window
    {
        string connectionString = "server=localhost; user=root; password=a@123456; database=ClinicalDatabaseee";
        private int userId;
        public string ownerName { get; set; }

        public OwnerWindow(int userId)
        {
            InitializeComponent();
            Loaded += Open_ODashboard;
            this.userId = userId;
            DisplayOwnerInfo();
            DataContext = this;
        }

        private void DisplayOwnerInfo()
        {
            // Fetch and display owner information using userId
            ownerName = GetUserNameFromDatabase(userId);

            // Update UI or perform other actions with the obtained information
            if (!string.IsNullOrEmpty(ownerName))
            {
                MessageBox.Show($"Welcome, {ownerName}!");
            }
            else
            {
                MessageBox.Show("Failed to retrieve owner information.");
            }
        }


        private void Open_Profile(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Profile profile = new Profile(userId);
            navframe.NavigationService.Navigate(profile);
        }

        private void Open_ODashboard(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            OwnerDashboard ownerDashboard = new OwnerDashboard(userId);
            navframe.NavigationService.Navigate(ownerDashboard);
        }

        private void Open_Analysis(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Analysis analysis = new Analysis(userId);
            navframe.NavigationService.Navigate(analysis);
        }

        private void Open_BillingSystem(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            BillingSystem billingSystem = new BillingSystem(userId);
            navframe.NavigationService.Navigate(billingSystem);
        }

        private void Open_Salary(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Salary salary = new Salary(userId);
            navframe.NavigationService.Navigate(salary);
        }

        private void Open_Chat(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Clinic_FrontEnd.Pages.Chat chat = new Clinic_FrontEnd.Pages.Chat(userId);
            navframe.NavigationService.Navigate(chat);
        }

        private void Open_AddUser(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            AddUser addUser = new AddUser(userId);
            navframe.NavigationService.Navigate(addUser);
        }

        private void Open_Attentance(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Attendance attendance = new Attendance(userId);
            navframe.NavigationService.Navigate(attendance);
        }


        private string GetUserNameFromDatabase(int userId)
        {
            string userName = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT FirstName, LastName FROM USER WHERE UserID = @UserID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader["FirstName"].ToString();
                                string lastName = reader["LastName"].ToString();
                                userName = $"{firstName} {lastName}";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return userName;
        }

        private void OwnerWindow_Dashboard(object sender, RoutedEventArgs e)
        {
            // Navigate to the OwnerDashboard page when the window is loaded
            navframe.NavigationService.Navigate(new Uri("/Pages/OwnerDashboard.xaml", UriKind.Relative));
        }

       
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();

        }
    }
}


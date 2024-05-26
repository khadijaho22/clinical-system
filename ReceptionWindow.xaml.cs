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
using Microsoft.Graph.Models;
using System.Web.Profile;

namespace Clinic_FrontEnd
{
    /// <summary>
    /// Interaction logic for ReceptionWindow.xaml
    /// </summary>
    public partial class ReceptionWindow : Window
    {

        string connectionString = "server=localhost; user=root; password=a@123456; database=ClinicalDatabaseee";
        public int userId;
        public string receptionistName { get; set; }
        public ReceptionWindow(int userId)
        {
            InitializeComponent();
            Loaded += Open_RDashboard;
            this.userId = userId;
            DisplayReceptionistInfo();
            DataContext = this;
        }

        private void DisplayReceptionistInfo()
        {
            receptionistName = GetUserNameFromDatabase(userId);

            if (!string.IsNullOrEmpty(receptionistName))
            {
                MessageBox.Show($"Welcome, {receptionistName}!");
            }
            else
            {
                MessageBox.Show("Failed to retrieve receptionist information.");
            }
        }

        private void Open_Profile(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Profile profile = new Profile(userId);
            navframe.NavigationService.Navigate(profile);
        }
        private void Open_AddPatient(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            AddPatient addPatient = new AddPatient(userId);
            navframe.NavigationService.Navigate(addPatient);
        }

        private void Open_Patients(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Patients patients = new Patients(userId);
            navframe.NavigationService.Navigate(patients);
        }

        private void Open_Appointment(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Appointment appointment = new Appointment(userId);
            navframe.NavigationService.Navigate(appointment);
        }

        private void Open_Chat(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Clinic_FrontEnd.Pages.Chat chat = new Clinic_FrontEnd.Pages.Chat(userId);
            navframe.NavigationService.Navigate(chat);
        }


        private void Open_RDashboard(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            ReceptionDashboard receptionDashboard = new ReceptionDashboard(userId);
            navframe.NavigationService.Navigate(receptionDashboard);
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
        

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();

        }

        private void Open_Equipment(object sender, RoutedEventArgs e)
        {
            Equipment equ = new Equipment();
            navframe.NavigationService.Navigate(equ);
        }
    }
}
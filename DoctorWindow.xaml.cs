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
using Clinic_FrontEnd.Pages;
using MySql.Data.MySqlClient;


namespace Clinic_FrontEnd
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        string connectionString = "server=localhost; user=root; password=a@123456; database=ClinicalDatabaseee";
        private int userId;
        public string doctorName { get; set; }

        public DoctorWindow(int userId)
        {
            InitializeComponent();
            Loaded += Open_DDashboard;
            this.userId = userId;
            DisplayDoctorInfo();
            DataContext = this;

        }

        private void DisplayDoctorInfo()
        {
            // Fetch and display doctor information using userId
            doctorName = GetUserNameFromDatabase(userId);

            // Update UI or perform other actions with the obtained information
            if (!string.IsNullOrEmpty(doctorName))
            {
                MessageBox.Show($"Welcome, Dr. {doctorName}!");
            }
            else
            {
                MessageBox.Show("Failed to retrieve doctor information.");
            }
        }

        private void Open_OCR(object sender, RoutedEventArgs e)
        {
            OCR Ocr = new OCR();
            navframe.NavigationService.Navigate(Ocr);
        }
        private void Open_Profile(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Profile profile = new Profile(userId);
            navframe.NavigationService.Navigate(profile);
        }
        private void Open_Report(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Report report = new Report(userId);
            navframe.NavigationService.Navigate(report);
        }

        private void Open_Assessment(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Assessment assessment = new Assessment(userId);
            navframe.NavigationService.Navigate(assessment);
        }

        private void Open_MedicalRecord(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            MedicalRecord medicalRecord = new MedicalRecord(userId);
            navframe.NavigationService.Navigate(medicalRecord);
        }

        private void Open_Chat(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            Clinic_FrontEnd.Pages.Chat chat = new Clinic_FrontEnd.Pages.Chat(userId);
            navframe.NavigationService.Navigate(chat);
        }



        private void Open_DDashboard(object sender, RoutedEventArgs e)
        {
            int userId = this.userId;
            DoctorDashboard doctorDashboard = new DoctorDashboard(userId);
            navframe.NavigationService.Navigate(doctorDashboard);
        }

        private void Open_Finalreport(object sender, RoutedEventArgs e)
        {
            Finalreport fReport = new Finalreport();
            navframe.NavigationService.Navigate(fReport);
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

        
    }
}


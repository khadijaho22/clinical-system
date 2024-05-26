using MySql.Data.MySqlClient;
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

namespace Clinic_FrontEnd
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        string connectionString = "server=localhost; user=root; password=a@123456; database=ClinicalDatabaseee";
        public string UserName { get; private set; }

        public ChangePasswordWindow(string userName)
        {
            InitializeComponent();
            UserName = userName;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = PasswordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;

            if (newPassword == confirmPassword)
            {
                // Passwords match, update the password in the database
                UpdatePasswordInDatabase(UserName, newPassword);
                MessageBox.Show("Password updated successfully.");
                Close(); // Close the window after successful password update
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please try again.");
            }
        }

        private void UpdatePasswordInDatabase(string userName, string newPassword)
        {
            // Assuming you have a MySQL database connection established
            // You should replace the connection string and query with your actual values
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Update the password for the specified user
                string updateQuery = "UPDATE USER SET Password = @NewPassword WHERE Username = @UserName";
                MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@UserName", userName);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    MessageBox.Show("Failed to update password in the database.");
                }
            }
        }
    }
}
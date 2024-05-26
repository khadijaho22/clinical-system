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
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Net;
using System.Net.Mail;

namespace Clinic_FrontEnd
{
    /// <summary>
    /// Interaction logic for PasswordResetWindow.xaml
    /// </summary>
    public partial class PasswordResetWindow : Window
    {
        public PasswordResetWindow()
        {
            InitializeComponent();
        }

        string username;
        string connectionString = "server=localhost; user=root; password=a@123456; database=ClinicalDatabaseee";
        private string resetCode;

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered username and email
            username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;

            // Example logic (replace this with your actual logic)
            if (IsUsernameAndEmailMatch(username, email))
            {
                // If the email matches the username and the username is in the user table, send the reset code via email
                SendResetCode(email);
                MessageBox.Show("Reset code sent to your email.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else
            {
                // Handle the case where the conditions are not met (display an error message, etc.)
                MessageBox.Show("Invalid email or username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool IsUsernameAndEmailMatch(string username, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to check if the username exists and the provided email matches
                    string query = "SELECT COUNT(*) FROM USER WHERE username = @Username AND email = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log, display error message, etc.)
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }



        private void SendResetCode(string Email)
        {
            try
            {
                // Sender's email address and password
                string fromMail = "khadijasherif79@gmail.com";
                string fromPassword = "dkevbigwgroumjbw";

                // Generate a random reset code (replace this with your actual code generation logic)
                resetCode = GenerateRandomCode(6);

                // Create a new MimeMessage
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "Password Reset Code";
                message.To.Add(new MailAddress(Email));
                message.Body = $"Your validation code is: {resetCode}";
                message.IsBodyHtml = true;

                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);


            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display error message, etc.)
                Console.WriteLine("Error sending reset code: " + ex.Message);
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string characters = "0123456789";
            StringBuilder randomCode = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                randomCode.Append(characters[index]);
            }

            return randomCode.ToString();
        }

        private void OpenChangePasswordWindow()
        {
            // Check if the entered code matches the generated code
            string enteredCode = CodeTextBox.Text;
            if (enteredCode == resetCode)
            {
                // If the codes match, open the ChangePasswordWindow
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(username);
                changePasswordWindow.Show();
                this.Close(); // Optionally close the password reset window
            }
            else
            {
                // Handle the case where the entered code does not match (display an error message, etc.)
                MessageBox.Show("Invalid reset code.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to open the ChangePasswordWindow
            OpenChangePasswordWindow();

        }
    
    }
}
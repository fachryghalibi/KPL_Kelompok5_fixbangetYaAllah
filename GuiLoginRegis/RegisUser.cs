using GuiLoginRegis.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace GuiLoginRegis
{
    public partial class RegisUser : Form
    {
        private UserRegistrationSystem<string> registrationSystem;

        public RegisUser()
        {
            InitializeComponent();
            registrationSystem = new UserRegistrationSystem<string>();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameBox.Text;
                string password = PasswordBox.Text;
                string name = NameBox.Text;
                string email = EmailBox.Text;

                if (string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Account<string> userAccount = registrationSystem.RegisterNewUser(username, password, name, email);
                MessageBox.Show("User account registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            OpenLogin();
        }

        private void OpenLogin()
        {
            LoginPage login = new LoginPage();
            login.Show();
            this.Hide();
        }
    }
}

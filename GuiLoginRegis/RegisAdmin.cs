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
    public partial class RegisAdmin : Form
    {
        private AdminRegistrationSystem<string> registrationSystem;


        public RegisAdmin()
        {
            InitializeComponent();
            registrationSystem = new AdminRegistrationSystem<string>();
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

                Account<string> adminAccount = registrationSystem.RegisterNewAdmin(username, password, name, email);
                MessageBox.Show("Admin account registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenAdminPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            OpenAdminPage();
        }

        private void OpenAdminPage()
        {
            HalamanAdmin AdminPage = new HalamanAdmin();
            AdminPage.Show();
            this.Hide();
        }
    }
}

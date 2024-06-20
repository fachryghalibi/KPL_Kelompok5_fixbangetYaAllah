using GuiLoginRegis.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace GuiLoginRegis
{
    public partial class LoginPage : Form
    {
        private LoginSystem<string> loginSystem;

        private const string AdminAccountsFilePath = "C:\\Users\\Asus\\Source\\Repos\\KPL_Kelompok5_fixbangetYaAllah\\GuiLoginRegis\\json\\admin_accounts.json"; // Ganti dengan path yang sesuai
        private const string UserAccountsFilePath = "C:\\Users\\Asus\\Source\\Repos\\KPL_Kelompok5_fixbangetYaAllah\\GuiLoginRegis\\json\\user_accounts.json"; // Ganti dengan path yang sesuai

        public LoginPage()
        {
            InitializeComponent();
            InitializeLoginSystem();
        }

        private void InitializeLoginSystem()
        {
            // Inisialisasi LoginSystem
            var adminAccounts = LoadAccountsFromFile<string>(AdminAccountsFilePath); // Ganti dengan implementasi sesuai kebutuhan
            var userAccounts = LoadAccountsFromFile<string>(UserAccountsFilePath); // Ganti dengan implementasi sesuai kebutuhan
            loginSystem = new LoginSystem<string>(adminAccounts, userAccounts);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameBox.Text.Trim(); // Ambil username dari TextBox UsernameBox
            string password = PasswordBox.Text; // Ambil password dari TextBox PasswordBox

            try
            {
                loginSystem.StartLogin(username, password); // Mulai proses login

                if (loginSystem.StartLogin(username, password))
                {
                    // Login successful
                    if (loginSystem.IsAdminLoggedIn())
                    {
                        MessageBox.Show("Admin berhasil login!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OpenAdminPage();
                    }
                    else
                    {
                        MessageBox.Show("User berhasil login!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OpenUserPage();
                    }
                }
                else
                {
                    // Login failed - Display error message
                    MessageBox.Show("Login gagal. Username atau password salah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login gagal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenAdminPage()
        {
            // Buka form HalamanAdmin
            HalamanAdmin adminForm = new HalamanAdmin();
            adminForm.Show();
            this.Hide();
        }

        private void OpenUserPage()
        {
            // Buka form HalamanUser
            HalamanUser userForm = new HalamanUser();
            userForm.Show();
            this.Hide();
        }

        private void ForgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword lupaPassword = new ForgotPassword();
            lupaPassword.Show();
            this.Hide();
        }

        private void RegisterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tindakan jika link register ditekan
            RegisUser regisUserForm = new RegisUser();
            regisUserForm.Show();
            this.Hide();
        }

        private List<Account<T>> LoadAccountsFromFile<T>(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    List<Account<T>> accounts = JsonSerializer.Deserialize<List<Account<T>>>(json);
                    if (accounts != null)
                    {
                        return accounts;
                    }
                    else
                    {
                        return new List<Account<T>>();
                    }
                }
                else
                {
                    MessageBox.Show("File tidak ditemukan: " + filename, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new List<Account<T>>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat akun: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Gagal memuat akun: {ex.Message}");
                return new List<Account<T>>();
            }
        }

        private void PasswordBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void UsernameBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

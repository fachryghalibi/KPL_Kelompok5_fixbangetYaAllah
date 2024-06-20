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


        private const string AdminAccountsFilePath = "D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\admin_accounts.json";
        private const string UserAccountsFilePath = "D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\user_accounts.json";

        public LoginPage()
        {
            InitializeComponent();
            InitializeLoginSystem();
        }

        private void InitializeLoginSystem()
        {
            var adminAccounts = LoadAccountsFromFile<string>(AdminAccountsFilePath);
            var userAccounts = LoadAccountsFromFile<string>(UserAccountsFilePath);
            loginSystem = new LoginSystem<string>(adminAccounts, userAccounts);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Text;

            try
            {
                // Coba login sebagai admin terlebih dahulu
                loginSystem.SetLoginStrategy(new AdminLoginStrategy<string>());

                if (!loginSystem.StartLogin(username, password))
                {
                    // Jika gagal, coba login sebagai user
                    loginSystem.SetLoginStrategy(new UserLoginStrategy<string>());
                    loginSystem.StartLogin(username, password);
                }

                if (loginSystem.IsLoggedIn())
                {
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
            HalamanAdmin adminForm = new HalamanAdmin();
            adminForm.Show();
            this.Hide();
        }

        private void OpenUserPage()
        {
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
                    return accounts ?? new List<Account<T>>();
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

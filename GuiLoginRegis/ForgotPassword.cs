using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GuiLoginRegis
{
    public partial class ForgotPassword : Form
    {
        private string dataFilePath1 = @"C:\Users\Asus\Source\Repos\KPL_Kelompok5_fixbangetYaAllah\GuiLoginRegis\json\user_accounts.json";
        private string dataFilePath2 = @"C:\Users\Asus\Source\Repos\KPL_Kelompok5_fixbangetYaAllah\GuiLoginRegis\json\admin_accounts.json";


        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name = NameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string newPassword = NewPasswordBox.Text.Trim();
            string confirmPassword = ConfirmBox.Text.Trim();

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Password baru dan konfirmasi password tidak cocok.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool accountFound = UpdatePassword(name, email, newPassword);

            if (accountFound)
            {
                MessageBox.Show("Password berhasil diubah.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Akun tidak ditemukan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool UpdatePassword(string name, string email, string newPassword)
        {
            bool accountFound = false;

            try
            {
                string jsonData1 = File.ReadAllText(dataFilePath1);
                string jsonData2 = File.ReadAllText(dataFilePath2);
                JArray data1 = JArray.Parse(jsonData1);
                JArray data2 = JArray.Parse(jsonData2);

                accountFound = UpdatePasswordInJsonArray(data1, name, email, newPassword);
                if (!accountFound)
                {
                    accountFound = UpdatePasswordInJsonArray(data2, name, email, newPassword);
                    if (accountFound)
                    {
                        File.WriteAllText(dataFilePath2, data2.ToString());
                    }
                }
                else
                {
                    File.WriteAllText(dataFilePath1, data1.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return accountFound;
        }

        private bool UpdatePasswordInJsonArray(JArray data, string name, string email, string newPassword)
        {
            foreach (JObject account in data)
            {
                if (account["Name"] != null && account["Email"] != null &&
                    account["Name"].ToString().Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    account["Email"].ToString().Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    account["Password"] = newPassword;
                    return true;
                }
            }
            return false;
        }
        private void NameBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            // Kosongkan atau tambahkan logika sesuai kebutuhan
        }

        private void EmailBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            // Kosongkan atau tambahkan logika sesuai kebutuhan
        }

        private void NewPasswordBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            // Kosongkan atau tambahkan logika sesuai kebutuhan
        }
    }
}
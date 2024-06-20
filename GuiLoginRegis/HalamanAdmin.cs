using Newtonsoft.Json;
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
using GuiLoginRegis.controller;

namespace GuiLoginRegis
{
    public partial class HalamanAdmin : Form
    {
        private List<Booking> bookings; // Deklarasi bookings sebagai field

        public HalamanAdmin()
        {
            InitializeComponent();
            LoadBookingData();
        }
        private void LoadBookingData()
        {
            string filePath = "D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\booking.json";

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    bookings = JsonConvert.DeserializeObject<List<Booking>>(json);

                    // Bind data to DataGridView
                    BookingJadwalTable.DataSource = bookings;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File 'booking.json' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveBookingData()
        {
            try
            {
                string json = JsonConvert.SerializeObject(bookings, Formatting.Indented); // Serialize bookings
                File.WriteAllText("D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\booking.json", json); // Save to JSON file
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BookingJadwalTable.SelectedRows.Count > 0)
            {
                var selectedRow = BookingJadwalTable.SelectedRows[0];
                var selectedBooking = (Booking)selectedRow.DataBoundItem;
                selectedBooking.Status = "sudah konsultasi"; 

                BookingJadwalTable.Refresh();

                SaveBookingData();
            }
            else
            {
                MessageBox.Show("Silakan pilih baris yang akan diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HalamanAdmin_Load(object sender, EventArgs e)
        {

        }
        private void TambahAdminButton_Click(object sender, EventArgs e)
        {
            RegisAdmin TambahAdminPage = new RegisAdmin();
            TambahAdminPage.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
        private void BookingJadwalTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            loginSystem.Logout();

            MessageBox.Show("Successfully logged out.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoginPage login = new LoginPage();
            login.Show();
            this.Hide();
        }
    }
}

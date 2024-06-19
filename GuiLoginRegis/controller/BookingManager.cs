using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace GuiLoginRegis
{
    public static class BookingManager
    {
        private const string FilePath = "D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\booking.json";

        public static void AddBooking(string nama, string nim, string tanggal, string jam, string status)
        {
            List<Booking> bookings = new List<Booking>();

            // Load existing bookings from JSON file if it exists
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            }

            // Create new booking object
            Booking newBooking = new Booking
            {
                Nama = nama,
                Nim = nim,
                Tanggal = tanggal,
                Jam = jam,
                Status = status
            };

            // Add new booking to the list
            bookings.Add(newBooking);

            // Serialize updated list to JSON and write to file
            string updatedJson = JsonConvert.SerializeObject(bookings, Formatting.Indented);
            File.WriteAllText(FilePath, updatedJson);
        }
    }
}

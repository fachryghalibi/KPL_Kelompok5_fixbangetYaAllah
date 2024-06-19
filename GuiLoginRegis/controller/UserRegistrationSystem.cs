using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace GuiLoginRegis.controller
{
    public class UserRegistrationSystem<T>
    {
        private enum State
        {
            EnteringUsername,
            EnteringPassword,
            EnteringName,
            EnteringEmail,
            Completed
        }

        private State currentState = State.EnteringUsername;
        private readonly string filePath = "D:\\Fachry\\Kuliah\\KPL_FIX BANGET YA ALLAH\\KPL_Kelompok5_TelkomMedika\\KPL_Kelompok5_TelkomMedika\\GuiLoginRegis\\json\\user_accounts.json";

        public UserRegistrationSystem()
        {
        }

        public Account<T> RegisterNewUser()
        {
            T username = default(T);
            string password = null;
            string name = null;
            string email = null;

            while (true)
            {
                try
                {
                    switch (currentState)
                    {
                        case State.EnteringUsername:
                            Console.WriteLine("Masukkan username:");
                            username = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                            currentState = State.EnteringPassword;
                            break;

                        case State.EnteringPassword:
                            Console.WriteLine("Masukkan password:");
                            password = Console.ReadLine();
                            currentState = State.EnteringName;
                            break;

                        case State.EnteringName:
                            Console.WriteLine("Masukkan nama:");
                            name = Console.ReadLine();
                            currentState = State.EnteringEmail;
                            break;

                        case State.EnteringEmail:
                            Console.WriteLine("Masukkan email:");
                            email = Console.ReadLine();
                            currentState = State.Completed;
                            break;

                        case State.Completed:
                            Account<T> userAccount = new Account<T>(username, password, name, email);
                            SaveAccountToFile(userAccount);
                            Console.WriteLine("Akun user berhasil didaftarkan.");
                            return userAccount;

                        default:
                            throw new InvalidOperationException("Status tidak valid.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Registrasi gagal: {ex.Message}");
                    currentState = State.EnteringUsername;
                }
            }
        }

        public Account<T> RegisterNewUser(T username, string password, string name, string email)
        {
            try
            {
                Account<T> userAccount = new Account<T>(username, password, name, email);
                SaveAccountToFile(userAccount);
                return userAccount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during registration: {ex.Message}");
            }
        }

        private void SaveAccountToFile(Account<T> account)
        {
            try
            {
                List<Account<T>> accounts = new List<Account<T>>();

                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    accounts = JsonSerializer.Deserialize<List<Account<T>>>(json) ?? new List<Account<T>>();
                }

                accounts.Add(account);
                string serializedAccounts = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, serializedAccounts);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving account to file: {ex.Message}");
            }
        }
    }
}

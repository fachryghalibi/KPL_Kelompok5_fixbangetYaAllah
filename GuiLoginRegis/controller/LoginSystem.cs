using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiLoginRegis.controller
{
    public class LoginSystem<T>
    {
        private enum State
        {
            NotLoggedIn,
            EnteringUsername,
            EnteringPassword,
            LoggedIn
        }

        private State currentState = State.NotLoggedIn;
        private List<Account<T>> adminAccounts;
        private List<Account<T>> userAccounts;
        private Account<T> currentAccount;
        private bool isAdminLoggedIn = false;

        public LoginSystem(List<Account<T>> admins, List<Account<T>> users)
        {
            adminAccounts = admins;
            userAccounts = users;
        }

        public void AddAdminAccount(Account<T> admin)
        {
            adminAccounts.Add(admin);
        }

        public void AddUserAccount(Account<T> user)
        {
            userAccounts.Add(user);
        }

        public void SetAdminAccount(Account<T> admin)
        {
            currentAccount = admin;
            isAdminLoggedIn = true;
        }

        public void SetUserAccount(Account<T> user)
        {
            currentAccount = user;
            isAdminLoggedIn = false;
        }

        public bool StartLogin(string username, string password)
        {
            currentAccount = adminAccounts.FirstOrDefault(a => a.Username.Equals((T)Convert.ChangeType(username, typeof(T)))) ??
                                userAccounts.FirstOrDefault(u => u.Username.Equals((T)Convert.ChangeType(username, typeof(T))));

            if (currentAccount != null && currentAccount.Password == password)
            {
                isAdminLoggedIn = adminAccounts.Contains(currentAccount);
                currentState = State.LoggedIn;
                return true; // Login successful
            }
            else
            {
                currentState = State.NotLoggedIn;
                return false; // Login failed
            }
        }


        public void Logout()
        {
            currentState = State.NotLoggedIn;
            isAdminLoggedIn = false;
        }

        public bool IsLoggedIn()
        {
            return currentState == State.LoggedIn;
        }

        public bool IsAdminLoggedIn()
        {
            return isAdminLoggedIn;
        }
    }
}

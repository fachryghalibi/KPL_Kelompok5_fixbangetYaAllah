using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiLoginRegis.controller
{
    public interface ILoginStrategy<T>
    {
        bool Login(LoginSystem<T> loginSystem, string username, string password);
    }

    public class AdminLoginStrategy<T> : ILoginStrategy<T>
    {
        public bool Login(LoginSystem<T> loginSystem, string username, string password)
        {
            var adminAccount = loginSystem.AdminAccounts.FirstOrDefault(a => a.Username.Equals((T)Convert.ChangeType(username, typeof(T))));
            if (adminAccount != null && adminAccount.Password == password)
            {
                loginSystem.SetAdminAccount(adminAccount);
                return true;
            }
            return false;
        }
    }

    public class UserLoginStrategy<T> : ILoginStrategy<T>
    {
        public bool Login(LoginSystem<T> loginSystem, string username, string password)
        {
            var userAccount = loginSystem.UserAccounts.FirstOrDefault(u => u.Username.Equals((T)Convert.ChangeType(username, typeof(T))));
            if (userAccount != null && userAccount.Password == password)
            {
                loginSystem.SetUserAccount(userAccount);
                return true;
            }
            return false;
        }
    }


    public class LoginSystem<T>
    {
        private enum State
        {
            NotLoggedIn,
            LoggedIn
        }

        private State currentState = State.NotLoggedIn;
        public List<Account<T>> AdminAccounts { get; private set; }
        public List<Account<T>> UserAccounts { get; private set; }
        private Account<T> currentAccount;
        private bool isAdminLoggedIn = false;
        private ILoginStrategy<T> loginStrategy;

        public LoginSystem(List<Account<T>> admins, List<Account<T>> users)
        {
            AdminAccounts = admins;
            UserAccounts = users;
        }

        public void SetLoginStrategy(ILoginStrategy<T> strategy)
        {
            loginStrategy = strategy;
        }

        public void AddAdminAccount(Account<T> admin)
        {
            AdminAccounts.Add(admin);
        }

        public void AddUserAccount(Account<T> user)
        {
            UserAccounts.Add(user);
        }

        public void SetAdminAccount(Account<T> admin)
        {
            currentAccount = admin;
            isAdminLoggedIn = true;
            currentState = State.LoggedIn;
        }

        public void SetUserAccount(Account<T> user)
        {
            currentAccount = user;
            isAdminLoggedIn = false;
            currentState = State.LoggedIn;
        }

        public bool StartLogin(string username, string password)
        {
            if (loginStrategy != null)
            {
                return loginStrategy.Login(this, username, password);
            }
            return false;
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

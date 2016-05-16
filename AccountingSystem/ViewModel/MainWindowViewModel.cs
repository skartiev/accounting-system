﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AccountingSystem.Domain.Abstract;
using AccountingSystem.Domain.Entities;
using AccountingSystem.View;
using Microsoft.Practices.Unity;

namespace AccountingSystem.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// Gets or sets Selected users
        /// </summary>
        public List<User> SelectedUsers { get; set; }

        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Get user repository
        /// </summary>
        private IUserRepository UserRepository { get; }
        #endregion

        #region Commands
        /// <summary>
        /// Show message box with text
        /// </summary>
        public ICommand ShowCommand { get; set; }

        /// <summary>
        /// Shows window of adding account
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Delete selected row on delete keyboard button 
        /// </summary>
        public ICommand DeleteRowCommand { get; set; }
        #endregion

        public MainWindowViewModel(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            Users = UserRepository.GetUsers().ToList();

            ShowCommand = new RelayCommandWithParameter<string>(ShowMessage, param => true);
            AddCommand = new RelayCommand(ShowAddWindow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
        }

        public void DeleteRow()
        {
            foreach (var user in SelectedUsers)
            {
                UserRepository.DeleteUser(user);
                UserRepository.Save();
            }
            UpdateFromDb();          
        }

        private static void ShowAddWindow()
        {
            var addAccountWindow = App.Container.Resolve<AddAccount>();
            addAccountWindow.Show();
        }

        #region Private methods
        private static void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }

        private void UpdateFromDb()
        {
            using (var db = UserRepository.Context)
            {
                Users = db.Users.ToList();

                if (SelectedUsers == null)
                {
                    SelectedUsers = Users;
                }
                NotifyPropertyChanged("SelectedUsers");
                NotifyPropertyChanged("Users");
            }
        }
        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}

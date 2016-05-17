using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;
using AccountingSystem.View;
using AccountingSystem.ViewModel.Commands;
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
        /// Gets or sets unit of work design pattern
        /// </summary>
        private UnitOfWork UnitOfWork { get; }
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

        /// <summary>
        /// On add user window closing
        /// </summary>
        public ICommand WindowClosing { get; private set; }

        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            UnitOfWork = new UnitOfWork();
            Users = UnitOfWork.UsersRepository.Get().ToList();

            ShowCommand = new RelayCommandWithParameter<string>(ShowMessage, param => true);
            AddCommand = new RelayCommand(ShowAddWindow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
            WindowClosing = new DelegateCommand(OnAddWindowClosing);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Call add user window
        /// </summary>
        private static void ShowAddWindow()
        {
            var addAccountWindow = App.Container.Resolve<AddAccount>();
            addAccountWindow.Show();
        }

        /// <summary>
        /// Delete row fucntionality on delete keyboard button 
        /// </summary>
        private void DeleteRow()
        {
            foreach (var user in SelectedUsers)
            {
                UnitOfWork.UsersRepository.Delete(user);
                UnitOfWork.Save();
            }
            UpdateFromDb();
        }

        /// <summary>
        /// Shom messagebox with message
        /// </summary>
        /// <param name="text"></param>
        private static void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }

        /// <summary>
        /// Update info from actual BD to view changes
        /// </summary>
        private void UpdateFromDb()
        {
            Users = UnitOfWork.UsersRepository.Get().ToList();

            if (SelectedUsers == null)
            {
                SelectedUsers = Users;
            }
            NotifyPropertyChanged("SelectedUsers");
            NotifyPropertyChanged("Users");
            
        }

        public void OnAddWindowClosing(object obj)
        {
            UpdateFromDb();
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

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.ViewModel
{
    public class AddAccountViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public Action CloseAction { get; set; }


        /// <summary>
        /// Add entity to database
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Cancel adding and close this window
        /// </summary>
        public ICommand CancelCommand { get; set; }


        public AddAccountViewModel()
        {
            AddCommand = new RelayCommand(AddUser);
            CancelCommand = new RelayCommand(Cancel);
        }

        //FIXME - delete unnecessary objects in RelayCommand
        public void AddUser(object obj)
        {
            var user = new User()
            {
                Age = Age,
                Email = Email,
                Name = Name
            };
            using (var db = new EfDatabaseContext())
            {
                db.Users.Attach(user);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        //FIXME - delete unnecessary objects in RelayCommand
        public void Cancel(object obj)
        {
        }

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

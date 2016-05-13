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
        /// Add entity to database
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Cancel adding and close this window
        /// </summary>
        public ICommand CancelCommand { get; set; }

        public AddAccountViewModel()
        {
            AddCommand = new RelayCommandWithParameter<User>(AddUser);
            CancelCommand = new RelayCommandWithParameter<Window>(Cancel);
        }

        public void AddUser(User user)
        {
            using (var db = new EfDatabaseContext())
            {
                db.Users.Attach(user);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void Cancel(Window window)
        {
            window?.Close();
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

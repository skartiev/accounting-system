using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AccountingSystem.Domain.Abstract;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Model;

namespace AccountingSystem.ViewModel
{
    public class AddAccountViewModel : INotifyPropertyChanged
    {
        private UnitOfWork UnitOfWork { get; }
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
        /// Add entity to database
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Cancel adding and close this window
        /// </summary>
        public ICommand CancelCommand { get; set; }


        public AddAccountViewModel()
        {
            AddCommand = new RelayCommandWithParameter<ICloseable>(AddUser);
            CancelCommand = new RelayCommandWithParameter<ICloseable>(Cancel);

            UnitOfWork = new UnitOfWork();
        }

        public void AddUser(ICloseable window)
        {
            var user = new User()
            {
                Age = Age,
                Email = Email,
                Name = Name
            };
            UnitOfWork.UsersRepository.Insert(user);
            UnitOfWork.Save();
  
            window.Close();
        }

        public void Cancel(ICloseable window)
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

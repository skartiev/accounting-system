using System.ComponentModel;
using System.Windows.Input;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Model;

namespace AccountingSystem.ViewModel
{
    public class AddAccountViewModel : INotifyPropertyChanged
    {
        #region Properties

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

        #endregion

        #region Commands
        /// <summary>
        /// Add entity to database
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Cancel adding and close this window
        /// </summary>
        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructors

        public AddAccountViewModel()
        {
            AddCommand = new RelayCommandWithParameter<ICloseable>(AddUser);
            CancelCommand = new RelayCommandWithParameter<ICloseable>(Cancel);

            UnitOfWork = new UnitOfWork();
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Add user click handler command
        /// </summary>
        /// <param name="window"></param>
        private void AddUser(ICloseable window)
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

        /// <summary>
        /// Cancel click handler command
        /// </summary>
        /// <param name="window"></param>
        private static void Cancel(ICloseable window)
        {
            window?.Close();
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

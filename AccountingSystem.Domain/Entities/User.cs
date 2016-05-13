using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Domain.Entities
{
    public class User : IEquatable<User>
    {
        private int _id;
        [Key]
        public int Id { get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
                } 
            } }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    NotifyPropertyChanged("Age");
                }
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public bool Equals(User other)
        {
            return (Id == other.Id);
        }
    }
}

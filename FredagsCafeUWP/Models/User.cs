using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class User : INotifyPropertyChanged
    {
        #region Field

        private string _name;
        private string _grade;
        private string _education;
        private string _email;
        private string _telephoneNumber;
        private string _userName;
        private string _password;
        private string _imageSource;

        #endregion

        #region Constructors

        public User(string name, string grade, string education, string email, string telephoneNumber, string userName, string password, string imageSource)
        {
            Name = name;
            Grade = grade;
            Education = education;
            Email = email;
            TelephoneNumber = telephoneNumber;
            UserName = userName;
            Password = password;
            ImageSource = imageSource;
        }

        public User()
        {
            
        }

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            } 
        }
        public string Grade
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged();
            }
        }
        public string Education
        {
            get => _education;
            set
            {
                _education = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string TelephoneNumber
        {
            get => _telephoneNumber;
            set
            {
                _telephoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

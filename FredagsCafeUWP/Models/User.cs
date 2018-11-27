using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class User : INotifyPropertyChanged
    {
        private string _name;
        private string _grade;
        private string _education;
        private string _email;
        private string _telephoneNumber;
        private string _userName;
        private string _password;
        private string _imageSource;

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

        #region Properties

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Grade
        {
            get => _grade;
            set => _grade = value;
        }
        public string Education
        {
            get => _education;
            set => _education = value;
        }
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        public string TelephoneNumber
        {
            get => _telephoneNumber;
            set => _telephoneNumber = value;
        }
        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
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

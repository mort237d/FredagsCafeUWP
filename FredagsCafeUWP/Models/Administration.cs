using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class Administration : INotifyPropertyChanged
    {
        private string _nameTB;
        private string _gradeTB;
        private string _educationTB;
        private string _emailTB;
        private string _telephoneNumberTB;
        private string _userNameTB;
        private string _passwordTB;

        private ObservableCollection<User> _users;
        private User _selectedUser;

        public string NameTb
        {
            get { return _nameTB; }
            set { _nameTB = value; }
        }

        public string GradeTb
        {
            get { return _gradeTB; }
            set { _gradeTB = value; }
        }

        public string EducationTb
        {
            get { return _educationTB; }
            set { _educationTB = value; }
        }

        public string EmailTb
        {
            get { return _emailTB; }
            set { _emailTB = value; }
        }

        public string TelephoneNumberTb
        {
            get { return _telephoneNumberTB; }
            set { _telephoneNumberTB = value; }
        }

        public string UserNameTb
        {
            get { return _userNameTB; }
            set { _userNameTB = value; }
        }

        public string PasswordTb
        {
            get { return _passwordTB; }
            set { _passwordTB = value; }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        public Administration()
        {
            Users = new ObservableCollection<User>()
            {
                new User("Morten", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Morten", "Morten", "Assets/Profile-icon.png"),
                new User("Daniel", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Daniel", "Daniel", "Assets/Profile-icon.png"),
                new User("Jacob", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Jacob", "Jacob", "Assets/Profile-icon.png"),
                new User("Lucas", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Lucas", "Lucas", "Assets/Profile-icon.png"),
                new User("Christian", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Christian", "Christian", "Assets/Profile-icon.png")
            };
        }

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

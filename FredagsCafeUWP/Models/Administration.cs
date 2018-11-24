using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class Administration : INotifyPropertyChanged
    {
        private Message message;

        private string _nameTB;
        private string _gradeTB;
        private string _educationTB;
        private string _emailTB;
        private string _telephoneNumberTB;
        private string _userNameTB;
        private string _passwordTB;
        private string _confirmPasswordTB;

        private ObservableCollection<User> _users;
        private User _selectedUser;

        #region Props

        public string NameTb
        {
            get { return _nameTB; }
            set
            {
                _nameTB = value;
                OnPropertyChanged();
            }
        }

        public string GradeTb
        {
            get { return _gradeTB; }
            set { _gradeTB = value;
                OnPropertyChanged();
            }
        }

        public string EducationTb
        {
            get { return _educationTB; }
            set { _educationTB = value;
                OnPropertyChanged();
            }
        }

        public string EmailTb
        {
            get { return _emailTB; }
            set
            {
                _emailTB = value;
                OnPropertyChanged();
            }
        }

        public string TelephoneNumberTb
        {
            get { return _telephoneNumberTB; }
            set { _telephoneNumberTB = value;
                OnPropertyChanged();
            }
        }

        public string UserNameTb
        {
            get { return _userNameTB; }
            set { _userNameTB = value;
                OnPropertyChanged();
            }
        }

        public string PasswordTb
        {
            get { return _passwordTB; }
            set { _passwordTB = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordTb
        {
            get { return _confirmPasswordTB; }
            set { _confirmPasswordTB = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public Administration()
        {
            message = new Message(this);

            Users = new ObservableCollection<User>()
            {
                new User("Morten", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Morten", "Morten", "Assets/Profile-icon.png"),
                new User("Daniel", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Daniel", "Daniel", "Assets/Profile-icon.png"),
                new User("Jacob", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Jacob", "Jacob", "Assets/Profile-icon.png"),
                new User("Lucas", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Lucas", "Lucas", "Assets/Profile-icon.png"),
                new User("Christian", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Christian", "Christian", "Assets/Profile-icon.png")
            };
        }

        public void AddUser()
        {
            //TODO check for same user
            if (NameTb != null && GradeTb != null && EducationTb != null && EmailTb != null && TelephoneNumberTb != null && UserNameTb != null && PasswordTb != null)
            {
                if (EmailTb.Contains("@edu.easj.dk") || EmailTb.Contains("@easj.dk"))
                {
                    if (PasswordTb == ConfirmPasswordTb)
                    {
                        Users.Add(new User(NameTb, GradeTb, EducationTb, EmailTb, TelephoneNumberTb, UserNameTb,
                            PasswordTb, "Assets/Profile-icon.png"));

                        NameTb = null;
                        GradeTb = null;
                        EducationTb = null;
                        EmailTb = null;
                        TelephoneNumberTb = null;
                        UserNameTb = null;
                        PasswordTb = null;
                        ConfirmPasswordTb = null;
                    }
                    else message.Error("Uoverensstemmelser", "Password stemmer ikke over ens med confirm password");
                }
                else message.Error("Forkert email", "Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
            }
        }

        public void RemoveUser()
        {
            if (SelectedUser != null) message.YesNo("Slet bruger", "Er du sikker på at du vil slette " + SelectedUser.Name + "?");
            else message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
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

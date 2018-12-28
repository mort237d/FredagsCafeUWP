using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class UserAdministrator : INotifyPropertyChanged
    {
        #region Field

        private static Message _message;
        BrowseImages _browseImages = new BrowseImages();

        public readonly string StandardImage = "UserImages/Profile-icon.png";

        private string _nameTb,_gradeTb,_educationTb,_emailTb,_telephoneNumberTb,_userNameTb,_passwordTb,_confirmPasswordTb;
        private string _imageTb = "";

        private string _visible;

        private bool _showAddUserPopUp;
        private bool _showLogPopUp;

        private ObservableCollection<User> _users;
        private User _selectedUser, _currentUser;

        #endregion

        #region Props

        public string NameTb
        {
            get => _nameTb;
            set
            {
                _nameTb = value;
                OnPropertyChanged();
            }
        }

        public string GradeTb
        {
            get => _gradeTb;
            set { _gradeTb = value;
                OnPropertyChanged();
            }
        }

        public string EducationTb
        {
            get => _educationTb;
            set { _educationTb = value;
                OnPropertyChanged();
            }
        }

        public string EmailTb
        {
            get => _emailTb;
            set
            {
                _emailTb = value;
                OnPropertyChanged();
            }
        }

        public string TelephoneNumberTb
        {
            get => _telephoneNumberTb;
            set { _telephoneNumberTb = value;
                OnPropertyChanged();
            }
        }

        public string UserNameTb
        {
            get => _userNameTb;
            set { _userNameTb = value;
                OnPropertyChanged();
            }
        }

        public string ImageTb
        {
            get { return _imageTb; }
            set
            {
                _imageTb = value; 
                OnPropertyChanged();
            }
        }

        public string PasswordTb
        {
            get => _passwordTb;
            set { _passwordTb = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordTb
        {
            get => _confirmPasswordTb;
            set { _confirmPasswordTb = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value; 
                OnPropertyChanged();
            }
        }

        public string Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAddUserPopUp
        {
            get { return _showAddUserPopUp; }
            set
            {
                _showAddUserPopUp = value;
                OnPropertyChanged();
            }
        }

        public bool ShowLogPopUp
        {
            get { return _showLogPopUp; }
            set
            {
                _showLogPopUp = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private UserAdministrator()
        {
            _message = new Message(this);
        }

        #region Singleton

        private static UserAdministrator _instance;

        public static UserAdministrator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserAdministrator();
                }
                return _instance;
            }
        }

        #endregion
        
        #region ButtonMethods

        public void ShowAddUserPopUpMethod()
        {
            ShowAddUserPopUp = true;
        }

        public void ShowLogPopUpMethod()
        {
            ShowLogPopUp = true;
        }

        public async void BrowseImageButton()
        {
            ImageTb = await _browseImages.BrowseImageWindow("UserImages/");
            ShowAddUserPopUpMethod();
        }
        
        public async void AddUser()
        {
            if ((NameTb ?? GradeTb ?? EducationTb ?? EmailTb ?? TelephoneNumberTb ?? UserNameTb ?? PasswordTb) != null)
            {
                if (EmailTb.Contains("@edu.easj.dk") || EmailTb.Contains("@easj.dk"))
                {
                    foreach (var user in Users)
                    {
                        if (user.Email.Equals(EmailTb))
                        {
                            await _message.Error("Email findes allerede",user.Email + " findes allerede til en anden bruger");
                            return;
                        }
                    }

                    if (int.TryParse(TelephoneNumberTb, out _) && TelephoneNumberTb.Length == 8)
                    {
                        if (PasswordTb == ConfirmPasswordTb)
                        {
                            if (string.IsNullOrEmpty(ImageTb)) Users.Add(new User(NameTb, GradeTb, EducationTb, EmailTb, TelephoneNumberTb, UserNameTb, PasswordTb, StandardImage, ""));
                            else Users.Add(new User(NameTb, GradeTb, EducationTb, EmailTb, TelephoneNumberTb, UserNameTb, PasswordTb, ImageTb, ""));

                            ShowAddUserPopUp = false;

                            NameTb = GradeTb = EducationTb = EmailTb = TelephoneNumberTb = UserNameTb = ImageTb = PasswordTb = ConfirmPasswordTb = null;                            
                        }
                        else await _message.Error("Uoverensstemmelser","Password stemmer ikke over ens med confirm password");
                    }
                    else await _message.Error("Forkert input","Telefonnummert skal være et tal på 8 cifre.");
                }
                else await _message.Error("Forkert email","Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
        }

        public async void RemoveUser()
        {
            if (SelectedUser != CurrentUser)
            {
                if (SelectedUser != null) await _message.YesNo("Slet bruger", "Er du sikker på at du vil slette " + SelectedUser.Name + "?");
                else await _message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
            }
            else await _message.Error("Sletning ugyldig", "Det er ikke muligt at slette dig selv i admin tilstand");
        }

        public void ButtonVisibility(User userToCheck)
        {
            if (userToCheck.Admin == "Admin") Visible = "Visible";
            else Visible = "Collapsed";
        }

        public async void ChangeAdmin()
        {
            if (SelectedUser != null) await _message.YesNo("Giv admin videre", "Er du sikker på at du vil give admin videre til " + SelectedUser.Name + "?");
            else await _message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
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

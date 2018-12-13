using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class AccountSettingsClass : INotifyPropertyChanged
    {
        #region Field

        private Administration _administration = Administration.Instance;
        private Message _message;

        private readonly string _standardImage = "UserImages/Profile-icon.png";

        private string _nameTb, _gradeTb, _educationTb, _emailTb, _telephoneNumberTb, _userNameTb, _passwordTb, _confirmPasswordTb;

        private bool _showAccountSettingsPopUp, _showAdminAccountPopup;

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
            set
            {
                _gradeTb = value;
                OnPropertyChanged();
            }
        }

        public string EducationTb
        {
            get => _educationTb;
            set
            {
                _educationTb = value;
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
            set
            {
                _telephoneNumberTb = value;
                OnPropertyChanged();
            }
        }

        public string UserNameTb
        {
            get => _userNameTb;
            set
            {
                _userNameTb = value;
                OnPropertyChanged();
            }
        }

        public string PasswordTb
        {
            get => _passwordTb;
            set
            {
                _passwordTb = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordTb
        {
            get => _confirmPasswordTb;
            set
            {
                _confirmPasswordTb = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAccountSettingsPopUp
        {
            get { return _showAccountSettingsPopUp; }
            set
            {
                _showAccountSettingsPopUp = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAdminAccountPopup
        {
            get { return _showAdminAccountPopup; }
            set
            {
                _showAdminAccountPopup = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public AccountSettingsClass()
        {
            _message = new Message(this);
        }

        #region ButtonMethods

        public async void ChangeSettings()
        {
            if (_administration.CurrentUser.Name == NameTb || _administration.CurrentUser.Grade == GradeTb ||
                _administration.CurrentUser.Education == EducationTb || _administration.CurrentUser.Email == EmailTb ||
                _administration.CurrentUser.TelephoneNumber == TelephoneNumberTb ||
                _administration.CurrentUser.UserName == UserNameTb ||
                _administration.CurrentUser.Password == PasswordTb)
            {
                if (PasswordTb == ConfirmPasswordTb)
                {
                    _administration.CurrentUser.Name = NameTb;
                    _administration.CurrentUser.Grade = GradeTb;
                    _administration.CurrentUser.Education = EducationTb;
                    _administration.CurrentUser.Email = EmailTb;
                    _administration.CurrentUser.TelephoneNumber = TelephoneNumberTb;
                    _administration.CurrentUser.UserName = UserNameTb;
                    _administration.CurrentUser.Password = PasswordTb;
                    foreach (var user in _administration.Users)
                    {
                        if (_administration.CurrentUser.Email == user.Email)
                        {
                            _administration.Users[_administration.Users.IndexOf(user)] = _administration.CurrentUser;
                            break;
                        }
                    }

                    ShowAccountSettingsPopUp = false;
                }
                else await _message.Error("Forkert password", "Passwords stemmer ikke overens.");
            }
            else await _message.Error("Intet ændret", "Intet er blevet ændret.");
        }

        public async void ChangeSelectedAccountSettingsMethod()
        {
            if (_administration.SelectedUser.Name == NameTb || _administration.SelectedUser.Grade == GradeTb ||
                _administration.SelectedUser.Education == EducationTb || _administration.SelectedUser.Email == EmailTb ||
                _administration.SelectedUser.TelephoneNumber == TelephoneNumberTb ||
                _administration.SelectedUser.UserName == UserNameTb ||
                _administration.SelectedUser.Password == PasswordTb)
            {
                if (PasswordTb == ConfirmPasswordTb)
                {
                    _administration.SelectedUser.Name = NameTb;
                    _administration.SelectedUser.Grade = GradeTb;
                    _administration.SelectedUser.Education = EducationTb;
                    _administration.SelectedUser.Email = EmailTb;
                    _administration.SelectedUser.TelephoneNumber = TelephoneNumberTb;
                    _administration.SelectedUser.UserName = UserNameTb;
                    _administration.SelectedUser.Password = PasswordTb;
                    foreach (var user in _administration.Users)
                    {
                        if (_administration.SelectedUser.Email == user.Email)
                        {
                            _administration.Users[_administration.Users.IndexOf(user)] = _administration.SelectedUser;
                            break;
                        }
                    }

                    ShowAccountSettingsPopUp = false;
                }
                else await _message.Error("Forkert password", "Passwords stemmer ikke overens.");
            }
            else await _message.Error("Intet ændret", "Intet er blevet ændret.");
        }

        public void ShowAccountSettingsPopUpMethod()
        {
            NameTb = _administration.CurrentUser.Name;
            GradeTb = _administration.CurrentUser.Grade;
            EducationTb = _administration.CurrentUser.Education;
            EmailTb = _administration.CurrentUser.Email;
            TelephoneNumberTb = _administration.CurrentUser.TelephoneNumber;
            UserNameTb = _administration.CurrentUser.UserName;
            PasswordTb = _administration.CurrentUser.Password;

            ShowAccountSettingsPopUp = true;
        }

        public void ShowAdminAccountPopUpMethod()
        {
            if (_administration.SelectedUser != null)
            {
                NameTb = _administration.SelectedUser.Name;
                GradeTb = _administration.SelectedUser.Grade;
                EducationTb = _administration.SelectedUser.Education;
                EmailTb = _administration.SelectedUser.Email;
                TelephoneNumberTb = _administration.SelectedUser.TelephoneNumber;
                UserNameTb = _administration.SelectedUser.UserName;
                PasswordTb = _administration.SelectedUser.Password;

                ShowAdminAccountPopup = true;
            }
            else _message.Error("Ingen bruger valgt", "Vælg venligst en bruger du vil ændre.");
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

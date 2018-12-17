using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class AccountSettings : INotifyPropertyChanged
    {
        #region Field

        private UserAdministrator _userAdministrator = UserAdministrator.Instance;
        private Message _message;

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

        public AccountSettings()
        {
        }

        #region ButtonMethods

        public async void ChangeSettings()
        {
            if (_userAdministrator.CurrentUser.Name == NameTb || _userAdministrator.CurrentUser.Grade == GradeTb ||
                _userAdministrator.CurrentUser.Education == EducationTb || _userAdministrator.CurrentUser.Email == EmailTb ||
                _userAdministrator.CurrentUser.TelephoneNumber == TelephoneNumberTb ||
                _userAdministrator.CurrentUser.UserName == UserNameTb ||
                _userAdministrator.CurrentUser.Password == PasswordTb)
            {
                if (PasswordTb == ConfirmPasswordTb)
                {
                    _userAdministrator.CurrentUser.Name = NameTb;
                    _userAdministrator.CurrentUser.Grade = GradeTb;
                    _userAdministrator.CurrentUser.Education = EducationTb;
                    _userAdministrator.CurrentUser.Email = EmailTb;
                    _userAdministrator.CurrentUser.TelephoneNumber = TelephoneNumberTb;
                    _userAdministrator.CurrentUser.UserName = UserNameTb;
                    _userAdministrator.CurrentUser.Password = PasswordTb;
                    foreach (var user in _userAdministrator.Users)
                    {
                        if (_userAdministrator.CurrentUser.Email == user.Email)
                        {
                            _userAdministrator.Users[_userAdministrator.Users.IndexOf(user)] = _userAdministrator.CurrentUser;
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
            if (_userAdministrator.SelectedUser.Name == NameTb || _userAdministrator.SelectedUser.Grade == GradeTb ||
                _userAdministrator.SelectedUser.Education == EducationTb || _userAdministrator.SelectedUser.Email == EmailTb ||
                _userAdministrator.SelectedUser.TelephoneNumber == TelephoneNumberTb ||
                _userAdministrator.SelectedUser.UserName == UserNameTb ||
                _userAdministrator.SelectedUser.Password == PasswordTb)
            {
                if (PasswordTb == ConfirmPasswordTb)
                {
                    _userAdministrator.SelectedUser.Name = NameTb;
                    _userAdministrator.SelectedUser.Grade = GradeTb;
                    _userAdministrator.SelectedUser.Education = EducationTb;
                    _userAdministrator.SelectedUser.Email = EmailTb;
                    _userAdministrator.SelectedUser.TelephoneNumber = TelephoneNumberTb;
                    _userAdministrator.SelectedUser.UserName = UserNameTb;
                    _userAdministrator.SelectedUser.Password = PasswordTb;
                    foreach (var user in _userAdministrator.Users)
                    {
                        if (_userAdministrator.SelectedUser.Email == user.Email)
                        {
                            _userAdministrator.Users[_userAdministrator.Users.IndexOf(user)] = _userAdministrator.SelectedUser;
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
            NameTb = _userAdministrator.CurrentUser.Name;
            GradeTb = _userAdministrator.CurrentUser.Grade;
            EducationTb = _userAdministrator.CurrentUser.Education;
            EmailTb = _userAdministrator.CurrentUser.Email;
            TelephoneNumberTb = _userAdministrator.CurrentUser.TelephoneNumber;
            UserNameTb = _userAdministrator.CurrentUser.UserName;
            PasswordTb = _userAdministrator.CurrentUser.Password;

            ShowAccountSettingsPopUp = true;
        }

        public async void ShowAdminAccountPopUpMethod()
        {
            if (_userAdministrator.SelectedUser != null)
            {
                NameTb = _userAdministrator.SelectedUser.Name;
                GradeTb = _userAdministrator.SelectedUser.Grade;
                EducationTb = _userAdministrator.SelectedUser.Education;
                EmailTb = _userAdministrator.SelectedUser.Email;
                TelephoneNumberTb = _userAdministrator.SelectedUser.TelephoneNumber;
                UserNameTb = _userAdministrator.SelectedUser.UserName;
                PasswordTb = _userAdministrator.SelectedUser.Password;

                ShowAdminAccountPopup = true;
            }
            else await _message.Error("Ingen bruger valgt", "Vælg venligst en bruger du vil ændre.");
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

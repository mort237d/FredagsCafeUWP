using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class AccountSettingsClass : INotifyPropertyChanged

    {
        private Administration _administration = Administration.Instance;


        private readonly string _standardImage = "UserImages/Profile-icon.png";

        private string _nameTb;
        private string _gradeTb;
        private string _educationTb;
        private string _emailTb;
        private string _telephoneNumberTb;
        private string _userNameTb;
        private string _passwordTb;
        private string _confirmPasswordTb;

        private bool _showAccountSettingsPopUp = false;

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

        public AccountSettingsClass()
        {
            _administration.CurrentUser = new User("Morten", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678",
                "Morten", "Morten", _standardImage, "");

            NameTb = _administration.CurrentUser.Name;
            GradeTb = _administration.CurrentUser.Grade;
            EducationTb = _administration.CurrentUser.Education;
            EmailTb = _administration.CurrentUser.Email;
            TelephoneNumberTb = _administration.CurrentUser.TelephoneNumber;
            UserNameTb = _administration.CurrentUser.UserName;
            PasswordTb = _administration.CurrentUser.Password;
        }

        public void ChangeSettings()
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
                        }
                    }

                    ShowAccountSettingsPopUp = false;
                }
            }
        }

        public void ShowAccountSettingsPopUpMethod()
        {
            ShowAccountSettingsPopUp = true;
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

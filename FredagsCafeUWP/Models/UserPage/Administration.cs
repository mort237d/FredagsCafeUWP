﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Administration : INotifyPropertyChanged
    {
        #region Field

        private static Message _message;

        private readonly string _standardImage = "UserImages/Profile-icon.png";

        private string _nameTb;
        private string _gradeTb;
        private string _educationTb;
        private string _emailTb;
        private string _telephoneNumberTb;
        private string _userNameTb;
        private string _passwordTb;
        private string _confirmPasswordTb;

        private ObservableCollection<User> _users;
        private User _selectedUser;
        private User _currentUser;

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
            set { _currentUser = value; }
        }

        #endregion

        public Administration()
        {
            _message = new Message(this);

            LoadAsync();

            foreach (var user in Users)
            {
                user.Email = user.Name + "@edu.easj.dk";
            }
        }

        #region ButtonMethods

        public async void AddUser()
        {
            //TODO add image
            if (NameTb != null && GradeTb != null && EducationTb != null && EmailTb != null &&
                TelephoneNumberTb != null && UserNameTb != null && PasswordTb != null)
            {
                if (EmailTb.Contains("@edu.easj.dk") || EmailTb.Contains("@easj.dk"))
                {
                    foreach (var u in Users)
                    {
                        if (u.Email.Equals(EmailTb))
                        {
                            await _message.Error("Email findes allerede",
                                u.Email + " findes allerede til en anden bruger");
                            return;
                        }
                    }

                    if (int.TryParse(TelephoneNumberTb, out int intTelephoneNumberT) && TelephoneNumberTb.Length == 8)
                    {
                        if (PasswordTb == ConfirmPasswordTb)
                        {
                            Users.Add(new User(NameTb, GradeTb, EducationTb, EmailTb, TelephoneNumberTb, UserNameTb,
                                PasswordTb, _standardImage));

                            NameTb = null;
                            GradeTb = null;
                            EducationTb = null;
                            EmailTb = null;
                            TelephoneNumberTb = null;
                            UserNameTb = null;
                            PasswordTb = null;
                            ConfirmPasswordTb = null;
                            
                        }
                        else
                            await _message.Error("Uoverensstemmelser",
                                "Password stemmer ikke over ens med confirm password");
                    }
                    else await _message.Error("Forkert input","Telefonnummert skal være et tal på 8 cifre.");
                }
                else
                    await _message.Error("Forkert email",
                        "Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
            }
            else await _message.Error("Manglende input", "Tekstfelter mangler at blive udfyldt");
        }

        public async void RemoveUser()
        {
            if (SelectedUser != null) await _message.YesNo("Slet bruger", "Er du sikker på at du vil slette " + SelectedUser.Name + "?");
            else await _message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
        }

        #endregion

        #region Save/Load

        public async Task SaveAsync()
        {
            Debug.WriteLine("Saving user async...");
            await XmlReadWriteClass.SaveObjectToXml(Users, "administration.xml");
            Debug.WriteLine("user.count: " + Users.Count);
        }

        public async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading user async...");
                Users = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<User>>("administration.xml");
                Debug.WriteLine("user.count:" + Users.Count);
            }
            catch (Exception)
            {
                Users = new ObservableCollection<User>()
                {
                    new User("Morten", "EASJ", "Datamatiker", "morten@edu.easj.dk", "12345678", "Morten", "Morten", _standardImage),
                    new User("Daniel", "EASJ", "Datamatiker", "Daniel@edu.easj.dk", "12345678", "Daniel", "Daniel", _standardImage),
                    new User("Jacob", "EASJ", "Datamatiker", "Jacob@edu.easj.dk", "12345678", "Jacob", "Jacob", _standardImage),
                    new User("Lucas", "EASJ", "Datamatiker", "Lucas@edu.easj.dk", "12345678", "Lucas", "Lucas", _standardImage),
                    new User("Christian", "EASJ", "Datamatiker", "Christian@edu.easj.dk", "12345678", "Christian", "Christian", _standardImage)
                };
                
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

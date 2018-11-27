using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Persistency;
using Newtonsoft.Json;

namespace FredagsCafeUWP.Models
{
    public class Administration : INotifyPropertyChanged
    {
        private static Message message;

        private string standardImage = "UserImages/Profile-icon.png";

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

        private string _userTextDoc;

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

        public string UserTextDoc
        {
            get { return _userTextDoc; }
            set { _userTextDoc = value; }
        }

        #endregion

        public Administration()
        {
            message = new Message(this);
            
            loadAsync();
        }

        public void AddUser()
        {
            //TODO add image
            if (NameTb != null && GradeTb != null && EducationTb != null && EmailTb != null && TelephoneNumberTb != null && UserNameTb != null && PasswordTb != null)
            {
                if (EmailTb.Contains("@edu.easj.dk") || EmailTb.Contains("@easj.dk"))
                {
                    foreach (var u in Users)
                    {
                        if (u.Email.Equals(EmailTb))
                        {
                            message.Error("Email findes allerede", u.Email + " findes allerede til en anden bruger");
                            return;
                        }
                    }
                    if (PasswordTb == ConfirmPasswordTb)
                    {
                        Users.Add(new User(NameTb, GradeTb, EducationTb, EmailTb, TelephoneNumberTb, UserNameTb, PasswordTb, standardImage));

                        NameTb = null;
                        GradeTb = null;
                        EducationTb = null;
                        EmailTb = null;
                        TelephoneNumberTb = null;
                        UserNameTb = null;
                        PasswordTb = null;
                        ConfirmPasswordTb = null;
                        saveAsync();
                    }
                    else message.Error("Uoverensstemmelser", "Password stemmer ikke over ens med confirm password");
                }
                else message.Error("Forkert email", "Du skal bruge en \"@edu.easj.dk\" eller en \"@easj.dk\" mail.");
            }
        }

        public async void RemoveUser()
        {
            if (SelectedUser != null) await message.YesNo("Slet bruger", "Er du sikker på at du vil slette " + SelectedUser.Name + "?");
            else message.Error("Ingen bruger valgt", "Vælg venligst en bruger.");
        }

        public async void saveAsync()
        {
            Debug.WriteLine("Saving list async...");
            await XMLReadWriteClass.SaveObjectToXml<ObservableCollection<User>>(Users, "administration.xml");
            Debug.WriteLine("list.count: " + Users.Count);
        }
        private async void loadAsync()
        {
            try
            {
            Debug.WriteLine("loading list async...");
            Users = await XMLReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<User>>("administration.xml");
            Debug.WriteLine("list.count:" + Users.Count);
            OnPropertyChanged("_users");
            }
            catch (Exception e)
            {
                Users = new ObservableCollection<User>()
                {
                    new User("Morten", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Morten", "Morten", standardImage),
                    new User("Daniel", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Daniel", "Daniel", standardImage),
                    new User("Jacob", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Jacob", "Jacob", standardImage),
                    new User("Lucas", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Lucas", "Lucas", standardImage),
                    new User("Christian", "EASJ", "Datamatiker", "@edu.easj.dk", "12345678", "Christian", "Christian", standardImage)
                };
                saveAsync();
            }

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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class LogOnLogOff : INotifyPropertyChanged
    {
        #region Field

        public string UserName { get; set; }
        public string PassWord { get; set; }
        private string _wrongLogin, _wrongLoginColor;
        private UserAdministrator _userAdministrator = UserAdministrator.Instance;
        private ObservableCollection<string> _logInLogOutList;

        #endregion

        #region Props

        public string WrongLogin
        {
            get => _wrongLogin;
            set
            {
                _wrongLogin = value;
                OnPropertyChanged();
            }
        }

        public string WrongLoginColor
        {
            get => _wrongLoginColor;
            set
            {
                _wrongLoginColor = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> LogInLogOutList
        {
            get { return _logInLogOutList; }
            set { _logInLogOutList = value; }
        }

        #endregion

        private LogOnLogOff()
        {
            
        }

        #region Singleton

        private static LogOnLogOff instance;
        public static LogOnLogOff Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogOnLogOff();
                }
                return instance;
            }
        }

        #endregion

        #region ButtonMethods

        public void LogOffMethod()
        {
            LogInLogOutList.Add(_userAdministrator.CurrentUser.Name + " logged off at " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));
            
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(LoginPage));
        }

        public void CheckLogin()
        {
            bool temp = false;
            foreach (var user in _userAdministrator.Users)
            {
                if (user.UserName == UserName && user.Password == PassWord)
                {
                    _userAdministrator.CurrentUser = user;
                    LogInLogOutList.Add(UserName + " logged in at " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));

                    _userAdministrator.ButtonVisibility(user);

                    PassWord = null;

                    temp = true;
                    break;
                }
            }
            if (temp)
            {
                Frame currentFrame = Window.Current.Content as Frame;
                currentFrame.Navigate(typeof(UserPage));
            }
            else
            {
                WrongLogin = "Forkert Password eller Username";
            }
        }

        #endregion

        #region Save/Load

        public async Task LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading loginlogout async...");
                LogInLogOutList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<string>>("loginlogout.xml");
                Debug.WriteLine("loginlogoutlist.count:" + LogInLogOutList.Count);
            }
            catch (Exception)
            {
                LogInLogOutList = new ObservableCollection<string>();
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

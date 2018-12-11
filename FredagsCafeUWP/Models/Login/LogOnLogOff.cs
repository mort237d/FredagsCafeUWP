using System;
using System.Collections.Generic;
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
        public string UserName { get; set; }
        public string PassWord { get; set; }
        private string _wrongLogin;
        private string _wrongLoginColor;
        private Administration _administration = Administration.Instance;
        private ObservableCollection<string> _logInLogOutList = new ObservableCollection<string>();

        private int i = 0;
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

        public Administration Administration
        {
            get { return _administration; }
            set { _administration = value; }
        }

        #endregion

        private LogOnLogOff()
        {
            
        }

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

        public void logOffMethod()
        {
            LogInLogOutList.Add(Administration.CurrentUser.Name + " logged off at " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));



            i = 0;
            foreach (var loginlogoff in LogInLogOutList)
            {
                Debug.WriteLine(i + " " + loginlogoff);
                i++;
            }

            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(LoginPage));
        }

        public void CheckLogin()
        {
            bool temp = false;
            foreach (var user in Administration.Users)
            {
                if (user.UserName == UserName && user.Password == PassWord)
                {
                    Administration.CurrentUser = user;
                    LogInLogOutList.Add(UserName + " logged in at " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));

                    Administration.ButtonVisibility(user);

                    PassWord = null;

                    i = 0;
                    foreach (var loginlogoff in LogInLogOutList)
                    {
                        Debug.WriteLine(i + " " + loginlogoff);
                        i++;
                    }

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
                WrongLogin = "Der er sku noget galt du";
            }
        }

        #region Save/Load

        public async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading loginlogout async...");
                LogInLogOutList = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<string>>("loginlogout.xml");
                Debug.WriteLine("loginlogoutlist.count:" + LogInLogOutList.Count);
            }
            catch (Exception)
            {
                
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

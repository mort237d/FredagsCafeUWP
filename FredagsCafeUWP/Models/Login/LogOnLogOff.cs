using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities.Core;
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
        private Administration _administration = new Administration();
        private List<string> _logInLogOutList;

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

        public List<string> LogInLogOutList
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

        public LogOnLogOff()
        {
            //LoadAsync();
        }

        public void logOffMethod()
        {
            if (Administration.CurrentUser == null) //TODO Delete when program is done
            {
                foreach (var user in Administration.Users)
                {
                    if (user.Name == "Morten")
                    {
                        Administration.CurrentUser = user;
                    }
                }
            }
            
            LogInLogOutList.Add(Administration.CurrentUser.Name + " logged off at " + DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));

            //SaveAsync();

            //i = 0;
            //foreach (var loginlogoff in LogInLogOutList)
            //{
            //    Debug.WriteLine(i + " " + loginlogoff);
            //    i++;
            //}

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

                    //SaveAsync();

                    //i = 0;
                    //foreach (var loginlogoff in LogInLogOutList)
                    //{
                    //    Debug.WriteLine(i + " " + loginlogoff);
                    //    i++;
                    //}

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

        public async void SaveAsync()
        {
            Debug.WriteLine("Saving loginlogout async...");
            await XmlReadWriteClass.SaveObjectToXml(LogInLogOutList, "loginlogout.xml");
            Debug.WriteLine("loginlogoutlist.count: " + LogInLogOutList.Count);
        }
        private async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading loginlogout async...");
                LogInLogOutList = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<List<string>>("loginlogout.xml");
                Debug.WriteLine("loginlogoutlist.count:" + LogInLogOutList.Count);
            }
            catch (Exception)
            {
                SaveAsync();
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

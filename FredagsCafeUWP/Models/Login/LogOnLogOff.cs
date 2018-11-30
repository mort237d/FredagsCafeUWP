using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        private readonly Administration _administration = new Administration();
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

        #endregion
        public void logOffMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(LoginPage));
        }

        public void CheckLogin()
        {
            bool temp = false;
            foreach (var user in _administration.Users)
            {
                if (user.UserName == UserName && user.Password == PassWord)
                {
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

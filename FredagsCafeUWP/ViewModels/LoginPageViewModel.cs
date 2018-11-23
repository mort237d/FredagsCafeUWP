using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        
        public string UserName { get; set; }
        public string PassWord { get; set; }
        private string _wrongLogin;
        private string _wrongLoginColor;
        public RelayCommand LoginRelayCommand { get; set; }


        private Administration administration = new Administration();
        ObservableCollection<User> _users = new ObservableCollection<User>();

        public LoginPageViewModel()
        {
            LoginRelayCommand = new RelayCommand(CheckLogin);
        }

        private void CheckLogin()
        {

            foreach (var User in administration.Users)
            {
                if (User.UserName == UserName && User.Password == PassWord)
                {
                    Frame currentFrame = Window.Current.Content as Frame;
                    currentFrame.Navigate(typeof(UserPage));
                    break;
                }
                else
                {
                    WrongLogin = "Der er sku noget galt du";
                }
            }
        }


        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        public string WrongLogin
        {
            get { return _wrongLogin; }
            set
            {
                _wrongLogin = value;
                OnPropertyChanged();
            }
        }

        public string WrongLoginColor
        {
            get { return _wrongLoginColor; }
            set
            {
                _wrongLoginColor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}

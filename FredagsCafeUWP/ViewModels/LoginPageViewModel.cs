using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginPageViewModel : INotifyPropertyChanged
    {
        private string _wrongLogin;
        private string _wrongLoginColor;
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public RelayCommand LoginRelayCommand { get; set; }


        private readonly Administration _administration = new Administration();
        private ObservableCollection<User> _users = new ObservableCollection<User>();

        public LoginPageViewModel()
        {
            LoginRelayCommand = new RelayCommand(CheckLogin);
        }

        private void CheckLogin()
        {

            foreach (var user in _administration.Users)
            {
                if (user.UserName == UserName && user.Password == PassWord)
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
            get => _users;
            set => _users = value;
        }

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

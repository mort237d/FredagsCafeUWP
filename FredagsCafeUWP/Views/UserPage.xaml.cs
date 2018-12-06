using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public sealed partial class UserPage : Page
    {
        private Stock _stock = Stock.Instance;
        private Sale _sale = Sale.Instance;
        private EventPage _eventPage = EventPage.Instance;
        private StatListClass _statListClass = StatListClass.Instance;
        private Administration _administration = Administration.Instance;
        private LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
        private Encrypt _encrypt = new Encrypt();

        public UserPage()
        {
            InitializeComponent();
            #region TitleBar

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Color.FromArgb(1,108,160,220);
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 108, 160, 220);

            #endregion

            _stock.LoadAsync();
            _sale.LoadAsync();
            _statListClass.LoadAsync();
            _administration.LoadAsync();
            _logOnLogOff.LoadAsync();
            _eventPage.LoadAsync();

            //adm decrypt
            foreach (var user in _administration.Users)
            {
                user.Name = _encrypt.DeCrypt(user.Name); 
                user.Admin = _encrypt.DeCrypt(user.Admin);
                user.Education = _encrypt.DeCrypt(user.Education);
                user.Email = _encrypt.DeCrypt(user.Email);
                user.Grade = _encrypt.DeCrypt(user.Grade);
                user.Password = _encrypt.DeCrypt(user.Password);
                user.TelephoneNumber = _encrypt.DeCrypt(user.TelephoneNumber);
                user.UserName = _encrypt.DeCrypt(user.UserName);
                user.ImageSource = _encrypt.DeCrypt(user.ImageSource);
            }

            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += this.OnCloseRequest;

        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;

            //adm encrypt
            foreach (var user in _administration.Users)
            {
                user.Name = _encrypt.Encrypting(user.Name);
                user.Admin = _encrypt.Encrypting(user.Admin);
                user.Education = _encrypt.Encrypting(user.Education);
                user.Email = _encrypt.Encrypting(user.Email);
                user.Grade = _encrypt.Encrypting(user.Grade);
                user.Password = _encrypt.Encrypting(user.Password);
                user.TelephoneNumber = _encrypt.Encrypting(user.TelephoneNumber);
                user.UserName = _encrypt.Encrypting(user.UserName);
                user.ImageSource = _encrypt.Encrypting(user.ImageSource);
            }

            await _stock.SaveAsync();
            await _sale.SaveAsync();
            await _statListClass.SaveAsync();
            await _administration.SaveAsync();
            await _logOnLogOff.SaveAsync();
            await _eventPage.SaveAsync();

            CoreApplication.Exit();
        }
    }
}

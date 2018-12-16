using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public sealed partial class LoginPage : Page
    {
        private Encrypt _encrypt = new Encrypt();
        public LoginPage()
        {
            InitializeComponent();
            #region TitleBar

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Color.FromArgb(1, 108, 160, 220);
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 108, 160, 220);

            #endregion

            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += this.OnCloseRequest;

        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;

            _encrypt.EncryptUsers();

            foreach (var product in StockAdministrator.Instance.Products)
            {
                product.AmountToBeSold = 0;
            }

            Debug.WriteLine("Closing: ");
            foreach (var user in UserAdministrator.Instance.Users)
            {
                Debug.WriteLine(user.UserName);
            }

            CoreApplication.Exit();
        }
    }
}

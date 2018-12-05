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

            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += this.OnCloseRequest;
        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;
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

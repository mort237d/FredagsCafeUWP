using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.UserPage;

namespace FredagsCafeUWP
{
    public sealed partial class UserPage : Page
    {
        private StockAdministrator _stockAdministrator = StockAdministrator.Instance;
        private SaleAdministrator _sale = SaleAdministrator.Instance;
        private EventAdministrator _eventAdministrator = EventAdministrator.Instance;
        private StatisticsAdministrator _statisticsAdministrator = StatisticsAdministrator.Instance;
        private UserAdministrator _userAdministrator = UserAdministrator.Instance;
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

            Loader();

            _encrypt.DecryptUsers();
            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += this.OnCloseRequest;

        }

        private async Task Loader()
        {
            await _userAdministrator.LoadAsync();
            await _stockAdministrator.LoadAsync();
            await _sale.LoadAsync();
            await _statisticsAdministrator.LoadAsync();
            await _logOnLogOff.LoadAsync();
            await _eventAdministrator.LoadAsync();
        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;

            foreach (var product in _stockAdministrator.Products)
            {
                product.AmountToBeSold = 0;
            }

            _encrypt.EncryptUsers();

            await Saver();

            CoreApplication.Exit();
        }

        private async Task Saver()
        {
            await XmlReadWrite.SaveAsync(_userAdministrator.Users, "userAdministrator");
            await XmlReadWrite.SaveAsync(_stockAdministrator.Products, "stockAdministrator");
            await XmlReadWrite.SaveAsync(_sale.Receipts, "receipt");
            await XmlReadWrite.SaveAsync(_statisticsAdministrator.StatList, "stats");
            await XmlReadWrite.SaveAsync(_statisticsAdministrator.ProductGraphList, "productStats");
            await XmlReadWrite.SaveAsync(_logOnLogOff.LogInLogOutList, "loginlogout");
            await XmlReadWrite.SaveAsync(_eventAdministrator.Events, "events");
        }

        private void Comboboxo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Comboboxo.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)Comboboxo.SelectedItem).Content.ToString();
                tbcombo.Text = selectedContent;
                Comboboxo.SelectedItem = null;
            }
            if (JohnHitler.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)JohnHitler.SelectedItem).Content.ToString();
                tcombo.Text = selectedContent;
                JohnHitler.SelectedItem = null;
            }
            if (ComboboxSort.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ComboboxSort.SelectedItem).Content.ToString();
                TBComboboxSorted.Text = selectedContent;
                ComboboxSort2.SelectedItem = null;
            }
        }

        private void Comboboxo_OnSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxSort2.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ComboboxSort2.SelectedItem).Content.ToString();
                TBComboboxSorted.Text = selectedContent;
                ComboboxSort.SelectedItem = null;
            }
        }
    }
}

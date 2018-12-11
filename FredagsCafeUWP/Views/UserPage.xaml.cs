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

            Loader();

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

        private void Loader()
        {
            _administration.LoadAsync();
            _stock.LoadAsync();
            _sale.LoadAsync();
            _statListClass.LoadAsync();
            _logOnLogOff.LoadAsync();
            _eventPage.LoadAsync();
        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;

            foreach (var product in _stock.Products)
            {
                product.AmountToBeSold = 0;
            }

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

            await Saver();

            CoreApplication.Exit();
        }

        private async System.Threading.Tasks.Task Saver()
        {
            await XmlReadWriteClass.SaveAsync(_administration.Users, "administration");
            await XmlReadWriteClass.SaveAsync(_stock.Products, "stock");
            await XmlReadWriteClass.SaveAsync(_sale.Receipts, "receipt");
            await XmlReadWriteClass.SaveAsync(_statListClass.StatList, "stats");
            await XmlReadWriteClass.SaveAsync(_statListClass.ProductGraphList, "productStats");
            await XmlReadWriteClass.SaveAsync(_logOnLogOff.LogInLogOutList, "loginlogout");
            await XmlReadWriteClass.SaveAsync(_eventPage.Events, "events");
        }

        private void Comboboxo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Comboboxo.SelectedItem != null)
            {
                string temp = ((ComboBoxItem)Comboboxo.SelectedItem).Content.ToString();
                tbcombo.Text = temp;
            }
            if (JohnHitler.SelectedItem != null)
            {
                string temp2 = ((ComboBoxItem)JohnHitler.SelectedItem).Content.ToString();
                tcombo.Text = temp2;
            }
            if (ComboboxSort.SelectedItem != null)
            {
                string temp3 = ((ComboBoxItem)ComboboxSort.SelectedItem).Content.ToString();
                TBComboboxSorted.Text = temp3;
            }

            if (ComboboxSort2.SelectedItem != null)
            {
                string temp4 = ((ComboBoxItem) ComboboxSort2.SelectedItem).Content.ToString();
                TBComboboxSorted.Text = temp4;
            }
        }
    }
}

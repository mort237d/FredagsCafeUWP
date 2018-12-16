using System;
using System.Diagnostics;
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
        private Encrypt _encrypt = new Encrypt();
        public UserPage()
        {
            InitializeComponent();
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

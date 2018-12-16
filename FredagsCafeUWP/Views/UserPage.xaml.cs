using System;
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
        public UserPage()
        {
            InitializeComponent();
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

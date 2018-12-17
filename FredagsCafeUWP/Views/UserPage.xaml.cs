using Windows.UI.Xaml.Controls;

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
                Tbcombo.Text = selectedContent;
                Comboboxo.SelectedItem = null;
            }
            if (JohnHitler.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)JohnHitler.SelectedItem).Content.ToString();
                Tcombo.Text = selectedContent;
                JohnHitler.SelectedItem = null;
            }
            if (ComboboxSort.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ComboboxSort.SelectedItem).Content.ToString();
                TbComboboxSorted.Text = selectedContent;
                ComboboxSort2.SelectedItem = null;
            }
        }

        private void Comboboxo_OnSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxSort2.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ComboboxSort2.SelectedItem).Content.ToString();
                TbComboboxSorted.Text = selectedContent;
                ComboboxSort.SelectedItem = null;
            }
        }
    }
}

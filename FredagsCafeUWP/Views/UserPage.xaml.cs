using Windows.UI.Xaml.Controls;

namespace FredagsCafeUWP
{
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
        }

        private void Cb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddTypeCb.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)AddTypeCb.SelectedItem).Content.ToString();
                Tbcombo.Text = selectedContent;
            }
            if (ChangeTypeCb.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ChangeTypeCb.SelectedItem).Content.ToString();
                Tcombo.Text = selectedContent;
            }
            if (ComboboxSort.SelectedItem != null)
            {
                string selectedContent = ((ComboBoxItem)ComboboxSort.SelectedItem).Content.ToString();
                TbComboboxSorted.Text = selectedContent;
                ComboboxSort2.SelectedItem = null;
            }
        }

        private void Cb_OnSelectionChanged2(object sender, SelectionChangedEventArgs e)
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

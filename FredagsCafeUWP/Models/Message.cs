using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FredagsCafeUWP.Models
{
    internal class Message
    {
        #region Field

        private readonly Stock _stock;
        private readonly Administration _administration;
        private Sale _sale;

        #endregion

        #region Constructors

        public Message(Administration administration)
        {
            _administration = administration;
        }
        public Message(Stock stock)
        {
            _stock = stock;
        }
        public Message(Sale sale)
        {
            _sale = sale;
        }

        #endregion

        #region Methods

        public async Task Error(string title, string content)
        {
            await new MessageDialog(content, title).ShowAsync();
        }

        public async Task YesNo(string title, string content)
        {
            var yesCommand = new UICommand("Ja", cmd => { });
            var noCommand = new UICommand("Nej", cmd => { });

            var dialog = new MessageDialog(content, title) {Options = MessageDialogOptions.None};
            dialog.Commands.Add(yesCommand);

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            if (noCommand != null)
            {
                dialog.Commands.Add(noCommand);
                dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
            }

            var command = await dialog.ShowAsync();

            if (command == yesCommand)
            {
                Debug.WriteLine("Yes");
                if (title == "Slet produkt") _stock.Products.Remove(_stock.SelectedProduct);
                if (title == "Slet bruger")
                {
                    _administration.Users.Remove(_administration.SelectedUser);
                    _administration.SaveAsync();
                }
            }
            else if (command == noCommand)
            {
                Debug.WriteLine("No");
            }
        }

        #endregion
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FredagsCafeUWP.Models.AddProduct;

namespace FredagsCafeUWP.Models
{
    internal class Message
    {
        #region Field

        private readonly Stock _stock;
        private readonly Administration _administration;
        private Sale _sale;
        private EventPage _eventPage;
        private AddProductClass _addProductClass;

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

        public Message(EventPage eventPage)
        {
            _eventPage = eventPage;
        }

        public Message(AddProductClass addProductClass)
        {
            _addProductClass = addProductClass;
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
                if (title == "Slet produkt")
                {
                    _stock.Products.Remove(_stock.SelectedProduct);
                    _stock.SaveAsync();
                }
                if (title == "Slet bruger")
                {
                    _administration.Users.Remove(_administration.SelectedUser);
                    _administration.SaveAsync();
                }
                if (title == "Slet bruger af eventet")
                {
                    _eventPage.SelectedEvent.EventsUsers.Remove(_eventPage.SelectedEventUser);
                    _eventPage.SaveAsync();
                }
                if (title == "Slet event")
                {
                    _eventPage.Events.Remove(_eventPage.SelectedEvent);
                    _eventPage.SaveAsync();
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

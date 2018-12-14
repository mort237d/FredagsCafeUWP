using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FredagsCafeUWP.Models.UserPage;

namespace FredagsCafeUWP.Models
{
    internal class Message
    {
        #region Field

        private StockAdministrator _stockAdministrator;
        private UserAdministrator _userAdministrator;
        private SaleAdministrator _sale;
        private EventAdministrator _eventAdministrator;
        private AccountSettings _accountSettings;

        #endregion

        public Message(StockAdministrator stockAdministrator)
        {
            _stockAdministrator = stockAdministrator;
        }

        public Message(UserAdministrator userAdministrator)
        {
            _userAdministrator = userAdministrator;
        }

        public Message(SaleAdministrator sale)
        {
            _sale = sale;
        }

        public Message(EventAdministrator eventAdministrator)
        {
            _eventAdministrator = eventAdministrator;
        }

        public Message(AccountSettings accountSettings)
        {
            _accountSettings = accountSettings;
        }

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
                if (title == "Slet produkt")_stockAdministrator.Products.Remove(_stockAdministrator.SelectedProduct);
                else if (title == "Slet bruger")_userAdministrator.Users.Remove(_userAdministrator.SelectedUser);
                else if (title == "Slet bruger af eventet")_eventAdministrator.SelectedEvent.EventsUsers.Remove(_eventAdministrator.SelectedEventUser);
                else if (title == "Slet event")_eventAdministrator.Events.Remove(_eventAdministrator.SelectedEvent);
                else if (title == "Giv admin videre")
                {
                    _userAdministrator.CurrentUser.Admin = null;
                    foreach (var user in _userAdministrator.Users) if (user.Email == _userAdministrator.CurrentUser.Email) user.Admin = null;
                    _userAdministrator.SelectedUser.Admin = "Admin";

                    _userAdministrator.ButtonVisibility(_userAdministrator.CurrentUser);
                }
            }
            else if (command == noCommand)
            {
            }
        }

        #endregion
    }
}

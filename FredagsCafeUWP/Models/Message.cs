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

        private Stock _stock;
        private readonly Administration _administration;
        private Sale _sale;
        private EventPage _eventPage;

        #endregion
        
        public Message(Stock stock)
        {
            _stock = stock;
        }

        public Message(Administration administration)
        {
            _administration = administration;
        }

        public Message(Sale sale)
        {
            _sale = sale;
        }

        public Message(EventPage eventPage)
        {
            _eventPage = eventPage;
        }

        //private static Message instance;
        //public static Message Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Message();
        //        }
        //        return instance;
        //    }
        //}

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
                if (title == "Slet produkt")_stock.Products.Remove(_stock.SelectedProduct);
                if (title == "Slet bruger")_administration.Users.Remove(_administration.SelectedUser);
                if (title == "Slet bruger af eventet")_eventPage.SelectedEvent.EventsUsers.Remove(_eventPage.SelectedEventUser);
                if (title == "Slet event")_eventPage.Events.Remove(_eventPage.SelectedEvent);
                }
            else if (command == noCommand)
            {
                Debug.WriteLine("No");
            }
        }

        #endregion
    }
}

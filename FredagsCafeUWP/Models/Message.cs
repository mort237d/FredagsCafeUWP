using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;
using Windows.UI.Popups;

namespace FredagsCafeUWP.Models
{
    class Message
    {
        private Stock stock;
        private Administration administration;
        private Sale sale;

        public Message(Administration administration)
        {
            this.administration = administration;
        }
        public Message(Stock stock)
        {
            this.stock = stock;
        }
        public Message(Sale sale)
        {
            this.sale = sale;
        }

        public async Task Error(string title, string content)
        {
            await new MessageDialog(content, title).ShowAsync();
        }

        public async Task YesNo(string title, string content)
        {
            var yesCommand = new UICommand("Ja", cmd => { });
            var noCommand = new UICommand("Nej", cmd => { });

            var dialog = new MessageDialog(content, title);
            dialog.Options = MessageDialogOptions.None;
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
                if (title == "Slet produkt") stock.Products.Remove(stock.SelectedProduct);
                if (title == "Slet bruger") administration.Users.Remove(administration.SelectedUser);
            }
            else if (command == noCommand)
            {
                Debug.WriteLine("No");
            }
        }
    }
}

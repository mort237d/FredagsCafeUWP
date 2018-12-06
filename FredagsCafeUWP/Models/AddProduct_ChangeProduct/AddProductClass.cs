using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models.AddProduct
{
    public class AddProductClass : INotifyPropertyChanged
    {
        private Message _message = Message.Instance;

        private Stock _stock = Stock.Instance;

        

        public string _colorRed = "Red";
        public string _colorGreen = "ForestGreen";

        public AddProductClass()
        {
        }

        
        
        

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

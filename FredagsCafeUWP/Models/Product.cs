using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class Product : INotifyPropertyChanged
    {
        #region Fields
        private double _buyingPrice;
        private double _sellingPrice;
        private string _name;
        private int _amount;
        private int _amountSold;
        private string _imageSource;
        #endregion

        #region Props
        public double BuyingPrice
        {
            get { return _buyingPrice; }
            set { _buyingPrice = value; }
        }

        public double SellingPrice
        {
            get { return _sellingPrice; }
            set
            {
                _sellingPrice = value; 
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value; 
                OnPropertyChanged();
            }
        }

        public int AmountSold
        {
            get { return _amountSold; }
            set { _amountSold = value; }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; }
        }

        #endregion

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
        }

        public Product()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

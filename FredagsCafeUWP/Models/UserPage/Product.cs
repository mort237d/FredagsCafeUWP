using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Product : INotifyPropertyChanged
    {
        #region Fields
        private double _buyingPrice;
        private double _sellingPrice;
        private string _name;
        private int _amountInStock;
        private int _amountSold;
        private string _imageSource;
        private string _foregroundColor = "ForestGreen";
        private int _amountToBeSold;
        private int _discountAtThisAmount = 3;
        private double _discountPricePerItem = 4;
        #endregion

        #region Constructors
        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, string foregroundColor)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;
        }

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, string foregroundColor, int amountToBeSold)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;
            AmountToBeSold = amountToBeSold;
        }

        public Product()
        {

        }

        #endregion

        #region Props
        public double BuyingPrice
        {
            get => _buyingPrice;
            set
            {
                _buyingPrice = value; 
                OnPropertyChanged();
            }
        }

        public double SellingPrice
        {
            get => _sellingPrice;
            set
            {
                _sellingPrice = value; 
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amountInStock;
            set
            {
                _amountInStock = value; 
                OnPropertyChanged();
            }
        }

        public int AmountSold
        {
            get => _amountSold;
            set
            {
                _amountSold = value; 
                OnPropertyChanged();
            }
        }

        public string ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }

        public string ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value; 
                OnPropertyChanged();
            }
        }

        public int AmountToBeSold
        {
            get => _amountToBeSold;
            set
            {
                _amountToBeSold = value;
                OnPropertyChanged();
            }
        }

        public int DiscountAtThisAmount
        {
            get { return _discountAtThisAmount; }
            set { _discountAtThisAmount = value; }
        }

        public double DiscountPricePerItem
        {
            get { return _discountPricePerItem; }
            set { _discountPricePerItem = value; }
        }

        #endregion

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

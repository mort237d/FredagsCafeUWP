using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Product : INotifyPropertyChanged
    {
        #region Field
        private double _buyingPrice, _sellingPrice, _discountPricePerItem;
        private string _name, _imageSource, _foregroundColor;
        private int _amountInStock, _amountSold, _amountToBeSold, _discountAtThisAmount;
        private EnumCategory.ProductCategory _category;

        #endregion

        #region Constructors
        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, string foregroundColor, EnumCategory.ProductCategory productCategory, int discountAtThisAmount, double discountPricePerItem)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;
            Category = productCategory;
            DiscountAtThisAmount = discountAtThisAmount;
            DiscountPricePerItem = discountPricePerItem;
        }

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, string foregroundColor, int amountToBeSold, int discountAtThisAmount, double discountPricePerItem)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;
            AmountToBeSold = amountToBeSold;
            DiscountAtThisAmount = discountAtThisAmount;
            DiscountPricePerItem = discountPricePerItem;
        }

        public Product(string name, int amountToBeSold)
        {
            Name = name;
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
            set
            {
                _imageSource = value; 
                OnPropertyChanged();
            }
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
            get => _discountAtThisAmount;
            set
            {
                _discountAtThisAmount = value; 
                OnPropertyChanged();
            }
        }

        public double DiscountPricePerItem
        {
            get => _discountPricePerItem;
            set
            {
                _discountPricePerItem = value;
                OnPropertyChanged();
            }
        }

        public EnumCategory.ProductCategory Category
        {
            get { return _category; }
            set { _category = value; }
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

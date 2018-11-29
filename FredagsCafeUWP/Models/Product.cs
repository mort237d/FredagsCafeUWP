using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.ViewModels;

namespace FredagsCafeUWP.Models
{
    public class Product : INotifyPropertyChanged
    {
        #region Fields
        private double _buyingPrice;
        private double _sellingPrice;
        private string _name;
        private int _amount;
        private int _amountSold;
        private string _imageSource;
        private string _foregroundColor;
        private int _amountToBeSold = 1;

        

        private UserViewModel _userViewModel;
        #endregion

        #region Constructors

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, string foregroundColor, UserViewModel userViewModel)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;

            _userViewModel = userViewModel;
        }

        public Product(double buyingPrice, double sellingPrice, string name, int amountToBeSold)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
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
            get => _amount;
            set
            {
                _amount = value; 
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
                if(_userViewModel != null)
                    _userViewModel.Sale.TotalTb = TotalTbMethod();
            }
        }
        #endregion

        public double TotalTbMethod()
        {
            double temp = 0;
            foreach (var product in _userViewModel.Stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    temp += product.AmountToBeSold * product.SellingPrice;
                }
            }
            return temp;
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

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
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
        private Color _foregroundColor;
        private int _amountToBeSold = 1;

        private UserViewModel userViewModel;
        #endregion

        #region Props
        public double BuyingPrice
        {
            get { return _buyingPrice; }
            set
            {
                _buyingPrice = value; 
                OnPropertyChanged();
            }
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
                OnPropertyChanged();
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
            set
            {
                _amountSold = value; 
                OnPropertyChanged();
            }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; }
        }

        public Color ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                _foregroundColor = value; 
                OnPropertyChanged();
            }
        }

        public int AmountToBeSold
        {
            get { return _amountToBeSold; }
            set
            {
                _amountToBeSold = value;
                OnPropertyChanged();
                userViewModel.Sale.TotalTB = TotalTBMethod();
            }
        }

        public double TotalTBMethod()
        {
            double temp = 0;
            foreach (var product in userViewModel.Stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    temp += product.AmountToBeSold * product.SellingPrice;
                }
            }
            return temp;
        }

        #endregion

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, Brush foregroundColor, UserViewModel userViewModel)
        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold, string imageSource, Color foregroundColor)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
            ImageSource = imageSource;
            ForegroundColor = foregroundColor;

            this.userViewModel = userViewModel;
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

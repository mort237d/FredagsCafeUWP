using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    class Sale : INotifyPropertyChanged
    {
        private ObservableCollection<Receipt> _receipts;
        private ObservableCollection<Product> _basket;


        private Stock stock = new Stock();
        private Product _selectedProduct;


        private double _total;

        public Sale()
        {
            Receipts = new ObservableCollection<Receipt>()
            {
                new Receipt(424, "no note", 0),
                new Receipt(3423, "Drugs and drugs", 1)
            };
        }

        public ObservableCollection<Receipt> Receipts
        {
            get { return _receipts; }
            set { _receipts = value; }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public ObservableCollection<Product> Basket
        {
            get { return _basket; }
            set { _basket = value; }
        }

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value; 
                OnPropertyChanged();
            }
        }

        public void AddOneFromToBeSold()
        {
            if (SelectedProduct != null) SelectedProduct.AmountToBeSold++;
        }

        public void RemoveOneFromToBeSold()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0) SelectedProduct.AmountToBeSold--;
        }

        public double SubTotal()
        {
            double subTotal = 0;
            foreach (var item in Stock.Products)
            {
                if (item.AmountToBeSold > 0)
                {
                    for (int i = item.AmountToBeSold; i >= 0; i--)
                    {
                        subTotal += item.SellingPrice;
                    }
                }
            }
            return Math.Round(subTotal);
        }

        public void CompleteSale()
        {
            Receipts.Insert(0, new Receipt(SubTotal(), "", Receipts.Count));
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using Newtonsoft.Json.Serialization;

namespace FredagsCafeUWP
{
    class Sale : INotifyPropertyChanged
    {
        private ObservableCollection<Receipt> _receipts;
        private ObservableCollection<Product> _basket;

        private Brush _colorRed = new SolidColorBrush(Colors.Red);
        private Brush _colorGreen = new SolidColorBrush(Colors.ForestGreen);

        private Message message;
        private Stock stock = new Stock();
        private Product _selectedProduct;


        private double _totalTB;

        public Sale()
        {
            Receipts = new ObservableCollection<Receipt>()
            {
                new Receipt(424, "no note", 1),
                new Receipt(3423, "Drugs and drugs", 0)
            };
            Basket = new ObservableCollection<Product>()
            {
                new Product(66, 67, "Tuborg Classic", 22, 2, "ProductImages/TuborgClassic.png", _colorGreen),
                new Product(55, 63, "Grøn Tuborg", 21, 13, "ProductImages/GrønTuborg.png", _colorGreen),
                new Product(55, 63, "Tuborg Gylden Dame", 24, 13, "ProductImages/TuborgGuldDame.png", _colorGreen),
                new Product(55, 63, "Carlsberg", 32, 13, "ProductImages/Carlsberg.png", _colorGreen),
                new Product(55, 63, "Cola Zero", 28, 13, "ProductImages/ColaZero.png", _colorGreen),
                new Product(55, 63, "Cola", 24, 13, "ProductImages/Cola.png", _colorGreen),
                new Product(55, 63, "Mokai", 29, 13, "ProductImages/Mokai.png", _colorGreen),
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

        public double TotalTB
        {
            get { return _totalTB; }
            set
            {
                _totalTB = SubTotal(); 
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
            bool isNotInstuck = false;

            foreach (var product in Stock.Products)
            {
                if (product.AmountToBeSold > product.Amount)
                {
                    isNotInstuck = true;
                    break;
                }
            }

            if (!isNotInstuck)
            {
                foreach (var product in Stock.Products)
                {
                    if (product.AmountToBeSold > 0)
                    {
                        for (int i = product.AmountToBeSold; i > 0; i--)
                        {
                            subTotal += product.SellingPrice;
                            product.Amount--;
                        }
                    }
                }
            }
            //Todo else if isNotInstuck error not in stock foreach
            return Math.Round(subTotal);
            
        }

        public void CompleteSale()
        {
            double temp = SubTotal();
            if (temp > 0)
            {
                Receipts.Insert(0, new Receipt(SubTotal(), "", Receipts.Count));
                foreach (var product in Stock.Products)
                {
                    product.AmountToBeSold = 0;
                }
            }
            //ToDO else error nothing to sell

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

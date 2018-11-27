using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Media;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using Newtonsoft.Json.Serialization;

namespace FredagsCafeUWP
{
    class Sale : INotifyPropertyChanged
    {
        private ObservableCollection<Receipt> _receipts;
        private static List<Product> _basket;

        private Brush _colorRed = new SolidColorBrush(Colors.Red);
        private Brush _colorGreen = new SolidColorBrush(Colors.ForestGreen);

        private Message message;
        private Stock stock = new Stock();
        private Product _selectedProduct;

        private double _totalTB;
        

        public Sale()
        {
            message = new Message(this);

            Basket = new List<Product>();

            Receipts = new ObservableCollection<Receipt>()
            {
                //new Receipt(424, "no note", 1),
                //new Receipt(3423, "Drugs and drugs", 0)
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

        public List<Product> Basket
        {
            get { return _basket; }
            set { _basket = value; }
        }

        public double TotalTB
        {
            get { return _totalTB; }
            set
            {
                _totalTB = value;
                OnPropertyChanged();
            }
        }

        public void AddOneFromToBeSold()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.AmountToBeSold++;
                TotalTB += SelectedProduct.SellingPrice;
            }
        }

        public void RemoveOneFromToBeSold()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0)
            {
                SelectedProduct.AmountToBeSold--;
                TotalTB -= SelectedProduct.SellingPrice;
            }
        }

        public void AddItemsToBasket()
        {
            foreach (var product in stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    Basket.Add(new Product(product.BuyingPrice,product.SellingPrice,product.Name,product.AmountToBeSold));       
                }
            }
        }

        public double SubTotal()
        {
            double subTotal = 0;
            bool isNotInstuck = false;
            string productsNotInStuck = null;

            foreach (var product in Stock.Products)
            {
                if (product.AmountToBeSold > product.Amount)
                {
                    isNotInstuck = true;
                    productsNotInStuck += product.Name + ": " + product.Amount + " stk.\n";
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
            else
            {
                message.Error("Ikke nok på lager", "Det gælder disse produkter:\n" + productsNotInStuck);
                return -1;
            }
            return Math.Round(subTotal);
            
        }

        public void CompleteSale()
        {
            string productAmountLow = null;

            double temp = SubTotal();
            if (temp > 0)
            {
                AddItemsToBasket();
                Receipts.Insert(0, new Receipt(temp, "", Receipts.Count, Basket));
                Basket.Clear();

                foreach (var product in Stock.Products)
                {
                    //product.AmountToBeSold = 0;
                    if (product.Amount < 10 && product.ForegroundColor != _colorRed)
                    {
                        product.ForegroundColor = _colorRed;
                        productAmountLow += product.Name + ": " + product.Amount + " stk.\n";
                    }
                }

                if (productAmountLow != null) message.Error("Lavt lager", "Der er lavt lager af:\n" + productAmountLow);
            }
            else if (temp != -1) message.Error("Ingen vare tilføjet", "Tilføj venligst vare for at betale.");
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

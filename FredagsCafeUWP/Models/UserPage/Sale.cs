using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class Sale : INotifyPropertyChanged
    {
        #region Field

        private ObservableCollection<Receipt> _receipts;
        private static List<Product> _basket;

        private readonly string _colorRed = "Red";
        private readonly string _colorGreen = "ForestGreen";

        private readonly Message _message;
        private Stock _stock = new Stock();
        private Product _selectedProduct;
        private Receipt _selectedReceipt;

        private double _totalTb;

        private int _noItems = 0;

        private StatListClass _statListClass = new StatListClass();

        #endregion

        public Sale()
        {
            
            _message = new Message(this);

            Basket = new List<Product>();

            Receipts = new ObservableCollection<Receipt>();

            LoadAsync();     
        }

        #region Props

        public ObservableCollection<Receipt> Receipts
        {
            get => _receipts;
            set
            {
                _receipts = value;
                OnPropertyChanged();
            } 
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => _selectedProduct = value;
        }

        public Stock Stock
        {
            get => _stock;
            set => _stock = value;
        }

        public List<Product> Basket
        {
            get => _basket;
            set => _basket = value;
        }

        public double TotalTb
        {
            get => _totalTb;
            set
            {
                _totalTb = value;
                OnPropertyChanged();
            }
        }

        public Receipt SelectedReceipt
        {
            get => _selectedReceipt;
            set => _selectedReceipt = value;
        }

        #endregion

        #region ButtonMethods

        public void AddProductButton()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.AmountToBeSold++;
                TotalTb += SelectedProduct.SellingPrice;
            }
        }

        public void RemoveProductButton()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0)
            {
                SelectedProduct.AmountToBeSold--;
                TotalTb -= SelectedProduct.SellingPrice;
            }
        }

        public void AddItemsToBasket()
        {
            Basket.Clear();
            foreach (var product in _stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    Basket.Add(product);
                    Debug.WriteLine(product.AmountToBeSold);
                    Basket.Last().Amount = product.AmountToBeSold;
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
                _message.Error("Ikke nok på lager", "Det gælder disse produkter:\n" + productsNotInStuck);
                return -1;
            }
            return Math.Round(subTotal);
        }
        public double SellingTotal()
        {
            double totalSaleValueSum = 0;
            foreach (var receipt in Receipts)
            {
                Debug.WriteLine("receipt: " + Receipts.Count);
                foreach (var basket in receipt.Basket)
                {
                    Debug.WriteLine("Basketcount: " + Basket.Count);
                    Debug.WriteLine("basket.Name: " +  basket.Name + " basket.AmountToBeSold: " + basket.AmountToBeSold);
                    totalSaleValueSum += basket.SellingPrice * basket.AmountToBeSold;
                }
            }
            return totalSaleValueSum;
        }
        public double BuyingTotal()
        {
            double totalBuyValueSum = 0;
            foreach (var receipt in Receipts)
            {
                foreach (var basket in receipt.Basket)
                {
                    totalBuyValueSum += basket.BuyingPrice * basket.AmountToBeSold;
                }
            }
            return totalBuyValueSum;
        }

        public async void CompleteSale()
        {
            string productAmountLow = null;
             
            double temp = SubTotal();
            if (temp > 0)
            {
                AddItemsToBasket();
                int count = Receipts.Count;
                Receipts.Insert(0, new Receipt(temp, count, Basket));
                TotalTb = _noItems;
                Stock.SaveAsync();
                SaveAsync();
                _statListClass.AddTotalSaleValue(SellingTotal(), BuyingTotal());
                _statListClass.SaveAsync();

                foreach (var product in Stock.Products)
                {
                    product.AmountToBeSold = 0;
                    if (product.Amount < 10 && product.ForegroundColor != _colorRed)
                    {
                        product.ForegroundColor = _colorRed;
                        productAmountLow += product.Name + ": " + product.Amount + " stk.\n";
                    }
                }

                if (productAmountLow != null) await _message.Error("Lavt lager", "Der er lavt lager af:\n" + productAmountLow);

            }
            else if (temp != -1) await _message.Error("Ingen vare tilføjet", "Tilføj venligst vare for at betale.");
        }

        public void CalculateTotalPrice()
        {
            double temp = 0;
            foreach (var product in Stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    temp += product.AmountToBeSold * product.SellingPrice;
                }
            }
            TotalTb = temp;
        }

        public void DeleteReceipt()
        {
            if (SelectedReceipt != null && SelectedReceipt.Color != _colorRed)
            {
                foreach (var basket in SelectedReceipt.Basket)
                {
                    foreach (var product in _stock.Products)
                    {
                        if (product.Name == basket.Name)
                        {
                            product.Amount += basket.AmountToBeSold;
                        }
                    }
                }
                

                //foreach (var product in Stock.Products)
                //{
                //    if (product.Amount > 0)
                //    {
                //        foreach (var basket in SelectedReceipt.Basket)
                //        {
                //            product.Amount += basket.Amount;
                //        }
                //    }
                //}
                SelectedReceipt.Color = _colorRed;
            }
        }

        #endregion

        #region Save/Load

        public async void SaveAsync()
        {
            Debug.WriteLine("Saving receipt async...");
            await XmlReadWriteClass.SaveObjectToXml(Receipts, "receipt.xml");
            Debug.WriteLine("receipts.count: " + Receipts.Count);
        }
        private async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading receipt async...");
                Receipts = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Receipt>>("receipt.xml");
                Debug.WriteLine("receipts.count:" + Receipts.Count);
            }
            catch (Exception)
            {
                Receipts = new ObservableCollection<Receipt>()
                {
                    new Receipt(424, 1, Basket)
                    //new Receipt(3423, "Drugs and drugs", 0)
                };
                SaveAsync();
            }
        }

        #endregion

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

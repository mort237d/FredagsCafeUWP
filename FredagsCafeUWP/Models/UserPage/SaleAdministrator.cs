using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class SaleAdministrator : INotifyPropertyChanged
    {
        #region Field

        private ObservableCollection<Receipt> _receipts;
        private static List<Product> _tempBasket;

        private readonly string _colorRed = "Red";
        private readonly string _colorGreen = "ForestGreen";

        private Message _message;
        private StockAdministrator _stockAdministrator = StockAdministrator.Instance;
        private Product _selectedProduct;
        private Receipt _selectedReceipt;

        private double _totalTb;

        private StatisticsAdministrator _statisticsAdministrator = StatisticsAdministrator.Instance;

        #endregion

        private  SaleAdministrator()
        {
            _message = new Message(this);
        }

        #region Singleton

        private static SaleAdministrator _instance;
        public static SaleAdministrator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaleAdministrator();
                }
                return _instance;
            }
        }

        #endregion

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

        public List<Product> Basket
        {
            get => _tempBasket;
            set => _tempBasket = value;
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
                DiscountedTotalcalculator();
            }
        }

        public void RemoveProductButton()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0)
            {
                SelectedProduct.AmountToBeSold--;
                DiscountedTotalcalculator();
            }
        }

        public void AddItemsToBasket()
        {
            Basket = new List<Product>();
            foreach (var product in _stockAdministrator.Products)
            {
                if (product.AmountToBeSold != 0) Basket.Add(new Product(product.BuyingPrice, product.SellingPrice, product.Name, product.Amount, product.ImageSource, "Black", product.AmountToBeSold, product.DiscountAtThisAmount, product.DiscountPricePerItem));
            }
        }


        public double SubTotal()
        {
            double subTotal = 0;
            bool isNotInstuck = false;
            string productsNotInStuck = null;

            foreach (var product in _stockAdministrator.Products)
            {
                if (product.AmountToBeSold > product.Amount)
                {
                    isNotInstuck = true;
                    productsNotInStuck += product.Name + ": " + product.Amount + " stk.\n";
                }
            }

            if (!isNotInstuck)
            {
                foreach (var product in _stockAdministrator.Products)
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
  
        public async void CompleteSale()
        {
            string productAmountLow = null;
            double total = SubTotal();

            if (total > 0)
            {
                AddItemsToBasket();
                double savings = total- DiscountedTotal(); 
                int saleNumber = Receipts.Count + 1;

                Receipts.Insert(0, new Receipt(DiscountedTotal(), saleNumber, savings, Basket));

                TotalTb = 0; //Total price is 0 after a sale

                _statisticsAdministrator.ProductViewGraph();
                _statisticsAdministrator.AddTotalSaleValue();

                foreach (var product in _stockAdministrator.Products)
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
            else if (total != -1) await _message.Error("Ingen vare tilføjet", "Tilføj venligst vare for at betale.");
        }

        public async void DeleteReceipt()
        {
            if (SelectedReceipt != null && SelectedReceipt.Color != _colorRed)
            {
                foreach (var basket in SelectedReceipt.Basket)
                {
                    foreach (var product in _stockAdministrator.Products)
                    {
                        if (product.Name == basket.Name)
                        {
                            product.Amount += basket.AmountToBeSold;
                            if (product.Amount < _stockAdministrator.MinAmount) product.ForegroundColor = _colorRed;
                            else product.ForegroundColor = _colorGreen;

                            break;
                        }
                    }
                }

                SelectedReceipt.Color = _colorRed;
                foreach (var item in SelectedReceipt.Basket)
                {
                    item.ForegroundColor = _colorRed;
                }
                _statisticsAdministrator.ProductViewGraph();
                _statisticsAdministrator.AddTotalSaleValue();
            }
            else if (SelectedReceipt.Color == _colorRed)
                await _message.Error("Transaktion er allerede slette", "Transaktion kan ikke slettes da den allerede er slettet.");
            else await _message.Error("Ingen transaktion valgt", "Vælg venligst en transaktion for at slette.");
        }

        public double DiscountedTotal()
        {
            double total = 0;

            foreach (var product in Basket)
            {
                if (product.DiscountAtThisAmount != 0 || product.DiscountPricePerItem != 0)
                {
                    total += VolumeDiscount(product.DiscountAtThisAmount, product.AmountToBeSold, product.DiscountPricePerItem, product.SellingPrice);
                }
                else total += product.AmountToBeSold * product.SellingPrice;
            }

            return total;
        }

        public void DiscountedTotalcalculator()
        {
            AddItemsToBasket();
            TotalTb = DiscountedTotal();
        }

        public double VolumeDiscount(int discountAtThisAmount, int itemsToBeDiscounted, double discountPrice, double normalPrice)
        {
            double total = itemsToBeDiscounted * normalPrice;
            discountPrice = discountPrice / discountAtThisAmount;
            int temp = 0;
            while (true)
            {
                if (itemsToBeDiscounted - discountAtThisAmount >= 0)
                {
                    temp++;
                    itemsToBeDiscounted -= discountAtThisAmount;
                }
                else break;
            }
            for (int i = (temp * discountAtThisAmount); i > 0; i--)
            {
                total -= normalPrice - discountPrice;
            }
            return Math.Round(total);
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models.UserPage
{
    public class Sale : INotifyPropertyChanged
    {
        #region Field

        private ObservableCollection<Receipt> _receipts;
        private static List<Product> _tempBasket;

        private readonly string _colorRed = "Red";
        private readonly string _colorGreen = "ForestGreen";

        private Message _message;
        private Stock _stock = Stock.Instance;
        private Product _selectedProduct;
        private Receipt _selectedReceipt;

        private double _totalTb;

        private int _noItems = 0;

        private StatListClass _statListClass = StatListClass.Instance;

        #endregion

        private  Sale()
        {
            //Basket = new List<Product>();

            Receipts = new ObservableCollection<Receipt>();

            _message = new Message(this);
        }

        #region Singleton

        private static Sale _instance;
        public static Sale Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Sale();
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
            }
        }

        public void RemoveProductButton()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0)
            {
                SelectedProduct.AmountToBeSold--;
            }
        }

        public void AddItemsToBasket()
        {
            Basket = new List<Product>();
            foreach (var product in _stock.Products)
            {
                if (product.AmountToBeSold != 0)
                {
                    Basket.Add(new Product(product.BuyingPrice, product.SellingPrice, product.Name, product.Amount, product.AmountSold, product.ImageSource, "Black", product.AmountToBeSold));
                }
            }
        }


        public double SubTotal()
        {
            double subTotal = 0;
            bool isNotInstuck = false;
            string productsNotInStuck = null;

            foreach (var product in _stock.Products)
            {
                if (product.AmountToBeSold > product.Amount)
                {
                    isNotInstuck = true;
                    productsNotInStuck += product.Name + ": " + product.Amount + " stk.\n";
                }
            }

            if (!isNotInstuck)
            {
                foreach (var product in _stock.Products)
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
                foreach (var basket in receipt.Basket)
                {
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
  
        public void ProductViewGraph()
        {
            List<Product> tempGraphList = new List<Product>();
            bool tempGraphBool = false;
            _statListClass.ProductGraphList.Clear();
            _statListClass.ProductGraphList.Add(new Product("",0));
            foreach (var receipt in Receipts)
            {
                foreach (var basket in receipt.Basket)
                {
                    tempGraphList.Clear();
                    if (_statListClass.ProductGraphList.Count == 0)
                        _statListClass.ProductGraphList.Add(new Product(basket.Name,basket.AmountToBeSold));
                    else
                    {
                        foreach (var product in _statListClass.ProductGraphList)
                        {
                            if (product.Name == basket.Name)
                            {
                                product.AmountToBeSold += basket.AmountToBeSold;
                                tempGraphBool = true;
                            }
                        }

                        if (!tempGraphBool)
                        {
                            _statListClass.ProductGraphList.Add(new Product(basket.Name, basket.AmountToBeSold));
                        }
                        else tempGraphBool = false;
                    }
                }
            }
        }
        public async void CompleteSale()
        {
            string productAmountLow = null;
            double temp = SubTotal();
            if (temp > 0)
            {
                AddItemsToBasket();
                double temp2 = DiscountedTotal();
                int count = Receipts.Count + 1;

                Receipts.Insert(0, new Receipt(temp2, count, Basket));

                TotalTb = _noItems;
                //Stock.SaveAsync();

                ProductViewGraph();
                _statListClass.AddTotalSaleValue(SellingTotal(), BuyingTotal());
                //_statListClass.SaveAsync();

                foreach (var product in _stock.Products)
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

        public async void DeleteReceipt()
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
                            if (product.Amount < _stock.MinAmount) product.ForegroundColor = _colorRed;
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
            }
            else await _message.Error("Ingen transaktion valgt", "Vælg venligst en.");
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
        //public void CalculateTotalPrice()
        //{
        //    double temp = 0;
        //    foreach (var product in Stock.Products)
        //    {
        //        if (product.AmountToBeSold != 0)
        //        {
        //            temp += product.AmountToBeSold * product.SellingPrice;
        //        }
        //    }
        //    TotalTb = temp;
        //}

        public double VolumeDiscount(int discountAtThisAmount, int itemsToBeDiscounted, double discountPrice, double normalPrice)
        {
            double total = itemsToBeDiscounted * normalPrice;
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
            return total;
        }

        #endregion

        #region Save/Load
        public async void LoadAsync()
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
                    new Receipt(424, 1, new List<Product>())
                    //new Receipt(3423, "Drugs and drugs", 0)
                };
                
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

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.UserPage;

namespace FredagsCafeUWP 
{
    public class StatisticsAdministrator : INotifyPropertyChanged
    {
        private string _colorRed = "Red";
        private static ObservableCollection<Statistics> _statList;
        private ObservableCollection<Product> _productGraphList;

        private StatisticsAdministrator()
        {
            
        }

        #region Singleton

        private static StatisticsAdministrator _instance;
        public static StatisticsAdministrator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatisticsAdministrator();
                }
                return _instance;
            }
        }

        #endregion

        #region Props
        public ObservableCollection<Statistics> StatList
        {
            get { return _statList; }
            set
            {
                _statList = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Product> ProductGraphList
        {
            get { return _productGraphList; }
            set
            {
                _productGraphList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public void AddTotalSaleValue()
        { 
            StatList.Clear();
            StatList.Add(new Statistics(SellingTotal(), "Indkomst"));
            StatList.Add(new Statistics(BuyingTotal(), "Udgifter"));
            StatList.Add(new Statistics((SellingTotal() - BuyingTotal()),"Profit"));
            StatList.Add(new Statistics(0,""));
        }

        public double SellingTotal()
        {
            double totalSaleValueSum = 0;
            foreach (var receipt in SaleAdministrator.Instance.Receipts)
            {
                

                foreach (var product in receipt.Basket)
                {
                    if (product.DiscountAtThisAmount != 0 || product.DiscountPricePerItem != 0)
                    {
                        if (product.ForegroundColor != _colorRed) totalSaleValueSum += SaleAdministrator.Instance.VolumeDiscount(product.DiscountAtThisAmount, product.AmountToBeSold, product.DiscountPricePerItem, product.SellingPrice);
                    }
                    else totalSaleValueSum += product.AmountToBeSold * product.SellingPrice;
                }
                //                foreach (var basket in receipt.Basket)
                //                {
                //                    if (basket.ForegroundColor != _colorRed)
                //                    {
                //                        totalSaleValueSum += basket.SellingPrice * basket.AmountToBeSold;
                //                    }
                //                }
            }
            return totalSaleValueSum;
        }

        public double BuyingTotal()
        {
            double totalBuyValueSum = 0;
            foreach (var receipt in SaleAdministrator.Instance.Receipts)
            {
                foreach (var basket in receipt.Basket)
                {

                    if (basket.ForegroundColor != _colorRed)
                    {
                        totalBuyValueSum += basket.BuyingPrice * basket.AmountToBeSold;
                    }
                }
            }
            return totalBuyValueSum;
        }
        
        public void ProductViewGraph()
        {
            bool tempGraphBool = false;
            ProductGraphList.Clear();
            ProductGraphList.Add(new Product("", 0));
            foreach (var receipt in SaleAdministrator.Instance.Receipts)
            {
                foreach (var basket in receipt.Basket)
                {
                    if (ProductGraphList.Count == 0)
                        ProductGraphList.Add(new Product(basket.Name, basket.AmountToBeSold));
                    else
                    {
                        foreach (var product in ProductGraphList)
                        {
                            if (product.Name == basket.Name && basket.ForegroundColor != _colorRed)
                            {
                                product.AmountToBeSold += basket.AmountToBeSold;
                                tempGraphBool = true;
                            }
                        }

                        if (!tempGraphBool && basket.ForegroundColor != _colorRed)
                        {
                            ProductGraphList.Add(new Product(basket.Name, basket.AmountToBeSold));
                        }
                        else tempGraphBool = false;
                    }
                }
            }
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

﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private static StatisticsAdministrator instance;
        public static StatisticsAdministrator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatisticsAdministrator();
                }
                return instance;
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
        
        #region save/load

        public async Task LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading stats async...");
                StatList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Statistics>>("stats.xml");
                Debug.WriteLine("stats.count:" + StatList.Count);
            }
            catch (Exception)
            {
                StatList = new ObservableCollection<Statistics>();
            }

            try
            {
                Debug.WriteLine("loading productStats async...");
                ProductGraphList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("productStats.xml");
                Debug.WriteLine("productStats.count:" + ProductGraphList.Count);
            }
            catch (Exception)
            {
                ProductGraphList = new ObservableCollection<Product>();
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

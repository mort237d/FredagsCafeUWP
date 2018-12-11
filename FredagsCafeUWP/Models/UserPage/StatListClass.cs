using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP 
{
    public class StatListClass : INotifyPropertyChanged
    {
        private static ObservableCollection<Statistics> _statList = new ObservableCollection<Statistics>();
        private ObservableCollection<Product> _productGraphList = new ObservableCollection<Product>();
//        private double totalSaleValueSum;
//        private double totalBuyValueSum;

        private StatListClass()
        {
            
        }

        private static StatListClass instance;
        public static StatListClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatListClass();
                }
                return instance;
            }
        }

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

        public void AddTotalSaleValue(double totalSaleValueSum, double totalBuyValueSum)
        { 
            
            StatList.Clear();
            StatList.Add(new Statistics(totalSaleValueSum,"Indkomst"));
            StatList.Add(new Statistics(totalBuyValueSum, "Udgifter"));
            StatList.Add(new Statistics((totalSaleValueSum-totalBuyValueSum),"Profit"));
            StatList.Add(new Statistics(0,""));

        }

        

        #region save/load

        public async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading stats async...");
                StatList = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Statistics>>("stats.xml");
                Debug.WriteLine("stats.count:" + StatList.Count);
            }
            catch (Exception)
            {
                StatList = new ObservableCollection<Statistics>();
            }

            try
            {
                Debug.WriteLine("loading productStats async...");
                ProductGraphList = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("productStats.xml");
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

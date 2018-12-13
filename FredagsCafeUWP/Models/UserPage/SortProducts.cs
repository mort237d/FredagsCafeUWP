using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models.UserPage
{
    public class SortProducts : INotifyPropertyChanged
    {
        private string _sortProductsTb;
        private string _sortProductsTb2;

        public string SortProductsTb
        {
            get => _sortProductsTb;
            set
            {
                OnPropertyChanged();
                _sortProductsTb = value;
                int temp = ConvertMethod(_sortProductsTb);
                Stock.Instance.Products = SortObservableCollectionMethodTest(Stock.Instance.Products, temp);
                OnPropertyChanged();
            }
        }
        public string SortProductsTb2
        {
            get { return _sortProductsTb2; }
            set
            {
                OnPropertyChanged();
                _sortProductsTb2 = value;
                int temp = ConvertMethod(_sortProductsTb2);
                Stock.Instance.Products = SortObservableCollectionMethodTest(Stock.Instance.Products, temp);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Product> SortObservableCollectionMethodTest(ObservableCollection<Product> inputObservableCollection, int whereToBegin)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            switch (whereToBegin)
            {
                case 1:
                    outPutObservableCollection = SortHelpBeer(inputObservableCollection);
                    break;
                case 2:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 1);
                    break;
                case 3:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 2);
                    break;
                case 4:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 3);
                    break;
                case 5:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 4);
                    break;
                case 6:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 5);
                    break;
                case 7:
                    outPutObservableCollection = SortHelp(inputObservableCollection, 6);
                    break;
            }
            return outPutObservableCollection;
        }
        private ObservableCollection<Product> SortHelpBeer(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();

            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategory)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.Category.ToString() == category) outPutObservableCollection.Add(product);
                }
            }

            return outPutObservableCollection;
        }

        private int ConvertMethod(string SortProductsTb)
        {
            int temp = 0;

            if (SortProductsTb == "Øl") temp = 1;
            else if (SortProductsTb == "Sodavand") temp = 2;
            else if (SortProductsTb == "Cider") temp = 3;
            else if (SortProductsTb == "Drink") temp = 4;
            else if (SortProductsTb == "Flaske") temp = 5;
            else if (SortProductsTb == "Shot") temp = 6;
            else if (SortProductsTb == "Andet") temp = 7;

            return temp;
        }

        private ObservableCollection<Product> SortHelp(ObservableCollection<Product> inputObservableCollection, int index)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();

            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategory)).Skip(index))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.Category.ToString() == category) outPutObservableCollection.Add(product);
                }
            }
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategory)).Take(index))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.Category.ToString() == category) outPutObservableCollection.Add(product);
                }
            }

            return outPutObservableCollection;
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

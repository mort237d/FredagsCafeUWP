using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP 
{
    public class StatListClass : INotifyPropertyChanged
    {
        private static ObservableCollection<Statistics> _statList = new ObservableCollection<Statistics>();
        private double totalSaleValueSum;
        private double totalBuyValueSum;
        

        public StatListClass()
        {
            
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

        
        #endregion

        public void AddTotalSaleValue(double totalSaleValue, double totalBuyValue)
        {
            totalSaleValueSum += totalSaleValue;
            totalBuyValueSum += totalBuyValue;
            StatList.Clear();
            StatList.Add(new Statistics(totalSaleValueSum,"Indkomst"));
            StatList.Add(new Statistics(totalBuyValue, "Udgifter"));
            StatList.Add(new Statistics((totalSaleValue-totalBuyValue),"Profit"));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

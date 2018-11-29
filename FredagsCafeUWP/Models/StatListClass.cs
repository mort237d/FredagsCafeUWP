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

        public void AddTotalSaleValue(double totalSaleValue)
        {
            totalSaleValueSum += totalSaleValue;
            if(StatList.Count > 0) StatList.RemoveAt(0);
            StatList.Add(new Statistics(totalSaleValueSum,"Indkomst"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    public class Receipt : INotifyPropertyChanged
    {
        #region Fields

        private string _saleDateTime = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss");
        private string _color;
        public string _cvr { get; set; }
        public string _phoneNumber { get; set; }
        public int SaleNumber { get; set; }
        public double SubTotal { get; set; }
        public double Savings { get; set; }
        public List<Product> Basket { get; set; }

        #endregion

        public Receipt(double subTotal, int saleNumber, double savings, List<Product> basket)
        {
            SubTotal = subTotal;
            SaleNumber = saleNumber;
            Savings = savings;
            Basket = basket;
        }

        public Receipt()
        {
            
        }

        #region Props
        public string Color
        {
            get => _color;
            set
            {
                _color = value; 
                OnPropertyChanged();
            }
        }
        #endregion

        #region Inotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}

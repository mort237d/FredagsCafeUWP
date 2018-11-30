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

        //private readonly DateTime _saleDateTime = DateTime.Now;
        private string _saleDateTime = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss");
        private string _cvr = "000000-0000";
        private string _phoneNumber = "+45 0000 0000";
        private int _saleNumber;
        private double _subTotal;
        private List<Product> _basket;

        #endregion

        public Receipt(double subTotal, int saleNumber, List<Product> basket)
        {
            SubTotal = subTotal;
            SaleNumber = saleNumber;
            _basket = basket;
        }

        public Receipt()
        {
            
        }

        #region Props

        public List<Product> Basket
        {
            get => _basket;
            set => _basket = value;
        }

        public double SubTotal
        {
            get => _subTotal;
            set => _subTotal = value;
        }

        public int SaleNumber
        {
            get { return _saleNumber; }
            set { _saleNumber = value; }
        }

        public string SaleDateTime
        {
            get { return _saleDateTime; }
            set { _saleDateTime = value; }
        }

        public void ChangeCvr(string newCvr)
        {
            if (newCvr != null && newCvr != "")
            {
                _cvr = newCvr;
            }
        }
        public void ChangePhoneNumber(string phoneNumber)
        {
            if (phoneNumber != null && phoneNumber != "")
            {
                _phoneNumber = phoneNumber;
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

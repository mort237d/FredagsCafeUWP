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

        private readonly DateTime _saleDateTime = DateTime.Now;
        private string _cvr = "000000-0000";
        private string _phoneNumber = "+45 0000 0000";
        private readonly int _saleNumber;
        private double _subTotal;
        private readonly string _note = "";
        private List<Product> _basket;

        #endregion

        public Receipt(double subTotal, string note, int saleNumber, List<Product> basket)
        {
            SubTotal = subTotal;
            _note = note;
            _saleNumber = saleNumber;
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

        public override string ToString()
        {
            return "Salgs tid:   " + _saleDateTime + "\nVirksomheds CVR " + _cvr + "\nTotal: " + _subTotal + "\nVirksomheds nummer" + _phoneNumber + "\nSalgs nummer: " + _saleNumber + "\nNote: " + _note + "\n";
        }

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

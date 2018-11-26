using System;
using System.Collections.Generic;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    class Receipt
    {
        private DateTime _saleDateTime = DateTime.Now;
        private string _cvr = "000000-0000";
        private string _phoneNumber = "+45 0000 0000";
        private int _saleNumber;
        private double _subTotal;
        private string _note = "";
        private List<Product> _basket;

        public Receipt(double subTotal, string note, int saleNumber)
        {
            _subTotal = subTotal;
            _note = note;
            _saleNumber = saleNumber;
        }

        public List<Product> Basket
        {
            get { return _basket; }
            set { _basket = value; }
        }

        public void ChangeCVR(string newCVR)
        {
            if (newCVR != null && newCVR != "")
            {
                _cvr = newCVR;
            }
        }
        public void ChangePhoneNumber(string phoneNumber)
        {
            if (phoneNumber != null && phoneNumber != "")
            {
                _phoneNumber = phoneNumber;
            }
        }

        public override string ToString()
        {
            return "Salgs tid:   " + _saleDateTime + "\nVirksomheds CVR " + _cvr + "\nTotal: " + _subTotal + "\nVirksomheds nummer" + _phoneNumber + "\nSalgs nummer: " + _saleNumber + "\nNote: " + _note + "\n".ToString();
        }
    }
}

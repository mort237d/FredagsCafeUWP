using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    class Receipt
    {
        private DateTime _saleDateTime = DateTime.Now;
        private string _cvr = "000000-0000";
        private string _phoneNumber;
        private int _saleNumber;
        private double _subTotal;
        private double _tax;
        private double _total;
        private string _note;
        private List<Product> _basket;

        public Receipt(double subTotal, string note)
        {
            _subTotal = subTotal;
            _note = note;
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
        public void ChangeTax(double tax)
        {
            if (tax != null && tax >= 0)
            {
                _tax = tax;
            }
        }



        public override string ToString()
        {
            return "Salgs tid:   " + _saleDateTime + "\nVirksomheds CVR " + _cvr + "\nTotal u. skat: " + _subTotal + "\nMoms: " + _tax + "\nTotal: " + _total + "\nVirksomheds nummer" + _phoneNumber + "\nSalgs nummer: " + _saleNumber + "\nNote: " + _note + "\n".ToString();
        }
    }
}

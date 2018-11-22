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
        private DateTime _saleDateTime;
        private string _cvr;
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

        public override string ToString()
        {
            return "Salgs tid:   " + _saleDateTime + "\nVirksomheds CVR " + _cvr + "\nVirksomheds nummer" + _phoneNumber + "\nSalgs nummer: " + _saleNumber + "\nTotal u. skat: " + _subTotal + "\nSkat: " + _tax + "\nTotal: " + _total + "Note: " + _note.ToString();
        }
    }
}

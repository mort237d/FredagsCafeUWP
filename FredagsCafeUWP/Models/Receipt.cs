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

        public Receipt(DateTime saleDateTime, string cvr, string phoneNumber, int saleNumber, double subTotal, double tax, double total, string note, List<Product> basket)
        {
            _saleDateTime = saleDateTime;
            _cvr = cvr;
            _phoneNumber = phoneNumber;
            _saleNumber = saleNumber;
            _subTotal = subTotal;
            _tax = tax;
            _total = total;
            _note = note;
            _basket = basket;
        }

        public override string ToString()
        {
            return "Sales time" + _saleDateTime.ToString();
        }
    }
}

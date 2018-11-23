using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FredagsCafeUWP.Models;

namespace FredagsCafeUWP
{
    class Sale
    {
        private ObservableCollection<Receipt> _receipts;
        private Stock stock = new Stock();
        private Product _selectedProduct;
        public Sale()
        {
            Receipts = new ObservableCollection<Receipt>()
            {
                new Receipt(424, "no note"),
                new Receipt(3423, "Drugs and drugs")
            };
        }

        public ObservableCollection<Receipt> Receipts
        {
            get { return _receipts; }
            set { _receipts = value; }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public void AddOneFromToBeSold()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.AmountToBeSold++;
            }
        }

        public void RemoveOneFromToBeSold()
        {
            if (SelectedProduct != null && SelectedProduct.AmountToBeSold > 0) SelectedProduct.AmountToBeSold--;
        }
    }
}

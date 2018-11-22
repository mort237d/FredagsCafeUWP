using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    class UserViewModel
    {
        private string _selectedItem;
        private Stock stock = new Stock();
        private Product product;

        private RelayCommand addProductCommand;
        private RelayCommand removeProductCommand;

        private RelayCommand addAmountCommand;

        public UserViewModel()
        {
            AddProductCommand = new RelayCommand(stock.AddProductToOBList);
            RemoveProductCommand = new RelayCommand(stock.RemoveProductFromOBList);

            AddAmountCommand = new RelayCommand(stock.AddAmountToProduct);
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public RelayCommand AddProductCommand
        {
            get { return addProductCommand; }
            set { addProductCommand = value; }
        }

        public RelayCommand RemoveProductCommand
        {
            get { return removeProductCommand; }
            set { removeProductCommand = value; }
        }

        public RelayCommand AddAmountCommand
        {
            get { return addAmountCommand; }
            set { addAmountCommand = value; }
        }
    }
}

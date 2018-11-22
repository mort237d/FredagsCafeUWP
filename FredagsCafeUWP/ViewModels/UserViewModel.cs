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
        private RelayCommand removeAmountCommand;
        private RelayCommand browseImageCommand;

        private RelayCommand changeProductPriceCommand;
        public UserViewModel()
        {
            AddProductCommand = new RelayCommand(stock.AddProductToOBList);
            RemoveProductCommand = new RelayCommand(stock.RemoveProductFromOBList);

            AddAmountCommand = new RelayCommand(stock.AddAmountToProduct);
            RemoveAmountCommand = new RelayCommand(stock.RemoveAmountFromProduct);
            BrowseImageCommand = new RelayCommand(stock.BrowseImageButton);
            ChangeProductPriceCommand = new RelayCommand(stock.ChangeProductPrice);
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

        #region RelayCommands
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

        public RelayCommand RemoveAmountCommand
        {
            get { return removeAmountCommand; }
            set { removeAmountCommand = value; }
        }

        public RelayCommand BrowseImageCommand
        {
            get { return browseImageCommand; }
            set { browseImageCommand = value; }
        }

        public RelayCommand ChangeProductPriceCommand
        {
            get { return changeProductPriceCommand; }
            set { changeProductPriceCommand = value; }
        }
        #endregion
    }
}

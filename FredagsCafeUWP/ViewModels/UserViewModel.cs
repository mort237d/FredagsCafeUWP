using System.Collections.Generic;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    class UserViewModel
    {
        private string _selectedItem;
        private Stock stock = new Stock();
        private User user;
        private Product product;
        private Sale sale = new Sale();
        private Administration administration = new Administration();

        private RelayCommand addProductCommand;
        private RelayCommand removeProductCommand;

        private RelayCommand addAmountCommand;
        private RelayCommand removeAmountCommand;
        private RelayCommand browseImageCommand;

        private RelayCommand changeProductSellPriceCommand;
        private RelayCommand changeProductBuyPriceCommand;

        private RelayCommand addUserCommand;
        private RelayCommand removeUserCommand;

        private RelayCommand compelteSaleCommand;

        public UserViewModel()
        {
            AddProductCommand = new RelayCommand(stock.AddProductToOBListAsync);
            RemoveProductCommand = new RelayCommand(stock.RemoveProductFromOBList);

            AddAmountCommand = new RelayCommand(stock.AddAmountToProduct);
            RemoveAmountCommand = new RelayCommand(stock.RemoveAmountFromProduct);

            BrowseImageCommand = new RelayCommand(stock.BrowseImageButton);

            ChangeProductSellPriceCommand = new RelayCommand(stock.ChangeProductSellPrice);
            ChangeProductBuyPriceCommand = new RelayCommand(stock.ChangeProductBuyPrice);

            AddUserCommand = new RelayCommand(administration.AddUser);
            RemoveUserCommand = new RelayCommand(administration.RemoveUser);

            CompelteSaleCommand = new RelayCommand(Sale.CompleteSale);
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

        public Administration Administration
        {
            get { return administration; }
            set { administration = value; }
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

        public RelayCommand ChangeProductSellPriceCommand
        {
            get { return changeProductSellPriceCommand; }
            set { changeProductSellPriceCommand = value; }
        }

        public RelayCommand ChangeProductBuyPriceCommand
        {
            get { return changeProductBuyPriceCommand; }
            set { changeProductBuyPriceCommand = value; }
        }

        public RelayCommand AddUserCommand
        {
            get { return addUserCommand; }
            set { addUserCommand = value; }
        }

        public RelayCommand RemoveUserCommand
        {
            get { return removeUserCommand; }
            set { removeUserCommand = value; }
        }

        public Sale Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public RelayCommand CompelteSaleCommand
        {
            get { return compelteSaleCommand; }
            set { compelteSaleCommand = value; }
        }

        #endregion
    }
}

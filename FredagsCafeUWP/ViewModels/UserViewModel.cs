using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Field

        private string _selectedItem;
        private Stock _stock;
        private User _user;
        private Product _product;
        private Sale _sale = new Sale();
        private Administration _administration = new Administration();
        private Statistics _statistics;
        private RelayCommand _addProductCommand;
        private RelayCommand _removeProductCommand;


        private RelayCommand _addAmountCommand;
        private RelayCommand _removeAmountCommand;
        private RelayCommand _browseImageCommand;

        private RelayCommand _changeProductSellPriceCommand;
        private RelayCommand _changeProductBuyPriceCommand;

        private RelayCommand _addUserCommand;
        private RelayCommand _removeUserCommand;

        private RelayCommand _saveCommand;
        private RelayCommand _loadCommand;

        private RelayCommand _completeSaleCommand;

        private RelayCommand _addOneToSaleCommand;
        private RelayCommand _removeeOneFromSaleCommand;
        private StatListClass _statList = new StatListClass();

        #endregion

        

        public UserViewModel()
        {
            _stock = new Stock(this);
            

            

            AddProductCommand = new RelayCommand(_stock.AddProductToObListAsync);
            RemoveProductCommand = new RelayCommand(_stock.RemoveProductFromObList);

            AddAmountCommand = new RelayCommand(_stock.AddAmountToProduct);
            RemoveAmountCommand = new RelayCommand(_stock.RemoveAmountFromProduct);

            BrowseImageCommand = new RelayCommand(_stock.BrowseImageButton);

            ChangeProductSellPriceCommand = new RelayCommand(_stock.ChangeProductSellPrice);
            ChangeProductBuyPriceCommand = new RelayCommand(_stock.ChangeProductBuyPrice);

            AddUserCommand = new RelayCommand(_administration.AddUser);
            RemoveUserCommand = new RelayCommand(_administration.RemoveUser);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);


            AddOneToSaleCommand = new RelayCommand(_sale.AddOneFromToBeSold);
            RemoveeOneFromSaleCommand = new RelayCommand(_sale.RemoveOneFromToBeSold);
        }

        #region Props

        public string SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public Stock Stock
        {
            get => _stock;
            set => _stock = value;
        }

        public Product Product
        {
            get => _product;
            set => _product = value;
        }


        public Administration Administration
        {
            get => _administration;
            set => _administration = value;
        }

        public Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }


        #region RelayCommands
        public RelayCommand AddProductCommand
        {
            get => _addProductCommand;
            set => _addProductCommand = value;
        }

        public RelayCommand RemoveProductCommand
        {
            get => _removeProductCommand;
            set => _removeProductCommand = value;
        }

        public RelayCommand AddAmountCommand
        {
            get => _addAmountCommand;
            set => _addAmountCommand = value;
        }

        public RelayCommand RemoveAmountCommand
        {
            get => _removeAmountCommand;
            set => _removeAmountCommand = value;
        }

        public RelayCommand BrowseImageCommand
        {
            get => _browseImageCommand;
            set => _browseImageCommand = value;
        }

        public RelayCommand ChangeProductSellPriceCommand
        {
            get => _changeProductSellPriceCommand;
            set => _changeProductSellPriceCommand = value;
        }

        public RelayCommand ChangeProductBuyPriceCommand
        {
            get => _changeProductBuyPriceCommand;
            set => _changeProductBuyPriceCommand = value;
        }

        public RelayCommand AddUserCommand
        {
            get => _addUserCommand;
            set => _addUserCommand = value;
        }

        public RelayCommand RemoveUserCommand
        {
            get => _removeUserCommand;
            set => _removeUserCommand = value;
        }

        public Sale Sale
        {
            get => _sale;
            set => _sale = value;
        }

        public RelayCommand CompleteSaleCommand
        {
            get => _completeSaleCommand;
            set => _completeSaleCommand = value;
        }

        public RelayCommand SaveCommand
        {
            get => _saveCommand;
            set => _saveCommand = value;
        }

        public RelayCommand LoadCommand
        {
            get => _loadCommand;
            set => _loadCommand = value;
        }

        public RelayCommand AddOneToSaleCommand
        {
            get { return _addOneToSaleCommand; }
            set { _addOneToSaleCommand = value; }
        }

        public RelayCommand RemoveeOneFromSaleCommand
        {
            get { return _removeeOneFromSaleCommand; }
            set { _removeeOneFromSaleCommand = value; }
        }

        public StatListClass StatList
        {
            get { return _statList; }
            set { _statList = value; }
        }

        #endregion

        #endregion

        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

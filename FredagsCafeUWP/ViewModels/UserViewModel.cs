﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.AddProduct;
using FredagsCafeUWP.Models.AddProduct_ChangeProduct;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Field
        private string _selectedItem;

        private Stock _stock = Stock.Instance;
        private Sale _sale = Sale.Instance;
        private EventPage _eventPage = EventPage.Instance;
        private Administration _administration = Administration.Instance;
        private StatListClass _statList = StatListClass.Instance;
        private LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
        private User _user;
        private Product _product;
        private Statistics _statistics;
        private AccountSettingsClass _accountSettingsClass = new AccountSettingsClass();
        private Help _help = new Help();
        private AddProductClass _addProductClass = new AddProductClass();
        private ChangeProductClass _changeProductClass = new ChangeProductClass();

        private RelayCommand _removeProductCommand;

        private RelayCommand _addAmountCommand;
        private RelayCommand _removeAmountCommand;

        private RelayCommand _changeProductSellPriceCommand;
        private RelayCommand _changeProductBuyPriceCommand;

        private RelayCommand _addUserCommand;
        private RelayCommand _removeUserCommand;

        private RelayCommand _saveCommand;
        private RelayCommand _loadCommand;

        private RelayCommand _completeSaleCommand;

        private RelayCommand _addOneToSaleCommand;
        private RelayCommand _removeOneFromSaleCommand;

        private RelayCommand _addEventUserCommand;
        private RelayCommand _removeEventUserCommand;

        private RelayCommand _addEventCommand;
        private RelayCommand _removeEventCommand;

        private RelayCommand _calculateTotalPriceCommand;

        private RelayCommand _logOffCommand;

        private RelayCommand _browseImageCommand;

        private RelayCommand _goToHelpPageCommand;
        private RelayCommand _showAddProductPopUpCommand;
        private RelayCommand _goToChangeProductPageCommand;

        private RelayCommand _addProductCommand;

        //Todo Skal slettes igen senere
        private RelayCommand _clearStatListCommand;

        private RelayCommand _changeToAccountCommand;

        private RelayCommand _deleteReceiptCommand;

        #endregion

        public UserViewModel()
        {
            RemoveProductCommand = new RelayCommand(Stock.RemoveProductFromObList);

            AddAmountCommand = new RelayCommand(Stock.AddAmountToProduct);
            RemoveAmountCommand = new RelayCommand(Stock.RemoveAmountFromProduct);

            ChangeProductSellPriceCommand = new RelayCommand(Stock.ChangeProductSellPrice);
            ChangeProductBuyPriceCommand = new RelayCommand(Stock.ChangeProductBuyPrice);

            AddUserCommand = new RelayCommand(Administration.AddUser);
            RemoveUserCommand = new RelayCommand(Administration.RemoveUser);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);

            AddOneToSaleCommand = new RelayCommand(Sale.AddProductButton);
            RemoveOneFromSaleCommand = new RelayCommand(Sale.RemoveProductButton);

            AddEventUserCommand = new RelayCommand(EventPage.AddUser);
            RemoveEventUserCommand = new RelayCommand(EventPage.RemoveUser);

            AddEventCommand = new RelayCommand(EventPage.AddEvent);
            RemoveEventCommand = new RelayCommand(EventPage.RemoveEvent);

            CalculateTotalPriceCommand = new RelayCommand(Sale.DiscountedTotalcalculator);

            LogOffCommand = new RelayCommand(LogOnLogOff.logOffMethod);

            ClearStatListCommand = new RelayCommand(StatList.ClearStats);

            ChangeToAccountCommand = new RelayCommand(AccountSettingsClass.GoToAccountSettings);

            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);

            ShowAddProductPopUpCommand = new RelayCommand(Stock.ShowAddProductPopUpMethod);
            GoToChangeProductPageCommand = new RelayCommand(ChangeProductClass.GoToChangeProductPage);

            DeleteReceiptCommand = new RelayCommand(Sale.DeleteReceipt);

            BrowseImageCommand = new RelayCommand(Stock.BrowseImageButton);

            AddProductCommand = new RelayCommand(Stock.AddProductToObList);
        }

        #region Props
        public RelayCommand BrowseImageCommand
        {
            get { return _browseImageCommand; }
            set { _browseImageCommand = value; }
        }

        public RelayCommand RemoveProductCommand
        {
            get { return _removeProductCommand; }
            set { _removeProductCommand = value; }
        }

        public string SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
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

        public RelayCommand RemoveOneFromSaleCommand
        {
            get { return _removeOneFromSaleCommand; }
            set { _removeOneFromSaleCommand = value; }
        }

        public RelayCommand RemoveEventUserCommand
        {
            get { return _removeEventUserCommand; }
            set { _removeEventUserCommand = value; }
        }

        public RelayCommand AddEventUserCommand
        {
            get { return _addEventUserCommand; }
            set { _addEventUserCommand = value; }
        }

        public RelayCommand AddEventCommand
        {
            get { return _addEventCommand; }
            set { _addEventCommand = value; }
        }

        public RelayCommand RemoveEventCommand
        {
            get { return _removeEventCommand; }
            set { _removeEventCommand = value; }
        }

        public StatListClass StatList
        {
            get { return _statList; }
            set { _statList = value; }
        }

        public RelayCommand CalculateTotalPriceCommand
        {
            get { return _calculateTotalPriceCommand; }
            set { _calculateTotalPriceCommand = value; }
        }

        public RelayCommand LogOffCommand
        {
            get { return _logOffCommand; }
            set { _logOffCommand = value; }
        }

        public LogOnLogOff LogOnLogOff
        {
            get { return _logOnLogOff; }
            set { _logOnLogOff = value; }
        }

        //ToDo skal slettes igen senere
        public RelayCommand ClearStatListCommand
        {
            get { return _clearStatListCommand; }
            set { _clearStatListCommand = value; }
        }

        public AccountSettingsClass AccountSettingsClass
        {
            get { return _accountSettingsClass; }
            set { _accountSettingsClass = value; }
        }

        public RelayCommand ChangeToAccountCommand
        {
            get { return _changeToAccountCommand; }
            set { _changeToAccountCommand = value; }
        }

        public Help Help
        {
            get { return _help; }
            set { _help = value; }
        }

        public RelayCommand GoToHelpPageCommand
        {
            get { return _goToHelpPageCommand; }
            set { _goToHelpPageCommand = value; }
        }

        public RelayCommand ShowAddProductPopUpCommand
        {
            get { return _showAddProductPopUpCommand; }
            set { _showAddProductPopUpCommand = value; }
        }

        public RelayCommand DeleteReceiptCommand
        {
            get { return _deleteReceiptCommand; }
            set { _deleteReceiptCommand = value; }
        }

        public AddProductClass AddProductClass
        {
            get { return _addProductClass; }
            set { _addProductClass = value; }
        }

        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public EventPage EventPage
        {
            get { return _eventPage; }
            set { _eventPage = value; }
        }

        public ChangeProductClass ChangeProductClass
        {
            get { return _changeProductClass; }
            set { _changeProductClass = value; }
        }

        public RelayCommand GoToChangeProductPageCommand
        {
            get { return _goToChangeProductPageCommand; }
            set { _goToChangeProductPageCommand = value; }
        }

        public RelayCommand AddProductCommand
        {
            get { return _addProductCommand; }
            set { _addProductCommand = value; }
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

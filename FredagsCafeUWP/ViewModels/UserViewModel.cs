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
        private Stock _stock = new Stock();
        private User _user;
        private Product _product;
        private Sale _sale = new Sale();
        private EventPage _eventPage = new EventPage();
        private Administration _administration = new Administration();
        private Statistics _statistics;
        private StatListClass _statList = new StatListClass();
        private  LogOnLogOff _logOnLogOff = new LogOnLogOff();

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
        private RelayCommand _removeOneFromSaleCommand;

        private RelayCommand _addEventUserCommand;
        private RelayCommand _removeEventUserCommand;

        private RelayCommand _addEventCommand;
        private RelayCommand _removeEventCommand;

        private RelayCommand _calculateTotalPriceCommand;

        private RelayCommand _logOffCommand;

        //Todo Skal slettes igen senere
        private RelayCommand _clearStatListCommand;

        #endregion

        public UserViewModel()
        {
            AddProductCommand = new RelayCommand(_stock.AddProductToObListAsync);
            RemoveProductCommand = new RelayCommand(_stock.RemoveProductFromObList);

            AddAmountCommand = new RelayCommand(Stock.AddAmountToProduct);
            RemoveAmountCommand = new RelayCommand(Stock.RemoveAmountFromProduct);

            BrowseImageCommand = new RelayCommand(Stock.BrowseImageButton);

            ChangeProductSellPriceCommand = new RelayCommand(Stock.ChangeProductSellPrice);
            ChangeProductBuyPriceCommand = new RelayCommand(Stock.ChangeProductBuyPrice);

            AddUserCommand = new RelayCommand(Administration.AddUser);
            RemoveUserCommand = new RelayCommand(Administration.RemoveUser);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);

            AddOneToSaleCommand = new RelayCommand(Sale.AddOneFromToBeSold);
            RemoveOneFromSaleCommand = new RelayCommand(Sale.RemoveOneFromToBeSold);

            AddEventUserCommand = new RelayCommand(EventPage.AddUser);
            RemoveEventUserCommand = new RelayCommand(EventPage.RemoveUser);

            AddEventCommand = new RelayCommand(EventPage.AddEvent);
            RemoveEventCommand = new RelayCommand(EventPage.RemoveEvent);

            CalculateTotalPriceCommand = new RelayCommand(Sale.TotalTbMethod);

            LogOffCommand = new RelayCommand(LogOnLogOff.logOffMethod);

            ClearStatListCommand = new RelayCommand(StatList.ClearStats);
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

        public RelayCommand RemoveOneFromSaleCommand
        {
            get { return _removeOneFromSaleCommand; }
            set { _removeOneFromSaleCommand = value; }
        }

        public EventPage EventPage
        {
            get { return _eventPage; }
            set { _eventPage = value; }
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

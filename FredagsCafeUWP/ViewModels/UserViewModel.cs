using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.UserPage;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Field

        private Stock _stock = Stock.Instance;
        private Sale _sale = Sale.Instance;
        private EventPage _eventPage = EventPage.Instance;
        private Administration _administration = Administration.Instance;
        private StatListClass _statList = StatListClass.Instance;
        private LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
        private Product _product;
        private Statistics _statistics;
        private AccountSettingsClass _accountSettingsClass = new AccountSettingsClass();
        private Help _help = new Help();

        private RelayCommand _removeProductCommand;

        private RelayCommand _showAddUserPopUpCommand;
        private RelayCommand _addUserCommand;
        private RelayCommand _removeUserCommand;

        private RelayCommand _saveCommand;
        private RelayCommand _loadCommand;

        private RelayCommand _completeSaleCommand;

        private RelayCommand _addOneToSaleCommand;
        private RelayCommand _removeOneFromSaleCommand;

        private RelayCommand _showAddEventUserPopUpCommand;
        private RelayCommand _addEventUserCommand;
        private RelayCommand _removeEventUserCommand;
        
        private RelayCommand _showAddEventPopUpCommand;
        private RelayCommand _addEventCommand;
        private RelayCommand _removeEventCommand;

        private RelayCommand _calculateTotalPriceCommand;

        private RelayCommand _logOffCommand;

        private RelayCommand _browseAddImageCommand;
        private RelayCommand _browseChangeImageCommand;

        private RelayCommand _goToHelpPageCommand;
        private RelayCommand _showAddProductPopUpCommand;
        private RelayCommand _showChangeProductPopUpCommand;

        private RelayCommand _addProductCommand;
        private RelayCommand _changeProductCommand;

        private RelayCommand _changeSettingsCommand;

        private RelayCommand _showAccountPopUp;

        private RelayCommand _deleteReceiptCommand;

        private RelayCommand _userImageBrowserCommand;

        private RelayCommand _resetReceiptsCommand;

        private RelayCommand _changeAdminCommand;

        #endregion

        public UserViewModel()
        {
            RemoveProductCommand = new RelayCommand(Stock.RemoveProductFromObList);

            ShowAddUserPopUpCommand = new RelayCommand(Administration.ShowAddUserPopUpMethod);
            AddUserCommand = new RelayCommand(Administration.AddUser);
            RemoveUserCommand = new RelayCommand(Administration.RemoveUser);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);

            AddOneToSaleCommand = new RelayCommand(Sale.AddProductButton);
            RemoveOneFromSaleCommand = new RelayCommand(Sale.RemoveProductButton);

            ShowAddEventUserPopUpCommand = new RelayCommand(EventPage.ShowAddEventUserPopUpMethod);
            AddEventUserCommand = new RelayCommand(EventPage.AddUser);
            RemoveEventUserCommand = new RelayCommand(EventPage.RemoveUser);

            ShowAddEventPopUpCommand = new RelayCommand(EventPage.ShowAddEventPopUpMethod);
            AddEventCommand = new RelayCommand(EventPage.AddEvent);
            RemoveEventCommand = new RelayCommand(EventPage.RemoveEvent);

            CalculateTotalPriceCommand = new RelayCommand(Sale.DiscountedTotalcalculator);

            LogOffCommand = new RelayCommand(LogOnLogOff.logOffMethod);


            ShowAccountPopUp = new RelayCommand(AccountSettingsClass.ShowAccountSettingsPopUpMethod);

            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);

            ShowAddProductPopUpCommand = new RelayCommand(Stock.ShowAddProductPopUpMethod);
            ShowChangeProductPopUpCommand = new RelayCommand(Stock.ShowChangeProductPopUpMethod);

            DeleteReceiptCommand = new RelayCommand(Sale.DeleteReceipt);

            UserImageBrowserCommand = new RelayCommand(Administration.BrowseImageButton);

            BrowseAddImageCommand = new RelayCommand(Stock.BrowseAddImageButton);
            BrowseChangeImageCommand = new RelayCommand(Stock.BrowseChangeImageButton);

            AddProductCommand = new RelayCommand(Stock.AddProductToObList);
            ChangeProductCommand = new RelayCommand(Stock.ChangeProductOfObList);
            
            ResetReceiptsCommand = new RelayCommand(Sale.ResetReceipt);

            ChangeSettingsCommand = new RelayCommand(AccountSettingsClass.ChangeSettings);

            ChangeAdminCommand = new RelayCommand(Administration.ChangeAdmin);
        }

        #region Props
        public RelayCommand BrowseAddImageCommand
        {
            get { return _browseAddImageCommand; }
            set { _browseAddImageCommand = value; }
        }

        public RelayCommand BrowseChangeImageCommand
        {
            get { return _browseChangeImageCommand; }
            set { _browseChangeImageCommand = value; }
        }

        public RelayCommand RemoveProductCommand
        {
            get { return _removeProductCommand; }
            set { _removeProductCommand = value; }
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

        public AccountSettingsClass AccountSettingsClass
        {
            get { return _accountSettingsClass; }
            set { _accountSettingsClass = value; }
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

        public RelayCommand UserImageBrowserCommand
        {
            get { return _userImageBrowserCommand; }
            set => _userImageBrowserCommand = value; 
        }

        public RelayCommand ShowChangeProductPopUpCommand
        {
            get { return _showChangeProductPopUpCommand; }
            set { _showChangeProductPopUpCommand = value; }
        }

        public RelayCommand AddProductCommand
        {
            get { return _addProductCommand; }
            set { _addProductCommand = value; }
        }

        public RelayCommand ChangeProductCommand
        {
            get { return _changeProductCommand; }
            set { _changeProductCommand = value; }
        }

        public RelayCommand ShowAddEventPopUpCommand
        {
            get { return _showAddEventPopUpCommand; }
            set { _showAddEventPopUpCommand = value; }
        }
        public RelayCommand ResetReceiptsCommand
        {
            get { return _resetReceiptsCommand; }
            set { _resetReceiptsCommand = value; }
        }

        public RelayCommand ShowAddEventUserPopUpCommand
        {
            get { return _showAddEventUserPopUpCommand; }
            set { _showAddEventUserPopUpCommand = value; }
        }

        public RelayCommand ShowAddUserPopUpCommand
        {
            get { return _showAddUserPopUpCommand; }
            set { _showAddUserPopUpCommand = value; }
        }

        public RelayCommand ShowAccountPopUp
        {
            get { return _showAccountPopUp; }
            set { _showAccountPopUp = value; }
        }

        public RelayCommand ChangeSettingsCommand
        {
            get { return _changeSettingsCommand; }
            set { _changeSettingsCommand = value; }
        }

        public RelayCommand ChangeAdminCommand
        {
            get { return _changeAdminCommand; }
            set { _changeAdminCommand = value; }
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

using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class UserViewModel
    {
        #region Field
        private StockAdministrator _stockAdministrator = StockAdministrator.Instance;
        private SaleAdministrator _sale = SaleAdministrator.Instance;
        private EventAdministrator _eventAdministrator = EventAdministrator.Instance;
        private UserAdministrator _userAdministrator = UserAdministrator.Instance;
        private StatisticsAdministrator _statList = StatisticsAdministrator.Instance;
        private LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
        private Product _product;
        private Statistics _statistics;
        private AccountSettings _accountSettings = new AccountSettings();
        private Help _help = new Help();
        private SortProducts _sortProducts = new SortProducts();
        #endregion

        #region Props
        public Product Product
        {
            get => _product;
            set => _product = value;
        }

        public UserAdministrator UserAdministrator
        {
            get => _userAdministrator;
            set => _userAdministrator = value;
        }

        public Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        public SaleAdministrator Sale
        {
            get => _sale;
            set => _sale = value;
        }

        public StatisticsAdministrator StatList
        {
            get { return _statList; }
            set { _statList = value; }
        }

        public LogOnLogOff LogOnLogOff
        {
            get { return _logOnLogOff; }
            set { _logOnLogOff = value; }
        }

        public AccountSettings AccountSettings
        {
            get { return _accountSettings; }
            set { _accountSettings = value; }
        }

        public Help Help
        {
            get { return _help; }
            set { _help = value; }
        }

        public StockAdministrator StockAdministrator
        {
            get { return _stockAdministrator; }
            set { _stockAdministrator = value; }
        }

        public EventAdministrator EventAdministrator
        {
            get { return _eventAdministrator; }
            set { _eventAdministrator = value; }
        }

        public SortProducts SortProducts
        {
            get { return _sortProducts; }
            set { _sortProducts = value; }
        }

        #endregion

        #region RelayProps

        public RelayCommand RemoveProductCommand { get; set; }
        public RelayCommand ShowAddUserPopUpCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand RemoveUserCommand { get; set; }
        public RelayCommand ShowLogPopUp { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand CompleteSaleCommand { get; set; }
        public RelayCommand AddOneToSaleCommand { get; set; }
        public RelayCommand RemoveOneFromSaleCommand { get; set; }
        public RelayCommand ShowAddEventUserPopUpCommand { get; set; }
        public RelayCommand AddEventUserCommand { get; set; }
        public RelayCommand RemoveEventUserCommand { get; set; }
        public RelayCommand ShowAddEventPopUpCommand { get; set; }
        public RelayCommand AddEventCommand { get; set; }
        public RelayCommand RemoveEventCommand { get; set; }
        public RelayCommand CalculateTotalPriceCommand { get; set; }
        public RelayCommand LogOffCommand { get; set; }
        public RelayCommand BrowseAddImageCommand { get; set; }
        public RelayCommand BrowseChangeImageCommand { get; set; }
        public RelayCommand GoToHelpPageCommand { get; set; }
        public RelayCommand ShowAddProductPopUpCommand { get; set; }
        public RelayCommand ShowChangeProductPopUpCommand { get; set; }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand ChangeProductCommand { get; set; }
        public RelayCommand ChangeSettingsCommand { get; set; }
        public RelayCommand ShowAccountPopUp { get; set; }
        public RelayCommand ShowAdminAccountPopUp { get; set; }
        public RelayCommand DeleteReceiptCommand { get; set; }
        public RelayCommand UserImageBrowserCommand { get; set; }
        public RelayCommand EventImageBrowserCommand { get; set; }
        public RelayCommand ChangeAdminCommand { get; set; }
        public RelayCommand UserAdminChange { get; set; }
        public RelayCommand ChangeSelectedAccountSettings { get; set; }

        #endregion

        public UserViewModel()
        {
            RemoveProductCommand = new RelayCommand(StockAdministrator.RemoveProductFromObList);
            ShowAddProductPopUpCommand = new RelayCommand(StockAdministrator.ShowAddProductPopUpMethod);
            ShowChangeProductPopUpCommand = new RelayCommand(StockAdministrator.ShowChangeProductPopUpMethod);
            BrowseAddImageCommand = new RelayCommand(StockAdministrator.BrowseAddImageButton);
            BrowseChangeImageCommand = new RelayCommand(StockAdministrator.BrowseChangeImageButton);
            AddProductCommand = new RelayCommand(StockAdministrator.AddProductToObList);
            ChangeProductCommand = new RelayCommand(StockAdministrator.ChangeProductOfObList);

            ShowAddUserPopUpCommand = new RelayCommand(UserAdministrator.ShowAddUserPopUpMethod);
            AddUserCommand = new RelayCommand(UserAdministrator.AddUser);
            RemoveUserCommand = new RelayCommand(UserAdministrator.RemoveUser);
            UserImageBrowserCommand = new RelayCommand(UserAdministrator.BrowseImageButton);
            ChangeAdminCommand = new RelayCommand(UserAdministrator.ChangeAdmin);
            ShowLogPopUp = new RelayCommand(UserAdministrator.ShowLogPopUpMethod);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);
            CalculateTotalPriceCommand = new RelayCommand(Sale.DiscountedTotalcalculator);
            AddOneToSaleCommand = new RelayCommand(Sale.AddProductButton);
            RemoveOneFromSaleCommand = new RelayCommand(Sale.RemoveProductButton);
            DeleteReceiptCommand = new RelayCommand(Sale.DeleteReceipt);

            ShowAddEventUserPopUpCommand = new RelayCommand(EventAdministrator.ShowAddEventUserPopUpMethod);
            AddEventUserCommand = new RelayCommand(EventAdministrator.AddUser);
            RemoveEventUserCommand = new RelayCommand(EventAdministrator.RemoveUser);
            ShowAddEventPopUpCommand = new RelayCommand(EventAdministrator.ShowAddEventPopUpMethod);
            AddEventCommand = new RelayCommand(EventAdministrator.AddEvent);
            RemoveEventCommand = new RelayCommand(EventAdministrator.RemoveEvent);
            EventImageBrowserCommand = new RelayCommand(EventAdministrator.BrowseAddImageButton);
            
            LogOffCommand = new RelayCommand(LogOnLogOff.LogOffMethod);

            ShowAccountPopUp = new RelayCommand(AccountSettings.ShowAccountSettingsPopUpMethod);
            ShowAdminAccountPopUp = new RelayCommand(AccountSettings.ShowAdminAccountPopUpMethod);
            ChangeSettingsCommand = new RelayCommand(AccountSettings.ChangeSettings);
            ChangeSelectedAccountSettings = new RelayCommand(AccountSettings.ChangeSelectedAccountSettingsMethod);

            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);
        }
    }
}

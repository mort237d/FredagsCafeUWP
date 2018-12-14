using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.UserPage;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class UserViewModel
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
        private SortProducts _sortProducts = new SortProducts();
        #endregion

        #region Props
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

        public Sale Sale
        {
            get => _sale;
            set => _sale = value;
        }

        public StatListClass StatList
        {
            get { return _statList; }
            set { _statList = value; }
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
            RemoveProductCommand = new RelayCommand(Stock.RemoveProductFromObList);
            ShowAddProductPopUpCommand = new RelayCommand(Stock.ShowAddProductPopUpMethod);
            ShowChangeProductPopUpCommand = new RelayCommand(Stock.ShowChangeProductPopUpMethod);
            BrowseAddImageCommand = new RelayCommand(Stock.BrowseAddImageButton);
            BrowseChangeImageCommand = new RelayCommand(Stock.BrowseChangeImageButton);
            AddProductCommand = new RelayCommand(Stock.AddProductToObList);
            ChangeProductCommand = new RelayCommand(Stock.ChangeProductOfObList);

            ShowAddUserPopUpCommand = new RelayCommand(Administration.ShowAddUserPopUpMethod);
            AddUserCommand = new RelayCommand(Administration.AddUser);
            RemoveUserCommand = new RelayCommand(Administration.RemoveUser);
            UserImageBrowserCommand = new RelayCommand(Administration.BrowseImageButton);
            ChangeAdminCommand = new RelayCommand(Administration.ChangeAdmin);

            CompleteSaleCommand = new RelayCommand(Sale.CompleteSale);
            CalculateTotalPriceCommand = new RelayCommand(Sale.DiscountedTotalcalculator);
            AddOneToSaleCommand = new RelayCommand(Sale.AddProductButton);
            RemoveOneFromSaleCommand = new RelayCommand(Sale.RemoveProductButton);
            DeleteReceiptCommand = new RelayCommand(Sale.DeleteReceipt);

            ShowAddEventUserPopUpCommand = new RelayCommand(EventPage.ShowAddEventUserPopUpMethod);
            AddEventUserCommand = new RelayCommand(EventPage.AddUser);
            RemoveEventUserCommand = new RelayCommand(EventPage.RemoveUser);
            ShowAddEventPopUpCommand = new RelayCommand(EventPage.ShowAddEventPopUpMethod);
            AddEventCommand = new RelayCommand(EventPage.AddEvent);
            RemoveEventCommand = new RelayCommand(EventPage.RemoveEvent);
            EventImageBrowserCommand = new RelayCommand(EventPage.BrowseAddImageButton);
            
            LogOffCommand = new RelayCommand(LogOnLogOff.LogOffMethod);

            ShowAccountPopUp = new RelayCommand(AccountSettingsClass.ShowAccountSettingsPopUpMethod);
            ShowAdminAccountPopUp = new RelayCommand(AccountSettingsClass.ShowAdminAccountPopUpMethod);
            ChangeSettingsCommand = new RelayCommand(AccountSettingsClass.ChangeSettings);
            ChangeSelectedAccountSettings = new RelayCommand(AccountSettingsClass.ChangeSelectedAccountSettingsMethod);

            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);
        }
    }
}

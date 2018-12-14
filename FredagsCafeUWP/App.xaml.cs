using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FredagsCafeUWP.Models;
using FredagsCafeUWP.Models.UserPage;

namespace FredagsCafeUWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private Encrypt _encrypt = new Encrypt();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.NotRunning || e.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
                {
                    //TODO: Load state from previously suspended application
                    try
                    {
                        Debug.WriteLine("loading user async...");
                        UserAdministrator.Instance.Users = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<User>>("userAdministrator.xml");
                        Debug.WriteLine("user.count:" + UserAdministrator.Instance.Users.Count);
                    }
                    catch (Exception)
                    {
                        UserAdministrator.Instance.Users = new ObservableCollection<User>()
                        {new User("Benjo", "", "", "Benjo@fev.fbtk.el", "", "Benjo", "Benjo", "VtfsJnbhft/Qspgjmf-jdpo.qoh", "Benjo")};
                    }
                    try
                    {
                        Debug.WriteLine("loading loginlogout async...");
                        LogOnLogOff.Instance.LogInLogOutList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<string>>("loginlogout.xml");
                        Debug.WriteLine("loginlogoutlist.count:" + LogOnLogOff.Instance.LogInLogOutList.Count);
                    }
                    catch (Exception)
                    {
                        LogOnLogOff.Instance.LogInLogOutList = new ObservableCollection<string>();
                    }
                    try
                    {
                        Debug.WriteLine("loading stats async...");
                        StatisticsAdministrator.Instance.StatList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Statistics>>("stats.xml");
                        Debug.WriteLine("stats.count:" + StatisticsAdministrator.Instance.StatList.Count);
                    }
                    catch (Exception)
                    {
                        StatisticsAdministrator.Instance.StatList = new ObservableCollection<Statistics>();
                    }
                    try
                    {
                        Debug.WriteLine("loading productStats async...");
                        StatisticsAdministrator.Instance.ProductGraphList = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("productStats.xml");
                        Debug.WriteLine("productStats.count:" + StatisticsAdministrator.Instance.ProductGraphList.Count);
                    }
                    catch (Exception)
                    {
                        StatisticsAdministrator.Instance.ProductGraphList = new ObservableCollection<Product>();
                    }
                    try
                    {
                        Debug.WriteLine("loading receipt async...");
                        SaleAdministrator.Instance.Receipts = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Receipt>>("receipt.xml");
                        Debug.WriteLine("receipts.count:" + SaleAdministrator.Instance.Receipts.Count);
                    }
                    catch (Exception)
                    {
                        SaleAdministrator.Instance.Receipts = new ObservableCollection<Receipt>();
                    }
                    try
                    {
                        Debug.WriteLine("loading list async...");
                        EventAdministrator.Instance.Events = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Event>>("events.xml");
                        Debug.WriteLine("events.count:" + EventAdministrator.Instance.Events.Count);
                    }
                    catch (Exception)
                    {
                        EventAdministrator.Instance.Events = new ObservableCollection<Event>();
                    }
                    try
                    {
                        Debug.WriteLine("loading product async...");
                        StockAdministrator.Instance.Products = await XmlReadWrite.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("stockAdministrator.xml");
                        Debug.WriteLine("products.count:" + StockAdministrator.Instance.Products.Count);
                    }
                    catch (Exception)
                    {
                        StockAdministrator.Instance.Products = new ObservableCollection<Product>();
                    }

                    _encrypt.DecryptUsers();

                    Debug.WriteLine("Starting: ");
                    foreach (var user in UserAdministrator.Instance.Users)
                    {
                        Debug.WriteLine(user.UserName);
                        Debug.WriteLine(user.ImageSource);
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(LoginPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity

            await XmlReadWrite.SaveAsync(UserAdministrator.Instance.Users, "userAdministrator");
            await XmlReadWrite.SaveAsync(StockAdministrator.Instance.Products, "stockAdministrator");
            await XmlReadWrite.SaveAsync(SaleAdministrator.Instance.Receipts, "receipt");
            await XmlReadWrite.SaveAsync(StatisticsAdministrator.Instance.StatList, "stats");
            await XmlReadWrite.SaveAsync(StatisticsAdministrator.Instance.ProductGraphList, "productStats");
            await XmlReadWrite.SaveAsync(LogOnLogOff.Instance.LogInLogOutList, "loginlogout");
            await XmlReadWrite.SaveAsync(EventAdministrator.Instance.Events, "events");

            _encrypt.EncryptUsers();

            foreach (var product in StockAdministrator.Instance.Products)
            {
                product.AmountToBeSold = 0;
            }

            Debug.WriteLine("Closing: ");
            foreach (var user in UserAdministrator.Instance.Users)
            {
                Debug.WriteLine(user.UserName);
            }

            deferral.Complete();
        }
    }
}

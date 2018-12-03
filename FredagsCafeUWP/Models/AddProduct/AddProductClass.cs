using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Views;

namespace FredagsCafeUWP.Models.AddProduct
{
    public class AddProductClass : INotifyPropertyChanged
    {
        private Message _message;

        private Stock _stock;

        private string _nameTb;
        private string _buyingPriceTb;
        private string _sellingPriceTb;
        private string _amountTb;
        private string _imageSourceTb = "";

        public string _colorRed = "Red";
        public string _colorGreen = "ForestGreen";

        public AddProductClass()
        {
            Stock = new Stock();
            _message = new Message(this);
        }

        public string NameTb
        {
            get => _nameTb;
            set
            {
                _nameTb = value;
                OnPropertyChanged();
            }
        }

        public string BuyingPriceTb
        {
            get => _buyingPriceTb;
            set
            {
                _buyingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string SellingPriceTb
        {
            get => _sellingPriceTb;
            set
            {
                _sellingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string AmountTb
        {
            get => _amountTb;
            set
            {
                _amountTb = value;
                OnPropertyChanged();
            }
        }

        public string ImageSourceTb
        {
            get => _imageSourceTb;
            set
            {
                _imageSourceTb = value;
                OnPropertyChanged();
            }
        }

        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public async void GoToAddProductPage()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(AddProductPage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        public async void AddProductToObList()
        {
            bool productExist = false;
            if (NameTb != null)
            {
                foreach (var element in Stock.Products)
                {
                    if (element.Name.ToLower().Equals(NameTb.ToLower()))
                    {
                        productExist = true;
                        break;
                    }
                }

                if (!productExist)
                {
                    if (double.TryParse(BuyingPriceTb, out double doubleBuyingPriceTb) &&
                    double.TryParse(SellingPriceTb, out double doubleSellingPriceTb) &&
                    int.TryParse(AmountTb, out int intAmountTb))
                    {
                        if (doubleBuyingPriceTb > 0 && doubleSellingPriceTb > 0 && AmountTb != null && intAmountTb >= 0)
                        {
                            if (string.IsNullOrEmpty(ImageSourceTb))
                            {
                                if (intAmountTb < Stock._minAmount) //TODO what is going on here?
                                    Stock.Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, "ProductImages/BlankDåse.png", _colorRed));
                                else
                                    Stock.Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, "ProductImages/BlankDåse.png", _colorGreen));
                            }
                            else
                            {
                                if (intAmountTb < Stock._minAmount)
                                    Stock.Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, ImageSourceTb, Stock.ColorRed));
                                else
                                    Stock.Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, ImageSourceTb, Stock.ColorGreen));
                            }

                            NameTb = null;
                            BuyingPriceTb = null;
                            SellingPriceTb = null;
                            AmountTb = null;
                            ImageSourceTb = null;

                            Debug.WriteLine("product: " + Stock.Products.Count);
                            //Stock.SaveAsync();

                            Window.Current.Close();
                        }
                        else await _message.Error("Forkert input", "Købspris og Salgspris skal være mere en 0.");
                    }
                    else await _message.Error("Forkert input", "Købspris, Salgspris og Antal skal være tal.");
                }
                else await _message.Error("Varen findes allerede!", "Scroll igennem varerne, for at finde den.");
            }
            else await _message.Error("Forkert input", "Produktet skal have et navn.");
        }

        public async Task<string> BrowseImageWindowTask()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            TextBlock outputTextBlock = new TextBlock();

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null) return outputTextBlock.Text = "ProductImages/" + file.Name;
            else return outputTextBlock.Text = "";
        }

        public async void BrowseImageButton()
        {
            ImageSourceTb = await BrowseImageWindowTask();
        }

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

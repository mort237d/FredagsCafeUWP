using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace FredagsCafeUWP.Models.AddProduct_ChangeProduct
{
    public class ChangeProductClass : INotifyPropertyChanged
    {
        private Stock _stock = Stock.Instance;
        private Message _message = Message.Instance;

        private string _nameTb;
        private string _buyingPriceTb;
        private string _sellingPriceTb;
        private string _amountTb;
        private string _imageSourceTb = "";

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

        public ChangeProductClass()
        {
            NameTb = _stock.SelectedProduct.Name;
            BuyingPriceTb = _stock.SelectedProduct.BuyingPrice.ToString();
            SellingPriceTb = _stock.SelectedProduct.SellingPrice.ToString();
            AmountTb = _stock.SelectedProduct.Amount.ToString();
            ImageSourceTb = _stock.SelectedProduct.ImageSource;
        }

        public async void GoToChangeProductPage()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(ChangeProductPage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        public void ChangeProductOfObList()
        {
            _stock.SelectedProduct.Name = NameTb;
            if (double.TryParse(BuyingPriceTb, out double doubleBuyingPriceTb)) _stock.SelectedProduct.BuyingPrice = doubleBuyingPriceTb;
            else _message.Error("Forkert input", "Købspris skal være et tal.");
            if (double.TryParse(SellingPriceTb, out double doubleSellingPriceTb)) _stock.SelectedProduct.SellingPrice = doubleSellingPriceTb;
            else _message.Error("Forkert input", "Salgspris skal være et tal.");
            if (int.TryParse(AmountTb, out int intAmountTb)) _stock.SelectedProduct.Amount = intAmountTb;
            else _message.Error("Forkert input", "Antal skal være et hel tal.");
            _stock.SelectedProduct.ImageSource = ImageSourceTb;
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

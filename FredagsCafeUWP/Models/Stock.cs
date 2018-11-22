using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class Stock : INotifyPropertyChanged
    {
        private Product product;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value; 
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value; 
                OnPropertyChanged();
            }
        }

        public string NameTB
        {
            get { return _nameTB; }
            set
            {
                _nameTB = value; 
                OnPropertyChanged();
            }
        }

        public string BuyingPriceTB
        {
            get { return _buyingPriceTB; }
            set
            {
                _buyingPriceTB = value;
                OnPropertyChanged();
            }
        }

        public string SellingPriceTB
        {
            get { return _sellingPriceTB; }
            set
            {
                _sellingPriceTB = value;
                OnPropertyChanged();
            }
        }

        public string AmountTB
        {
            get { return _amountTB; }
            set
            {
                _amountTB = value;
                OnPropertyChanged();
            }
        }

        public string ImageSourceTB
        {
            get { return _imageSourceTB; }
            set
            {
                _imageSourceTB = value;
                OnPropertyChanged();
            }
        }

        public string FrameAmountTB
        {
            get { return _frameAmountTB; }
            set
            {
                _frameAmountTB = value;
                OnPropertyChanged();
            }
        }

        public string FrameSizeTB
        {
            get { return _frameSizeTB; }
            set
            {
                _frameSizeTB = value;
                OnPropertyChanged();
            }
        }

        public string ProductAmountTB
        {
            get { return _productAmountTB; }
            set
            {
                _productAmountTB = value; 
                OnPropertyChanged();
            }
        }

        private string _nameTB;
        private string _buyingPriceTB;
        private string _sellingPriceTB;
        private string _amountTB;
        private string _imageSourceTB;

        //private int _frameAmountTB;
        //private int _frameSizeTB;

        //private int _productAmountTB;

        private string _frameAmountTB;
        private string _frameSizeTB;

        private string _productAmountTB;

        public Stock()
        {
            Products = new ObservableCollection<Product>()
            {
                new Product(66, 67, "Tuborg Classic", 22, 2, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Grøn Tuborg", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Tuborg Gylden Dame", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Carlsberg", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Mokai", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Breezer", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Sommersby Apple Cider", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Sommersby Pear Cider", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Cola", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Cola Zero", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                //new Product(55, 63, "Squash", 2, 13, "ProductImages/Tuborg-Dåse.png")
            };
        }

        public async void WriteListToTxt()
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync("StockTextDoc.txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);
            
            Windows.Storage.StorageFile sampleFile1 =
                await storageFolder.GetFileAsync("StockTextDoc.txt");

            foreach (var p in Products)
            {
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile1, p.ToString());
            }

        }

        public void AddProductToOBList()
        {
            bool productExist = false;
            foreach (var element in Products)
            {
                if (element.Name.ToLower().Equals(NameTB.ToLower()))
                {
                    productExist = true;
                    break;
                }
            }
            
            if (!productExist)
            {
                double.TryParse(BuyingPriceTB, out double doubleBuyingPriceTB);
                double.TryParse(SellingPriceTB, out double doubleSellingPriceTB);
                int.TryParse(AmountTB, out int intAmountTB);

                if (NameTB != null && doubleBuyingPriceTB != 0 && doubleSellingPriceTB != 0 && AmountTB != null)
                {
                    Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, "ProductImages/Tuborg-Dåse.png"));

                    NameTB = null;
                    BuyingPriceTB = null;
                    SellingPriceTB = null;
                    AmountTB = null;
                    ImageSourceTB = null;
                }
               
            }
            else Debug.WriteLine("Varen findes allerede");
        }

        public void RemoveProductFromOBList()
        {
            Products.Remove(SelectedProduct);
        }

        public void AddAmountToProduct()
        {
            Int32.TryParse(FrameAmountTB, out int intFrameAmountTB);
            Int32.TryParse(FrameSizeTB, out int intFrameSizeTB);
            Int32.TryParse(ProductAmountTB, out int intProductAmountTB);


            if (intFrameAmountTB != 0 && intFrameSizeTB != 0 && intProductAmountTB != 0)
            {
                SelectedProduct.Amount += (intFrameAmountTB * intFrameSizeTB) + intProductAmountTB;
                FrameAmountTB = null;
                FrameSizeTB = null;
                ProductAmountTB = null;
            }
            else if (intFrameAmountTB != 0 && intFrameSizeTB != 0)
            {
                SelectedProduct.Amount += intFrameAmountTB * intFrameSizeTB;
                FrameAmountTB = null;
                FrameSizeTB = null;
            }
            else if (intProductAmountTB != 0)
            {
                SelectedProduct.Amount += intProductAmountTB;
                ProductAmountTB = null;
            }
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
            if (file != null)
            {
                return outputTextBlock.Text = file.Name;
            }
            else
            {
                return outputTextBlock.Text = "Operation cancelled.";
            }

        }

        public async void BrowseImageButton()
        {
            ImageSourceTB = await BrowseImageWindowTask();

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

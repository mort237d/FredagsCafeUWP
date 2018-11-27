using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using Windows.UI.Xaml.Media;
using FredagsCafeUWP.ViewModels;

namespace FredagsCafeUWP.Models
{
    public class Stock : INotifyPropertyChanged
    {
        private Message message;

        private static ObservableCollection<Product> _products;

        private UserViewModel _userViewModel;

        private Product _selectedProduct;

        private int _minAmount = 10;

        private Color _colorRed = Colors.Red;
        private Color _colorGreen = Colors.ForestGreen;
        //private Brush _colorRed = new SolidColorBrush(Colors.Red);
        //private Brush _colorGreen = new SolidColorBrush(Colors.ForestGreen);
        private Brush _amountColor;

        private string _nameTB;
        private string _buyingPriceTB;
        private string _sellingPriceTB;
        private string _amountTB;
        private string _imageSourceTB = "";

        private string _frameAmountTB;
        private string _frameSizeTB;

        private string _productAmountTB;

        private string _productPriceChangeTB;

        private string _textDoc;

        public Stock(UserViewModel userViewModel)
        {
            message = new Message(this);

            _userViewModel = userViewModel;

            Products = new ObservableCollection<Product>();

            loadAsync();
        }

        public Stock()
        {
            
        }

        #region Properties
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set {_products = value; }
        }

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

        public string ProductPriceChangeTb
        {
            get { return _productPriceChangeTB; }
            set
            {
                _productPriceChangeTB = value; 
                OnPropertyChanged();
            }
        }

        public string TextDoc
        {
            get { return _textDoc; }
            set
            {
                _textDoc = value; 
                OnPropertyChanged();
            }
        }

        #endregion
        public void AddProductToOBListAsync()
        {
            bool productExist = false;
            if (NameTB != null)
            {
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

                    if (doubleBuyingPriceTB > 0 && doubleSellingPriceTB > 0 && AmountTB != null && intAmountTB >= 0)
                    {
                        if (ImageSourceTB == "" || ImageSourceTB == null)
                        {
                            if (intAmountTB < _minAmount) Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, "ProductImages/BlankDåse.png", _colorRed, _userViewModel));
                            else Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, "ProductImages/BlankDåse.png", _colorGreen, _userViewModel));
                        }
                        else
                        {
                            if (intAmountTB < _minAmount) Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, ImageSourceTB, _colorRed, _userViewModel));
                            else Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, ImageSourceTB, _colorGreen, _userViewModel));
                        }

                        NameTB = null;
                        BuyingPriceTB = null;
                        SellingPriceTB = null;
                        AmountTB = null;
                        ImageSourceTB = null;

                        saveAsync();
                    }
                }
                else message.Error("Varen findes allerede!", "Scroll igennem varerne, for at finde den.");
            }
        }
        
        public void RemoveProductFromOBList()
        {
            if (SelectedProduct != null)
            {
                message.YesNo("Slet produkt", "Er du sikker på at du vil slette " + SelectedProduct.Name + "?");
                saveAsync();
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public void AddAmountToProduct()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(FrameAmountTB, out int intFrameAmountTB);
                Int32.TryParse(FrameSizeTB, out int intFrameSizeTB);
                Int32.TryParse(ProductAmountTB, out int intProductAmountTB);


                if (intFrameAmountTB > 0 && intFrameSizeTB > 0 && intProductAmountTB > 0)
                {
                    SelectedProduct.Amount += (intFrameAmountTB * intFrameSizeTB) + intProductAmountTB;
                    FrameAmountTB = null;
                    FrameSizeTB = null;
                    ProductAmountTB = null;
                }
                else if (intFrameAmountTB > 0 && intFrameSizeTB > 0)
                {
                    SelectedProduct.Amount += intFrameAmountTB * intFrameSizeTB;
                    FrameAmountTB = null;
                    FrameSizeTB = null;
                }
                else if (intProductAmountTB > 0)
                {
                    SelectedProduct.Amount += intProductAmountTB;
                    ProductAmountTB = null;
                }

                if (SelectedProduct != null && SelectedProduct.Amount < _minAmount) SelectedProduct.ForegroundColor = _colorRed;
                else SelectedProduct.ForegroundColor = _colorGreen;

                saveAsync();
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public void RemoveAmountFromProduct()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(FrameAmountTB, out int intFrameAmountTB);
                Int32.TryParse(FrameSizeTB, out int intFrameSizeTB);
                Int32.TryParse(ProductAmountTB, out int intProductAmountTB);

                if (intFrameAmountTB > 0 && intFrameSizeTB > 0 && intProductAmountTB > 0)
                {
                    if (SelectedProduct.Amount >= ((intFrameAmountTB * intFrameSizeTB) + intProductAmountTB))
                    {
                        SelectedProduct.Amount -= (intFrameAmountTB * intFrameSizeTB) + intProductAmountTB;
                        FrameAmountTB = null;
                        FrameSizeTB = null;
                        ProductAmountTB = null;
                    }
                    else message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + "." +
                                 "\nDerfor kan du ikke fjerne " + intFrameAmountTB + "*" + intFrameSizeTB + "+" + intProductAmountTB + "=" + ((intFrameAmountTB * intFrameSizeTB) + intProductAmountTB) + " af dette produkt.");
                }
                else if (intFrameAmountTB > 0 && intFrameSizeTB > 0)
                {
                    if (SelectedProduct.Amount >= (intFrameAmountTB * intFrameSizeTB))
                    {
                        SelectedProduct.Amount -= intFrameAmountTB * intFrameSizeTB;
                        FrameAmountTB = null;
                        FrameSizeTB = null;
                    }
                    else message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + "." +
                                 "\nDerfor kan du ikke fjerne " + intFrameAmountTB + "*" + intFrameSizeTB + "=" + (intFrameAmountTB * intFrameSizeTB) + " af dette produkt.");
                }
                else if (intProductAmountTB > 0)
                {
                    if (SelectedProduct.Amount >= intProductAmountTB)
                    {
                        SelectedProduct.Amount -= intProductAmountTB;
                        ProductAmountTB = null;
                    }
                    else message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + ".\nDerfor kan du ikke fjerne " + intProductAmountTB + " af dette produkt.");
                }

                if (SelectedProduct.Amount < _minAmount)
                {
                    SelectedProduct.ForegroundColor = _colorRed;
                    message.Error("Advarsel", "Lageret er næsten tomt");
                }
                else SelectedProduct.ForegroundColor = _colorGreen;

                saveAsync();
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
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
            ImageSourceTB = await BrowseImageWindowTask();
        }

        public void ChangeProductSellPrice()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTB);
                if (ProductPriceChangeTb != null && intProductPriceChangedTB > 0)
                {
                    SelectedProduct.SellingPrice = intProductPriceChangedTB;
                    ProductPriceChangeTb = null;

                    saveAsync();
                }
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public void ChangeProductBuyPrice()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTB);
                if (ProductPriceChangeTb != null && intProductPriceChangedTB > 0)
                {
                    SelectedProduct.BuyingPrice = intProductPriceChangedTB;
                    ProductPriceChangeTb = null;

                    saveAsync();
                }
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public async void saveAsync()
        {
            Debug.WriteLine("Saving product async...");
            await XMLReadWriteClass.SaveObjectToXml<ObservableCollection<Product>>(Products, "stock.xml");
            Debug.WriteLine("products.count: " + Products.Count);
        }
        private async void loadAsync()
        {
            try
            {
                Debug.WriteLine("loading product async...");
                Products = await XMLReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("stock.xml");
                Debug.WriteLine("products.count:" + Products.Count);
                OnPropertyChanged("_products");
            }
            catch (Exception e)
            {
                Products.Add(new Product(2, 5, "Tuborg Classic", 48, 0, "ProductImages/TuborgClassic.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Grøn Tuborg", 48, 0, "ProductImages/GrønTuborg.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Tuborg Gylden Dame", 48, 0, "ProductImages/TuborgGuldDame.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Carlsberg", 48, 0, "ProductImages/Carlsberg.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Cola Zero", 48, 0, "ProductImages/ColaZero.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Cola", 48, 0, "ProductImages/Cola.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Mokai", 48, 0, "ProductImages/Mokai.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Mokai Jordbær Lime", 48, 0, "ProductImages/MokaiStrawberryLime.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Somersby Apple Cider", 48, 0, "ProductImages/SomersbyApple.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Somersby Pear Cider", 48, 0, "ProductImages/SomersbyPear.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Breezer", 48, 0, "ProductImages/Breezer.png", _colorGreen, _userViewModel));
                Products.Add(new Product(2, 5, "Fanta", 48, 0, "ProductImages/Fanta.png", _colorGreen, _userViewModel));

                saveAsync();
            }
            
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

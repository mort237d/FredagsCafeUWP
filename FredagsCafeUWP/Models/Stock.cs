using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using Windows.UI.Xaml.Media;
using FredagsCafeUWP.ViewModels;

namespace FredagsCafeUWP.Models
{
    public class Stock : INotifyPropertyChanged
    {
        #region Field

        private readonly Message _message;

        private static ObservableCollection<Product> _products;

        private Product _selectedProduct;

        private int _minAmount = 10;

        private readonly Color _colorRed = Colors.Red;
        private readonly Color _colorGreen = Colors.ForestGreen; //TODO Ændre til brush, så farven træder frem i programmet
        //private Brush _colorRed = new SolidColorBrush(Colors.Red);
        //private Brush _colorGreen = new SolidColorBrush(Colors.ForestGreen);
        private Brush _amountColor;

        private string _nameTb;
        private string _buyingPriceTb;
        private string _sellingPriceTb;
        private string _amountTb;
        private string _imageSourceTb = "";

        private string _frameAmountTb;
        private string _frameSizeTb;

        private string _productAmountTb;

        private string _productPriceChangeTb;

        #endregion

        public Stock()
        {
            _message = new Message(this);

            LoadAsync();
        }

        #region Properties
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => _products = value;
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value; 
                OnPropertyChanged();
            }
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

        public string FrameAmountTb
        {
            get => _frameAmountTb;
            set
            {
                _frameAmountTb = value;
                OnPropertyChanged();
            }
        }

        public string FrameSizeTb
        {
            get => _frameSizeTb;
            set
            {
                _frameSizeTb = value;
                OnPropertyChanged();
            }
        }

        public string ProductAmountTb
        {
            get => _productAmountTb;
            set
            {
                _productAmountTb = value; 
                OnPropertyChanged();
            }
        }

        public string ProductPriceChangeTb
        {
            get => _productPriceChangeTb;
            set
            {
                _productPriceChangeTb = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region ButtonMethods

        public async void AddProductToObListAsync()
        {
            bool productExist = false;
            if (NameTb != null)
            {
                foreach (var element in Products)
                {
                    if (element.Name.ToLower().Equals(NameTb.ToLower()))
                    {
                        productExist = true;
                        break;
                    }
                }
                
                if (!productExist)
                {
                    double.TryParse(BuyingPriceTb, out double doubleBuyingPriceTb);
                    double.TryParse(SellingPriceTb, out double doubleSellingPriceTb);
                    int.TryParse(AmountTb, out int intAmountTb);

                    if (doubleBuyingPriceTb > 0 && doubleSellingPriceTb > 0 && AmountTb != null && intAmountTb >= 0)
                    {
                        if (string.IsNullOrEmpty(ImageSourceTb))
                        {
                            if (intAmountTb < _minAmount) Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", _colorRed));
                            else Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", _colorGreen));
                        }
                        else
                        {
                            if (intAmountTb < _minAmount) Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb, 0, ImageSourceTb, _colorRed));
                            else Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb, 0, ImageSourceTb, _colorGreen));
                        }

                        NameTb = null;
                        BuyingPriceTb = null;
                        SellingPriceTb = null;
                        AmountTb = null;
                        ImageSourceTb = null;

                        SaveAsync();
                    }
                }
                else await _message.Error("Varen findes allerede!", "Scroll igennem varerne, for at finde den.");
            }
        }
        
        public async void RemoveProductFromObList()
        {
            if (SelectedProduct != null)
            {
                await _message.YesNo("Slet produkt", "Er du sikker på at du vil slette " + SelectedProduct.Name + "?");
            }
            else await _message.Error("Intet produkt valgt", "Vælg venligst et produkt.");
        }

        public async void AddAmountToProduct()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(FrameAmountTb, out int intFrameAmountTb);
                Int32.TryParse(FrameSizeTb, out int intFrameSizeTb);
                Int32.TryParse(ProductAmountTb, out int intProductAmountTb);


                if (intFrameAmountTb > 0 && intFrameSizeTb > 0 && intProductAmountTb > 0)
                {
                    SelectedProduct.Amount += (intFrameAmountTb * intFrameSizeTb) + intProductAmountTb;
                    FrameAmountTb = null;
                    FrameSizeTb = null;
                    ProductAmountTb = null;
                }
                else if (intFrameAmountTb > 0 && intFrameSizeTb > 0)
                {
                    SelectedProduct.Amount += intFrameAmountTb * intFrameSizeTb;
                    FrameAmountTb = null;
                    FrameSizeTb = null;
                }
                else if (intProductAmountTb > 0)
                {
                    SelectedProduct.Amount += intProductAmountTb;
                    ProductAmountTb = null;
                }

                if (SelectedProduct != null && SelectedProduct.Amount < _minAmount) SelectedProduct.ForegroundColor = _colorRed;
                else SelectedProduct.ForegroundColor = _colorGreen;

                SaveAsync();
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public async void RemoveAmountFromProduct()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(FrameAmountTb, out int intFrameAmountTb);
                Int32.TryParse(FrameSizeTb, out int intFrameSizeTb);
                Int32.TryParse(ProductAmountTb, out int intProductAmountTb);

                if (intFrameAmountTb > 0 && intFrameSizeTb > 0 && intProductAmountTb > 0)
                {
                    if (SelectedProduct.Amount >= ((intFrameAmountTb * intFrameSizeTb) + intProductAmountTb))
                    {
                        SelectedProduct.Amount -= (intFrameAmountTb * intFrameSizeTb) + intProductAmountTb;
                        FrameAmountTb = null;
                        FrameSizeTb = null;
                        ProductAmountTb = null;
                    }
                    else await _message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + "." +
                                                                      "\nDerfor kan du ikke fjerne " + intFrameAmountTb + "*" + intFrameSizeTb + "+" + intProductAmountTb + "=" + ((intFrameAmountTb * intFrameSizeTb) + intProductAmountTb) + " af dette produkt.");
                }
                else if (intFrameAmountTb > 0 && intFrameSizeTb > 0)
                {
                    if (SelectedProduct.Amount >= (intFrameAmountTb * intFrameSizeTb))
                    {
                        SelectedProduct.Amount -= intFrameAmountTb * intFrameSizeTb;
                        FrameAmountTb = null;
                        FrameSizeTb = null;
                    }
                    else await _message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + "." +
                                                                      "\nDerfor kan du ikke fjerne " + intFrameAmountTb + "*" + intFrameSizeTb + "=" + (intFrameAmountTb * intFrameSizeTb) + " af dette produkt.");
                }
                else if (intProductAmountTb > 0)
                {
                    if (SelectedProduct.Amount >= intProductAmountTb)
                    {
                        SelectedProduct.Amount -= intProductAmountTb;
                        ProductAmountTb = null;
                    }
                    else await _message.Error("Tallene stemmer ikke", "Der er kun " + SelectedProduct.Amount + " af " + SelectedProduct.Name + ".\nDerfor kan du ikke fjerne " + intProductAmountTb + " af dette produkt.");
                }

                if (SelectedProduct.Amount < _minAmount)
                {
                    SelectedProduct.ForegroundColor = _colorRed;
                    await _message.Error("Advarsel", "Lageret er næsten tomt");
                }
                else SelectedProduct.ForegroundColor = _colorGreen;

                SaveAsync();
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
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

        public async void ChangeProductSellPrice()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTb);
                if (ProductPriceChangeTb != null && intProductPriceChangedTb > 0)
                {
                    SelectedProduct.SellingPrice = intProductPriceChangedTb;
                    ProductPriceChangeTb = null;

                    SaveAsync();
                }
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public async void ChangeProductBuyPrice()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTb);
                if (ProductPriceChangeTb != null && intProductPriceChangedTb > 0)
                {
                    SelectedProduct.BuyingPrice = intProductPriceChangedTb;
                    ProductPriceChangeTb = null;


                    //saveAsync();
                }
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }


        #endregion

        #region Save/Load

        public async void SaveAsync()
        {
            Debug.WriteLine("Saving product async...");
            await XmlReadWriteClass.SaveObjectToXml(Products, "stock.xml");
            Debug.WriteLine("products.count: " + Products.Count);
        }
        private async void LoadAsync()
        {
            try
            {
                Debug.WriteLine("loading product async...");
                Products = await XmlReadWriteClass.ReadObjectFromXmlFileAsync<ObservableCollection<Product>>("stock.xml");
                Debug.WriteLine("products.count:" + Products.Count);
            }
            catch (Exception)
            {
                Products = new ObservableCollection<Product>()
                {
                    new Product(2, 5, "Tuborg Classic", 48, 0, "ProductImages/TuborgClassic.png", _colorGreen),
                    new Product(2, 5, "Grøn Tuborg", 48, 0, "ProductImages/GrønTuborg.png", _colorGreen),
                    new Product(2, 5, "Tuborg Gylden Dame", 48, 0, "ProductImages/TuborgGuldDame.png", _colorGreen),
                    new Product(2, 5, "Carlsberg", 48, 0, "ProductImages/Carlsberg.png", _colorGreen),
                    new Product(2, 5, "Cola Zero", 48, 0, "ProductImages/ColaZero.png", _colorGreen),
                    new Product(2, 5, "Cola", 48, 0, "ProductImages/Cola.png", _colorGreen),
                    new Product(2, 5, "Mokai", 48, 0, "ProductImages/Mokai.png", _colorGreen),
                    new Product(2, 5, "Mokai Jordbær Lime", 48, 0, "ProductImages/MokaiStrawberryLime.png", _colorGreen),
                    new Product(2, 5, "Somersby Apple Cider", 48, 0, "ProductImages/SomersbyApple.png", _colorGreen),
                    new Product(2, 5, "Somersby Pear Cider", 48, 0, "ProductImages/SomersbyPear.png", _colorGreen),
                    new Product(2, 5, "Breezer", 48, 0, "ProductImages/Breezer.png", _colorGreen),
                    new Product(2, 5, "Fanta", 48, 0, "ProductImages/Fanta.png", _colorGreen)
                };
                SaveAsync();
            }
            
        }

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

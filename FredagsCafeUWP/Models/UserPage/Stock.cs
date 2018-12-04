using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;
using Windows.UI.Xaml.Media;

namespace FredagsCafeUWP.Models
{
    public class Stock : INotifyPropertyChanged
    {
        #region Field
        private bool _showAddProductPopUp = false;

        private readonly Message _message = Message.Instance;
        private Product _selectedProduct = new Product();

        private ObservableCollection<Product> _products;

        public string _colorRed = "Red";
        public string _colorGreen = "ForestGreen";
        private Brush _amountColor;

        public int _minAmount = 10;

        private string _frameAmountTb;
        private string _frameSizeTb;

        private string _productAmountTb;

        private string _productPriceChangeTb;

        private int _selectionStart;

        private string _nameTb;
        private string _buyingPriceTb;
        private string _sellingPriceTb;
        private string _amountTb;
        private string _imageSourceTb = "";

        #endregion

        private Stock()
        {
        }

        #region Singleton

        private static Stock _instance;
        public static Stock Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Stock();
                }
                return _instance;
            }
        }

        #endregion

        #region Properties
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

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
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
                _productPriceChangeTb = value; //TODO opdater når det ikke er tal...
                //if (!Regex.IsMatch(_productPriceChangeTb, "^\\d*\\.?\\d*$") && _productPriceChangeTb != "")
                //{
                //    int pos = _selectionStart - 1;
                //    _productPriceChangeTb = _productPriceChangeTb.Remove(pos);//Remove(pos, 1);
                //    _selectionStart = pos;
                //}

                //double dtemp;
                //if (!double.TryParse(value, out dtemp) && value != "")
                //{
                //    int pos = _selectionStart - 1;
                //    _productPriceChangeTb = value.Remove(pos);
                //    _selectionStart = pos;
                //    Debug.WriteLine(_productPriceChangeTb);
                //}
                OnPropertyChanged();
            }
        }

        public int SelectionStart
        {
            get { return _selectionStart; }
            set
            {
                _selectionStart = value;
                OnPropertyChanged();
            }
        }

        public string ColorRed
        {
            get { return _colorRed; }
            set { _colorRed = value; }
        }

        public string ColorGreen
        {
            get { return _colorGreen; }
            set { _colorGreen = value; }
        }

        public bool ShowAddProductPopUp
        {
            get { return _showAddProductPopUp; }
            set
            {
                _showAddProductPopUp = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ButtonMethods
        public async void AddProductToObList()
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
                    if (double.TryParse(BuyingPriceTb, out double doubleBuyingPriceTb) &&
                    double.TryParse(SellingPriceTb, out double doubleSellingPriceTb) &&
                    int.TryParse(AmountTb, out int intAmountTb))
                    {
                        if (doubleBuyingPriceTb > 0 && doubleSellingPriceTb > 0 && AmountTb != null && intAmountTb >= 0)
                        {
                            if (string.IsNullOrEmpty(ImageSourceTb))
                            {
                                if (intAmountTb < _minAmount) //TODO what is going on here?
                                    Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, "ProductImages/BlankDåse.png", _colorRed));
                                else
                                    Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb,
                                        intAmountTb,
                                        0, "ProductImages/BlankDåse.png", _colorGreen));
                            }
                            else
                            {
                                if (intAmountTb < _minAmount)
                                    Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, ImageSourceTb, ColorRed));
                                else
                                    Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, NameTb, intAmountTb,
                                        0, ImageSourceTb, ColorGreen));
                            }

                            NameTb = null;
                            BuyingPriceTb = null;
                            SellingPriceTb = null;
                            AmountTb = null;
                            ImageSourceTb = null;

                            Debug.WriteLine("product: " + Products.Count);
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

        public void ShowAddProductPopUpMethod()
        {
            ShowAddProductPopUp = true;
        }

        public async void RemoveProductFromObList()
        {
            if (SelectedProduct != null)
            {
                await _message.YesNo("Slet produkt", "Er du sikker på at du vil slette " + SelectedProduct.Name + "?");
            }
            else await _message.Error("Intet produkt valgt", "Vælg venligst et produkt.");
        }

        private int _intFrameAmountTb;
        private int _intFrameSizeTb;
        private int _intProductAmountTb;

        public async void AddAmountToProduct()
        {
            if (SelectedProduct != null)
            {
                Int32.TryParse(FrameAmountTb, out _intFrameAmountTb);
                Int32.TryParse(FrameSizeTb, out _intFrameSizeTb);
                Int32.TryParse(ProductAmountTb, out _intProductAmountTb);

                if (_intFrameAmountTb > 0 && _intFrameSizeTb > 0 && _intProductAmountTb > 0)
                {
                    if (Int32.TryParse(FrameAmountTb, out _intFrameAmountTb) &&
                        Int32.TryParse(FrameSizeTb, out _intFrameSizeTb) &&
                        Int32.TryParse(ProductAmountTb, out _intProductAmountTb))
                    {
                        SelectedProduct.Amount += (_intFrameAmountTb * _intFrameSizeTb) + _intProductAmountTb;
                        FrameAmountTb = null;
                        FrameSizeTb = null;
                        ProductAmountTb = null;
                    }
                    else await _message.Error("Forkert input", "Antal rammer, Ramme størrelse og Stk. skal være tal");
                }

                else if (_intFrameAmountTb > 0 && _intFrameSizeTb > 0)
                {
                    if (Int32.TryParse(FrameAmountTb, out _intFrameAmountTb) &&
                        Int32.TryParse(FrameSizeTb, out _intFrameSizeTb))
                    {
                        SelectedProduct.Amount += _intFrameAmountTb * _intFrameSizeTb;
                        FrameAmountTb = null;
                        FrameSizeTb = null;
                    }
                    else await _message.Error("Forkert input", "Antal rammer og Ramme størrelse skal være tal");
                }

                else if (_intProductAmountTb > 0)
                {
                    if (Int32.TryParse(ProductAmountTb, out _intProductAmountTb))
                    {
                        SelectedProduct.Amount += _intProductAmountTb;
                        ProductAmountTb = null;
                    }
                    else await _message.Error("Forkert input", "Stk. skal være tal");
                }

                else await _message.Error("Manglende input", "Til tal til felterne for at tilføje antal til produktet");
                
                if (SelectedProduct != null && SelectedProduct.Amount < _minAmount) SelectedProduct.ForegroundColor = ColorRed;
                else SelectedProduct.ForegroundColor = ColorGreen;
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
                    SelectedProduct.ForegroundColor = ColorRed;
                    await _message.Error("Advarsel", "Lageret er næsten tomt");
                }
                else SelectedProduct.ForegroundColor = ColorGreen;

                
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public async void ChangeProductSellPrice()
        {
            if (SelectedProduct != null)
            {
                if (Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTb))
                {
                    if (ProductPriceChangeTb != null && intProductPriceChangedTb > 0)
                    {
                        SelectedProduct.SellingPrice = intProductPriceChangedTb;
                        ProductPriceChangeTb = null;

                        
                    }
                    else await _message.Error("Forkert input", "Prisen skal være mere end 0.");
                }
                else await _message.Error("Forkert input", "Prisen skal være et tal.");
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }

        public async void ChangeProductBuyPrice()
        {
            if (SelectedProduct != null)
            {
                if (Int32.TryParse(ProductPriceChangeTb, out int intProductPriceChangedTb))
                {
                    if (ProductPriceChangeTb != null && intProductPriceChangedTb > 0)
                    {
                        SelectedProduct.BuyingPrice = intProductPriceChangedTb;
                        ProductPriceChangeTb = null;

                        
                    }
                    else await _message.Error("Forkert input", "Prisen skal være mere end 0.");
                }
                else await _message.Error("Forkert input", "Prisen skal være et tal.");
            }
            else await _message.Error("Intet produkt valg", "Vælg venligst et produkt");
        }


        #endregion

        #region Save/Load

        public async Task SaveAsync()
        {
            Debug.WriteLine("Saving product async...");
            await XmlReadWriteClass.SaveObjectToXml(Products, "stock.xml");
            Debug.WriteLine("products.count: " + Products.Count);
        }

        public async void LoadAsync()
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
                    new Product(2, 5, "Tuborg Classic", 48, 0, "ProductImages/TuborgClassic.png", ColorGreen),
                    new Product(2, 5, "Grøn Tuborg", 48, 0, "ProductImages/GrønTuborg.png", ColorGreen),
                    new Product(2, 5, "Tuborg Gylden Dame", 48, 0, "ProductImages/TuborgGuldDame.png", ColorGreen),
                    new Product(2, 5, "Carlsberg", 48, 0, "ProductImages/Carlsberg.png", ColorGreen),
                    new Product(2, 5, "Cola Zero", 48, 0, "ProductImages/ColaZero.png", ColorGreen),
                    new Product(2, 5, "Cola", 48, 0, "ProductImages/Cola.png", ColorGreen),
                    new Product(2, 5, "Mokai", 48, 0, "ProductImages/Mokai.png", ColorGreen),
                    new Product(2, 5, "Mokai Jordbær Lime", 48, 0, "ProductImages/MokaiStrawberryLime.png", ColorGreen),
                    new Product(2, 5, "Somersby Apple Cider", 48, 0, "ProductImages/SomersbyApple.png", ColorGreen),
                    new Product(2, 5, "Somersby Pear Cider", 48, 0, "ProductImages/SomersbyPear.png", ColorGreen),
                    new Product(2, 5, "Breezer", 48, 0, "ProductImages/Breezer.png", ColorGreen),
                    new Product(2, 5, "Fanta", 48, 0, "ProductImages/Fanta.png", ColorGreen)
                };
                
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

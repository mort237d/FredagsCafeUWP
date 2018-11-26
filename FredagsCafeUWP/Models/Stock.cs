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

namespace FredagsCafeUWP.Models
{
    class Stock : INotifyPropertyChanged
    {
        private Message message;

        private ObservableCollection<Product> _products;

        private Product _selectedProduct;

        private int _minAmount = 10;
        private Brush _colorRed = new SolidColorBrush(Colors.Red);
        private Brush _colorGreen = new SolidColorBrush(Colors.ForestGreen);
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

        public Stock()
        {
            message = new Message(this);

            Products = new ObservableCollection<Product>()
            {
                new Product(66, 67, "Tuborg Classic", 22, 2, "ProductImages/TuborgClassic.png", _colorGreen),
                new Product(55, 63, "Grøn Tuborg", 21, 13, "ProductImages/GrønTuborg.png", _colorGreen),
                new Product(55, 63, "Tuborg Gylden Dame", 24, 13, "ProductImages/TuborgGuldDame.png", _colorGreen),
                new Product(55, 63, "Carlsberg", 32, 13, "ProductImages/Carlsberg.png", _colorGreen),
                new Product(55, 63, "Cola Zero", 28, 13, "ProductImages/ColaZero.png", _colorGreen),
                new Product(55, 63, "Cola", 24, 13, "ProductImages/Cola.png", _colorGreen),
                new Product(55, 63, "Mokai", 29, 13, "ProductImages/Mokai.png", _colorGreen),
                //new Product(55, 63, "Mokai Jordbær Lime", 9, 13, "ProductImages/MokaiStrawberryLime.png", _colorRed),
                //new Product(55, 63, "Somersby Apple Cider", 42, 13, "ProductImages/SomersbyApple.png", _colorGreen),
                //new Product(55, 63, "Somersby Pear Cider", 15, 13, "ProductImages/SomersbyPear.png", _colorGreen),
                //new Product(55, 63, "Breezer", 10, 13, "ProductImages/Breezer.png", _colorGreen),
                //new Product(55, 63, "Fanta", 5, 13, "ProductImages/Fanta.png", _colorRed)
            };


            //WriteListToTxt();
            ReadTxt();
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
      

        public async void WriteListToTxt()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile textFile = await localFolder.CreateFileAsync("Stock.txt", CreationCollisionOption.ReplaceExisting);

                using (IRandomAccessStream iRandomAccessStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (DataWriter textWriter = new DataWriter(iRandomAccessStream))
                    {
                        foreach (var p in Products) textWriter.WriteString(p.ToString());
                        await textWriter.StoreAsync();
                    }
                }

                message.Error("Gemt", null);
            }
            catch
            {
                message.Error("Kan ikke gemme filen", null);
            }
        }

        public async void ReadTxt()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile textFile = await localFolder.GetFileAsync("Stock.txt");

                using (IRandomAccessStream textstream = await textFile.OpenReadAsync())
                {
                    using (DataReader textReader = new DataReader(textstream))
                    {
                        uint textLength = (uint)textstream.Size;
                        await textReader.LoadAsync(textLength);
                        TextDoc = textReader.ReadString(textLength);
                        
                        string[] lines = TextDoc.Split(
                            new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.None
                        );

                        foreach (var line in lines)
                        {
                            if ((int.Parse(line.Substring(line.IndexOf("Antal: ") + "Antal: ".Length,
                                    (line.LastIndexOf("Antal solgt: ")) -
                                    (line.IndexOf("Antal: ") + "Antal: ".Length)))) < 10)
                            _amountColor = _colorRed;
                            else _amountColor = _colorGreen;
                            Products.Add(new Product(
                                double.Parse(line.Substring(line.IndexOf("Købspris: ") + "Købspris: ".Length, (line.LastIndexOf("Salgspris: ")) - (line.IndexOf("Købspris: ") + "Købspris: ".Length))),
                                double.Parse(line.Substring(line.IndexOf("Salgspris: ") + "Salgspris: ".Length, (line.LastIndexOf("Antal: ")) - (line.IndexOf("Salgspris: ") + "Salgspris: ".Length))),
                                line.Substring(line.IndexOf("Navn: ") + "Navn: ".Length, (line.LastIndexOf("Købspris: ")) - (line.IndexOf("Navn: ") + "Navn: ".Length)),
                                int.Parse(line.Substring(line.IndexOf("Antal: ") + "Antal: ".Length, (line.LastIndexOf("Antal solgt: ")) - (line.IndexOf("Antal: ") + "Antal: ".Length))),
                                int.Parse(line.Substring(line.IndexOf("Antal solgt: ") + "Antal solgt: ".Length, (line.LastIndexOf("Image Source: ")) - (line.IndexOf("Antal solgt: ") + "Antal solgt: ".Length))),
                                line.Substring(line.IndexOf("Image Source: ") + "Image Source: ".Length, (line.LastIndexOf("ForegroundColor: ")) - (line.IndexOf("Image Source: ") + "Image Source: ".Length)),
                                _amountColor
                                ));
                        }
                    }
                }
            }
            catch
            {
                message.Error("Kan ikke læse filen", null);
            }
        }

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
                            if (intAmountTB < _minAmount) Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, "ProductImages/BlankDåse.png", _colorRed));
                            else Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, "ProductImages/BlankDåse.png", _colorGreen));
                        }
                        else
                        {
                            if (intAmountTB < _minAmount) Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, ImageSourceTB, _colorRed));
                            else Products.Add(new Product(doubleBuyingPriceTB, doubleSellingPriceTB, NameTB, intAmountTB, 0, ImageSourceTB, _colorGreen));
                        }

                        NameTB = null;
                        BuyingPriceTB = null;
                        SellingPriceTB = null;
                        AmountTB = null;
                        ImageSourceTB = null;

                        WriteListToTxt();
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
                WriteListToTxt();
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

                WriteListToTxt();
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

                WriteListToTxt();
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

                    WriteListToTxt();
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

                    WriteListToTxt();
                }
            }
            else message.Error("Intet produkt valg", "Vælg venligst et produkt");
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

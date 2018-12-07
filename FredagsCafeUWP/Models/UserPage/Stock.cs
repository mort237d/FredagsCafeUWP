using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Stock : INotifyPropertyChanged
    {
        #region Field
        private bool _showAddProductPopUp = false;
        private bool _showChangeProductPopUp = false;

        private readonly Message _message;
        private Product _selectedProduct = new Product();

        private ObservableCollection<Product> _products;

        public string _colorRed = "Red";
        public string _colorGreen = "ForestGreen";

        public int MinAmount = 10;

        private string _productPriceChangeTb;

        private string _addNameTb;
        private string _addBuyingPriceTb;
        private string _addSellingPriceTb;
        private string _addAmountTb;
        private string _addImageSourceTb = "";
        private string _addTypeTb;

        private string _changeNameTb;
        private string _changeBuyingPriceTb;
        private string _changeSellingPriceTb;
        private string _changeAmountTb;
        private string _changeImageSourceTb = "";
        private string _changeTypeTb;

        #endregion

        private Stock()
        {
            _message = new Message(this);
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
        public string AddNameTb
        {
            get => _addNameTb;
            set
            {
                _addNameTb = value;
                OnPropertyChanged();
            }
        }

        public string AddBuyingPriceTb
        {
            get => _addBuyingPriceTb;
            set
            {
                _addBuyingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string AddSellingPriceTb
        {
            get => _addSellingPriceTb;
            set
            {
                _addSellingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string AddAmountTb
        {
            get => _addAmountTb;
            set
            {
                _addAmountTb = value;
                OnPropertyChanged();
            }
        }

        public string AddImageSourceTb
        {
            get => _addImageSourceTb;
            set
            {
                _addImageSourceTb = value;
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

        public string ProductPriceChangeTb
        {
            get => _productPriceChangeTb;
            set
            {
                _productPriceChangeTb = value;
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

        public bool ShowChangeProductPopUp
        {
            get { return _showChangeProductPopUp; }
            set
            {
                _showChangeProductPopUp = value;
                OnPropertyChanged();
            }
        }

        public string ChangeNameTb
        {
            get { return _changeNameTb; }
            set
            {
                _changeNameTb = value;
                OnPropertyChanged();
            }
        }

        public string ChangeBuyingPriceTb
        {
            get { return _changeBuyingPriceTb; }
            set
            {
                _changeBuyingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string ChangeSellingPriceTb
        {
            get { return _changeSellingPriceTb; }
            set
            {
                _changeSellingPriceTb = value;
                OnPropertyChanged();
            }
        }

        public string ChangeAmountTb
        {
            get { return _changeAmountTb; }
            set
            {
                _changeAmountTb = value;
                OnPropertyChanged();
            }
        }

        public string ChangeImageSourceTb
        {
            get { return _changeImageSourceTb; }
            set
            {
                _changeImageSourceTb = value;
                OnPropertyChanged();
            }
        }

        public string AddTypeTb
        {
            get { return _addTypeTb; }
            set
            {
                _addTypeTb = value; 
                OnPropertyChanged();
            }
        }

        public string ChangeTypeTb
        {
            get { return _changeTypeTb; }
            set
            {
                _changeTypeTb = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region ButtonMethods
        public async void AddProductToObList()
        {

            bool productExist = false;
            if (AddNameTb != null)
            {
                foreach (var element in Products)
                {
                    if (element.Name.ToLower().Equals(AddNameTb.ToLower()))
                    {
                        productExist = true;
                        break;
                    }
                }

                if (!productExist)
                {
                    if (double.TryParse(AddBuyingPriceTb, out double doubleBuyingPriceTb) && double.TryParse(AddSellingPriceTb, out double doubleSellingPriceTb) && int.TryParse(AddAmountTb, out int intAmountTb))
                    {
                        if (doubleBuyingPriceTb > 0 && doubleSellingPriceTb > 0 && AddAmountTb != null && intAmountTb >= 0)
                        {
                            if (string.IsNullOrEmpty(AddImageSourceTb))
                            {
                                if (intAmountTb < MinAmount)
                                {
                                    switch (AddTypeTb)
                                    {
                                        case "Øl":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Beer));
                                            break;
                                        case "Sodavand":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Soda));
                                            break;
                                        case "Cider":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Cider));
                                            break;
                                        case "Drink":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Drink));
                                            break;
                                        case "Flaske":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Bottle));
                                            break;
                                        case "Shot":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Shot));
                                            break;
                                        case "Andet":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategoryBeer.Other));
                                            break;
                                    }
                                }
                                else
                                { 
                                    switch (AddTypeTb)
                                    {
                                        case "Øl":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Beer));
                                            break;
                                        case "Sodavand":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Soda));
                                            break;
                                        case "Cider":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Cider));
                                            break;
                                        case "Drink":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Drink));
                                            break;
                                        case "Flaske":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Bottle));
                                            break;
                                        case "Shot":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Shot));
                                            break;
                                        case "Andet":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategoryBeer.Other));
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (intAmountTb < MinAmount)
                                {
                                    switch (AddTypeTb)
                                    {
                                        case "Øl":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Beer));
                                            break;
                                        case "Sodavand":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Soda));
                                            break;
                                        case "Cider":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Cider));
                                            break;
                                        case "Drink":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Drink));
                                            break;
                                        case "Flaske":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Bottle));
                                            break;
                                        case "Shot":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Shot));
                                            break;
                                        case "Andet":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategoryBeer.Other));
                                            break;
                                    }
                                }

                                else
                                {
                                    switch (AddTypeTb)
                                    {
                                        case "Øl":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Beer));
                                            break;
                                        case "Sodavand":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Soda));
                                            break;
                                        case "Cider":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Cider));
                                            break;
                                        case "Drink":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Drink));
                                            break;
                                        case "Flaske":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Bottle));
                                            break;
                                        case "Shot":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Shot));
                                            break;
                                        case "Andet":
                                            Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategoryBeer.Other));
                                            break;
                                    }
                                }
                            }

                            AddNameTb = null;
                            AddBuyingPriceTb = null;
                            AddSellingPriceTb = null;
                            AddAmountTb = null;
                            AddImageSourceTb = null;
                            AddTypeTb = null;

                            ShowAddProductPopUp = false;
                        }
                        else await _message.Error("Forkert input", "Købspris og Salgspris skal være mere en 0.");
                    }
                    else await _message.Error("Forkert input", "Købspris, Salgspris og Antal skal være tal.");
                }
                else await _message.Error("Varen findes allerede!", "Scroll igennem varerne, for at finde den.");
            }
            else await _message.Error("Forkert input", "Produktet skal have et navn.");
        }

        public async Task<string> BrowseAddImageWindowTask()
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

        public async void BrowseAddImageButton()
        {
            AddImageSourceTb = await BrowseAddImageWindowTask();
        }

        public async Task<string> BrowseChangeImageWindowTask()
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

        public async void BrowseChangeImageButton()
        {
            ChangeImageSourceTb = await BrowseChangeImageWindowTask();
        }

        public void ShowAddProductPopUpMethod()
        {
            ShowAddProductPopUp = true;
        }

        public async void ShowChangeProductPopUpMethod()
        {
            if (SelectedProduct.Name != null)
            {
                ChangeNameTb = SelectedProduct.Name;
                ChangeBuyingPriceTb = SelectedProduct.BuyingPrice.ToString();
                ChangeSellingPriceTb = SelectedProduct.SellingPrice.ToString();
                ChangeAmountTb = SelectedProduct.Amount.ToString();
                ChangeImageSourceTb = SelectedProduct.ImageSource;
                ChangeTypeTb = SelectedProduct.CategoryBeer.ToString();


                ShowChangeProductPopUp = true;
            }
            else await _message.Error("Intet produkt valgt", "Vælg venligst et produkt.");
        }

        public async void ChangeProductOfObList()
        {
            SelectedProduct.Name = ChangeNameTb;
            if (double.TryParse(ChangeBuyingPriceTb, out double doubleChangeBuyingPriceTb)) SelectedProduct.BuyingPrice = doubleChangeBuyingPriceTb;
            else await _message.Error("Forkert input", "Købspris skal være et tal.");
            if (double.TryParse(ChangeSellingPriceTb, out double doubleChangeSellingPriceTb)) SelectedProduct.SellingPrice = doubleChangeSellingPriceTb;
            else await _message.Error("Forkert input", "Salgspris skal være et tal.");
            if (int.TryParse(ChangeAmountTb, out int intChangeAmountTb)) SelectedProduct.Amount = intChangeAmountTb;
            else await _message.Error("Forkert input", "Antal skal være et hel tal.");
            SelectedProduct.ImageSource = ChangeImageSourceTb;

            switch (ChangeTypeTb)
            {
                case "Øl":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Beer;
                    break;
                case "Sodavand":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Soda;
                    break;
                case "Cider":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Cider;
                    break;
                case "Drink":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Drink;
                    break;
                case "Flaske":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Bottle;
                    break;
                case "Shot":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Shot;
                    break;
                case "Andet":
                    SelectedProduct.CategoryBeer = EnumCategory.ProductCategoryBeer.Other;
                    break;
            }

            ChangeNameTb = null;
            ChangeBuyingPriceTb = null;
            ChangeSellingPriceTb = null;
            ChangeAmountTb = null;
            ChangeImageSourceTb = null;
            ChangeTypeTb = null;  //Todo Combobox selected item bliver ikke placeholder text begrund af underlig binding

            ShowChangeProductPopUp = false;
        }

        public async void RemoveProductFromObList()
        {
            if (SelectedProduct.Name != null)
            {
                await _message.YesNo("Slet produkt", "Er du sikker på at du vil slette " + SelectedProduct.Name + "?");
            }
            else await _message.Error("Intet produkt valgt", "Vælg venligst et produkt.");
        }

        public ObservableCollection<Product> SortOCByType(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryBeer)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }


        public ObservableCollection<Product> Test(ObservableCollection<Product> inputObservableCollection, int whereToBegin)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            switch (whereToBegin)
            {
                case 1: outPutObservableCollection = TestBeer(inputObservableCollection);
                    break;
                case 2: outPutObservableCollection = TestBottle(inputObservableCollection);
                    break;
                case 3: outPutObservableCollection = TestCider(inputObservableCollection);
                    break;
                case 4: outPutObservableCollection = TestDrink(inputObservableCollection);
                    break;
                case 5: outPutObservableCollection = TestOther(inputObservableCollection);
                    break;
                case 6: outPutObservableCollection = TestShot(inputObservableCollection);
                    break;
                case 7: outPutObservableCollection = TestSoda(inputObservableCollection);
                    break;
            } 

            return outPutObservableCollection;
        }

        private ObservableCollection<Product> TestBeer(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryBeer)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestBottle(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryBottle)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestCider(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryCider)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestDrink(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryDrink)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestOther(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryOther)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestShot(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategoryShot)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
        }
        private ObservableCollection<Product> TestSoda(ObservableCollection<Product> inputObservableCollection)
        {
            ObservableCollection<Product> outPutObservableCollection = new ObservableCollection<Product>();
            foreach (string category in Enum.GetNames(typeof(EnumCategory.ProductCategorySoda)))
            {
                foreach (var product in inputObservableCollection)
                {
                    if (product.CategoryBeer.ToString() == category)
                    {
                        outPutObservableCollection.Add(product);
                    }
                }
            }

            return outPutObservableCollection;
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
                    new Product(2, 5, "Tuborg Classic", 48, 0, "ProductImages/TuborgClassic.png", ColorGreen, EnumCategory.ProductCategoryBeer.Beer),
                    new Product(2, 5, "Grøn Tuborg", 48, 0, "ProductImages/GrønTuborg.png", ColorGreen, EnumCategory.ProductCategoryBeer.Beer),
                    new Product(2, 5, "Tuborg Gylden Dame", 48, 0, "ProductImages/TuborgGuldDame.png", ColorGreen, EnumCategory.ProductCategoryBeer.Beer),
                    new Product(2, 5, "Carlsberg", 48, 0, "ProductImages/Carlsberg.png", ColorGreen, EnumCategory.ProductCategoryBeer.Beer),
                    new Product(2, 5, "Cola Zero", 48, 0, "ProductImages/ColaZero.png", ColorGreen, EnumCategory.ProductCategoryBeer.Soda),
                    new Product(2, 5, "Cola", 48, 0, "ProductImages/Cola.png", ColorGreen, EnumCategory.ProductCategoryBeer.Soda),
                    new Product(2, 5, "Mokai", 48, 0, "ProductImages/Mokai.png", ColorGreen, EnumCategory.ProductCategoryBeer.Cider),
                    new Product(2, 5, "Mokai Jordbær Lime", 48, 0, "ProductImages/MokaiStrawberryLime.png", ColorGreen, EnumCategory.ProductCategoryBeer.Cider),
                    new Product(2, 5, "Somersby Apple Cider", 48, 0, "ProductImages/SomersbyApple.png", ColorGreen, EnumCategory.ProductCategoryBeer.Cider),
                    new Product(2, 5, "Somersby Pear Cider", 48, 0, "ProductImages/SomersbyPear.png", ColorGreen, EnumCategory.ProductCategoryBeer.Cider),
                    new Product(2, 5, "Breezer", 48, 0, "ProductImages/Breezer.png", ColorGreen, EnumCategory.ProductCategoryBeer.Drink),
                    new Product(2, 5, "Fanta", 48, 0, "ProductImages/Fanta.png", ColorGreen, EnumCategory.ProductCategoryBeer.Soda)
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

﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    public class Stock : INotifyPropertyChanged
    {
        #region Field
        private bool _showAddProductPopUp , _showChangeProductPopUp = false;

        private readonly Message _message;
        private Product _selectedProduct = new Product();
        private BrowseImages _browseImages = new BrowseImages();

        private ObservableCollection<Product> _products;

        public string _colorRed = "Red";
        public string _colorGreen = "ForestGreen";

        public int MinAmount = 10;

        private string _productPriceChangeTb;

        private string _addNameTb, _addBuyingPriceTb, _addSellingPriceTb, _addAmountTb, _addTypeTb, _addDiscountAtThisAmountTb, _addDiscountPricePerPieceTb;
        private string _addImageSourceTb = "";

        private string _changeNameTb,_changeBuyingPriceTb, _changeSellingPriceTb, _changeAmountTb, _changeTypeTb;
        private string _changeImageSourceTb = "";
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
             get => _changeTypeTb;
            set
             {
                 _changeTypeTb = value; 
                 OnPropertyChanged();
             }
        }

        public string AddDiscountAtThisAmountTb
        {
            get => _addDiscountAtThisAmountTb;
            set
            {
                _addDiscountAtThisAmountTb = value;
                OnPropertyChanged();
            }
        }

        public string AddDiscountPricePerPieceTb
        {
            get => _addDiscountPricePerPieceTb;
            set
            {
                _addDiscountPricePerPieceTb = value;
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

                 if (!int.TryParse(AddDiscountAtThisAmountTb, out int intDiscountAtThisAmountTb)) intDiscountAtThisAmountTb = 0;
                 if(!double.TryParse(AddDiscountPricePerPieceTb, out double doubleDiscountPricePerItemTb)) doubleDiscountPricePerItemTb = 0;

                 if (!productExist)
                 {
                      if (double.TryParse(AddBuyingPriceTb, out double doubleBuyingPriceTb) && double.TryParse(AddSellingPriceTb, out double doubleSellingPriceTb) && int.TryParse(AddAmountTb, out int intAmountTb))
                      {
                          if (doubleBuyingPriceTb > 0 && doubleSellingPriceTb > 0 && AddAmountTb != null && intAmountTb >= 0)
                          {
                              if (doubleDiscountPricePerItemTb > 0 && intDiscountAtThisAmountTb != 0 || doubleDiscountPricePerItemTb != 0 && intDiscountAtThisAmountTb > 0)
                              {
                                  
                                  if (string.IsNullOrEmpty(AddImageSourceTb))
                                  {
                                      if (intAmountTb < MinAmount)
                                      {
                                          switch (AddTypeTb)
                                          {
                                            case "Øl":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Beer, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Sodavand":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Soda, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Cider":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Cider, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Drink":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Drink, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Flaske":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Bottle, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Shot":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Shot, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Andet":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorRed, EnumCategory.ProductCategory.Other, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                          }
                                      }
                                      else
                                      { 
                                          switch (AddTypeTb)
                                          {
                                            case "Øl":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Beer, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Sodavand":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Soda, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Cider":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Cider, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Drink":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Drink, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Flaske":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Bottle, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Shot":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Shot, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Andet":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, "ProductImages/BlankDåse.png", ColorGreen, EnumCategory.ProductCategory.Other, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
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
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Beer, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Sodavand":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Soda, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Cider":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Cider, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Drink":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Drink, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Flaske":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Bottle, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Shot":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Shot, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Andet":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorRed, EnumCategory.ProductCategory.Other, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                          }
                                      }

                                      else
                                      {
                                          switch (AddTypeTb)
                                          {
                                            case "Øl":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Beer, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Sodavand":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Soda, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Cider":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Cider, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Drink":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Drink, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Flaske":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Bottle, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Shot":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Shot, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                            case "Andet":
                                                Products.Add(new Product(doubleBuyingPriceTb, doubleSellingPriceTb, AddNameTb, intAmountTb, 0, AddImageSourceTb, ColorGreen, EnumCategory.ProductCategory.Other, intDiscountAtThisAmountTb, doubleDiscountPricePerItemTb));
                                                break;
                                          }
                                      }
                                  }

                                  AddNameTb = AddBuyingPriceTb = AddSellingPriceTb = AddAmountTb = AddImageSourceTb = AddTypeTb = AddDiscountAtThisAmountTb = AddDiscountPricePerPieceTb = null;

                                  ShowAddProductPopUp = false;
                              }
                              else await _message.Error("Forkert input", "En varer skal enten have tilbud eller ingen tilbud");
                          } 
                          else await _message.Error("Forkert input", "Købspris og Salgspris skal være mere en 0.");
                      }
                      else await _message.Error("Forkert input", "Købspris, Salgspris og Antal skal være tal.");
                 }
                 else await _message.Error("Varen findes allerede!", "Scroll igennem varerne, for at finde den.");
             }
             else await _message.Error("Forkert input", "Produktet skal have et navn.");
            Debug.WriteLine("Discount at this amount " + Products.Last().DiscountAtThisAmount + "\nDiscountPrice " + Products.Last().DiscountPricePerItem);
        }
        
        public void BrowseAddImageButton()
        {
             _browseImages.BrowseImageButton(AddImageSourceTb, "ProductImages/", ShowAddProductPopUp);
        }

        public void BrowseChangeImageButton()
        {
            _browseImages.BrowseImageButton(ChangeImageSourceTb, "ProductImages/", ShowChangeProductPopUp);
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
                 ChangeTypeTb = SelectedProduct.Category.ToString();
                
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
                      SelectedProduct.Category = EnumCategory.ProductCategory.Beer;
                      break;
                 case "Sodavand":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Soda;
                      break;
                 case "Cider":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Cider;
                      break;
                 case "Drink":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Drink;
                      break;
                 case "Flaske":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Bottle;
                      break;
                 case "Shot":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Shot;
                      break;
                 case "Andet":
                      SelectedProduct.Category = EnumCategory.ProductCategory.Other;
                      break;
             }

             ChangeNameTb = ChangeBuyingPriceTb = ChangeSellingPriceTb = ChangeAmountTb = ChangeImageSourceTb = ChangeTypeTb = null;  //Todo Combobox selected item bliver ikke placeholder text begrund af underlig binding

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
        #endregion

        #region Save/Load
        public async Task LoadAsync()
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
                      new Product(2, 5, "Tuborg Classic", 48, 0, "ProductImages/TuborgClassic.png", ColorGreen, EnumCategory.ProductCategory.Beer,3,3),
                      new Product(2, 5, "Grøn Tuborg", 48, 0, "ProductImages/GrønTuborg.png", ColorGreen, EnumCategory.ProductCategory.Beer,3,3),
                      new Product(2, 5, "Tuborg Gylden Dame", 48, 0, "ProductImages/TuborgGuldDame.png", ColorGreen, EnumCategory.ProductCategory.Beer,3,3),
                      new Product(2, 5, "Carlsberg", 48, 0, "ProductImages/Carlsberg.png", ColorGreen, EnumCategory.ProductCategory.Beer,3,3),
                      new Product(2, 5, "Cola Zero", 48, 0, "ProductImages/ColaZero.png", ColorGreen, EnumCategory.ProductCategory.Soda,3,3),
                      new Product(2, 5, "Cola", 48, 0, "ProductImages/Cola.png", ColorGreen, EnumCategory.ProductCategory.Soda,3,3),
                      new Product(2, 5, "Mokai", 48, 0, "ProductImages/Mokai.png", ColorGreen, EnumCategory.ProductCategory.Cider,3,3),
                      new Product(2, 5, "Mokai Jordbær Lime", 48, 0, "ProductImages/MokaiStrawberryLime.png", ColorGreen, EnumCategory.ProductCategory.Cider,3,3),
                      new Product(2, 5, "Somersby Apple Cider", 48, 0, "ProductImages/SomersbyApple.png", ColorGreen, EnumCategory.ProductCategory.Cider,3,3),
                      new Product(2, 5, "Somersby Pear Cider", 48, 0, "ProductImages/SomersbyPear.png", ColorGreen, EnumCategory.ProductCategory.Cider,3,3),
                      new Product(2, 5, "Breezer", 48, 0, "ProductImages/Breezer.png", ColorGreen, EnumCategory.ProductCategory.Drink,3,3),
                      new Product(2, 5, "Fanta", 48, 0, "ProductImages/Fanta.png", ColorGreen, EnumCategory.ProductCategory.Soda,3,3)
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

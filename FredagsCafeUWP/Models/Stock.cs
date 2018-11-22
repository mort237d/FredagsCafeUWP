using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using Windows.UI.Popups;
using FredagsCafeUWP.Annotations;

namespace FredagsCafeUWP.Models
{
    class Stock : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value; 
                
            }
        }

        public Stock()
        {
            Products = new ObservableCollection<Product>()
            {
                new Product(66, 67, "Tuborg Classic", 22, 2, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Grøn Tuborg", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Tuborg Gylden Dame", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Carlsberg", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Mokai", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Breezer", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Sommersby Apple Cider", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Sommersby Pear Cider", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Cola", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Cola Zero", 2, 13, "ProductImages/Tuborg-Dåse.png"),
                new Product(55, 63, "Squash", 2, 13, "ProductImages/Tuborg-Dåse.png")
            };

            //WriteListToTxt();

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

        public void AddProductToOBList(Product product)
        {
            bool productExist = false;
            foreach (var element in Products)
            {
                if (!element.Name.ToLower().Equals(product.Name.ToLower()))
                {
                    productExist = true;
                    break;
                }
            }

            if (productExist) Products.Add(product);
            else Debug.WriteLine("Varen findes allerede");
        }

        public void RemoveProductFromOBList(Product product)
        {
            Products.Remove(product);
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

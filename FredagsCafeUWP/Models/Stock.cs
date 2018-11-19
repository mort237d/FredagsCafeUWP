using System.Collections.ObjectModel;

namespace FredagsCafeUWP.Models
{
    class Stock
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        public Stock()
        {
            Products = new ObservableCollection<Product>();
        }

        public void AddProductToOBList(Product product)
        {
            Products.Add(product);
        }
    }
}

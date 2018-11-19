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
            Products.Add(new Product(66,67,"Tuborg", 22, 2));
            Products.Add(new Product(55, 63, "Cola", 2, 13));

        }

        public void AddProductToOBList(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProductFromOBList(Product product)
        {
            Products.Remove(product);
        }
    }
}

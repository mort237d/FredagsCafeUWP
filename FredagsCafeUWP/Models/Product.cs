namespace FredagsCafeUWP.Models
{
    class Product
    {
        #region Fields
        private double _buyingPrice;
        private double _sellingPrice;
        private string _name;
        private int _amount;
        private int _amountSold;
        #endregion

        #region Props
        public double BuyingPrice
        {
            get { return _buyingPrice; }
            set { _buyingPrice = value; }
        }

        public double SellingPrice
        {
            get { return _sellingPrice; }
            set { _sellingPrice = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int AmountSold
        {
            get { return _amountSold; }
            set { _amountSold = value; }
        }

        #endregion

        public Product(double buyingPrice, double sellingPrice, string name, int amount, int amountSold)
        {
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
            Name = name;
            Amount = amount;
            AmountSold = amountSold;
        }
    }
}

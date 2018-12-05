using FredagsCafeUWP.Models.AddProduct;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    public class AddProductViewModel
    {
        private AddProductClass _addProductClass = new AddProductClass();

        private RelayCommand _addProductCommand;

        private RelayCommand _browseImageCommand;

        public AddProductClass AddProductClass
        {
            get { return _addProductClass; }
            set { _addProductClass = value; }
        }

        public RelayCommand AddProductCommand
        {
            get { return _addProductCommand; }
            set { _addProductCommand = value; }
        }

        public RelayCommand BrowseImageCommand
        {
            get { return _browseImageCommand; }
            set { _browseImageCommand = value; }
        }


        public AddProductViewModel()
        {
            //AddProductCommand = new RelayCommand(AddProductClass.AddProductToObList);

            //BrowseImageCommand = new RelayCommand(AddProductClass.BrowseAddImageButton);
        }
    }
}

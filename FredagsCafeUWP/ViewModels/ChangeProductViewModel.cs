using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FredagsCafeUWP.Models.AddProduct_ChangeProduct;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    class ChangeProductViewModel
    {
        private ChangeProductClass _changeProductClass = new ChangeProductClass();

        private RelayCommand _changeProductCommand;

        private RelayCommand _browseImageCommand;

        public ChangeProductClass ChangeProductClass
        {
            get { return _changeProductClass; }
            set { _changeProductClass = value; }
        }

        public RelayCommand ChangeProductCommand
        {
            get { return _changeProductCommand; }
            set { _changeProductCommand = value; }
        }

        public RelayCommand BrowseImageCommand
        {
            get { return _browseImageCommand; }
            set { _browseImageCommand = value; }
        }

        public ChangeProductViewModel()
        {
            ChangeProductCommand = new RelayCommand(ChangeProductClass.ChangeProductOfObList);

            BrowseImageCommand = new RelayCommand(ChangeProductClass.BrowseImageButton);
        }
    }
}

using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginViewModel
    {
        private Help _help = new Help();

        public RelayCommand LoginRelayCommand { get; set; }

        public RelayCommand GoToHelpPageCommand { get; set; }

        public LoginViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.Instance.CheckLogin);

            GoToHelpPageCommand = new RelayCommand(_help.GoToHelpPage);
        }
    }
}

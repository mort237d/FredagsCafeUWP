using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginPageViewModel
    {
        public RelayCommand LoginRelayCommand { get; set; }

        public LoginPageViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.Instance.CheckLogin);
        }
    }
}

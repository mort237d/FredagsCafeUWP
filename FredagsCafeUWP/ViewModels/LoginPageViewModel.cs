using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginPageViewModel
    {
        public RelayCommand LoginRelayCommand { get; set; }

        public RelayCommand RecoverAccountCommand { get; set; }

        public LoginPageViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.Instance.CheckLogin);

            //RecoverAccountCommand = new RelayCommand();
        }
    }
}

using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginViewModel
    {
        public RelayCommand LoginRelayCommand { get; set; }

        public RelayCommand RecoverAccountCommand { get; set; }

        public LoginViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.Instance.CheckLogin);

            //RecoverAccountCommand = new RelayCommand();
        }
    }
}

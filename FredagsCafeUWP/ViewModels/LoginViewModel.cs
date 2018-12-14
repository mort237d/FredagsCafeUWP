using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginViewModel
    {
        public LogOnLogOff LogOnLogOff { get; set; } = LogOnLogOff.Instance;
        public Help Help { get; set; } = new Help();

        public RelayCommand LoginRelayCommand { get; set; }

        public RelayCommand GoToHelpPageCommand { get; set; }

        public LoginViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.Instance.CheckLogin);

            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);
        }
    }
}

using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
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

            Windows.UI.Xaml.Window.Current.CoreWindow.KeyDown += (sender, arg) => {if (arg.VirtualKey == Windows.System.VirtualKey.Enter) LogOnLogOff.Instance.CheckLogin();};
        }
    }
}

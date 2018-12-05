using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using FredagsCafeUWP.Models;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP.ViewModels
{
    class AccountViewModel : INotifyPropertyChanged
    {
        private Administration _administration;
        private AccountSettingsClass _accountSettingsClass = new AccountSettingsClass();
        private LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
        private User _user = new User();
        private Help _help = new Help();

        private RelayCommand _logOffCommand;

        private RelayCommand _goToAccountCommand;

        private RelayCommand _changeSettingsCommand;

        private RelayCommand _goToHelpPageCommand;

        public AccountViewModel()
        {
            ChangeSettingsCommand = new RelayCommand(SettingsClass.ChangeSettings);
            LogOffCommand = new RelayCommand(OnLogOff.logOffMethod);
            GoToAccountCommand = new RelayCommand(SettingsClass.GoToAccountSettings);
            GoToHelpPageCommand = new RelayCommand(Help.GoToHelpPage);
        }

        public RelayCommand ChangeSettingsCommand
        {
            get { return _changeSettingsCommand; }
            set { _changeSettingsCommand = value; }
        }

        public AccountSettingsClass SettingsClass
        {
            get { return _accountSettingsClass; }
            set { _accountSettingsClass = value; }
        }

        public RelayCommand LogOffCommand
        {
            get { return _logOffCommand; }
            set { _logOffCommand = value; }
        }

        public LogOnLogOff OnLogOff
        {
            get { return _logOnLogOff; }
            set { _logOnLogOff = value; }
        }

        public RelayCommand GoToAccountCommand
        {
            get { return _goToAccountCommand; }
            set { _goToAccountCommand = value; }
        }

        public RelayCommand GoToHelpPageCommand
        {
            get { return _goToHelpPageCommand; }
            set { _goToHelpPageCommand = value; }
        }

        public Help Help
        {
            get { return _help; }
            set { _help = value; }
        }

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}


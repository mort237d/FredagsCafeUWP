using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private RelayCommand _logOffCommand;

        private RelayCommand _goToAccountCommand;

        private RelayCommand _changeSettingsCommand;

        public AccountViewModel()
        {
            ChangeSettingsCommand = new RelayCommand(SettingsClass.ChangeSettings);
            LogOffCommand = new RelayCommand(OnLogOff.logOffMethod);
            GoToAccountCommand = new RelayCommand(SettingsClass.GoToAccountSettings);
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


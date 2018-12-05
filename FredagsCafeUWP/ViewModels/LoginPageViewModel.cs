using System.ComponentModel;
using System.Runtime.CompilerServices;
using FredagsCafeUWP.Annotations;
using GalaSoft.MvvmLight.Command;

namespace FredagsCafeUWP
{
    internal class LoginPageViewModel : INotifyPropertyChanged
    {

        #region Field

        LogOnLogOff _logOnLogOff = LogOnLogOff.Instance;
      
       
        public RelayCommand LoginRelayCommand { get; set; }

        public LogOnLogOff LogOnLogOff
        {
            get { return _logOnLogOff; }
            set { _logOnLogOff = value; }
        }

        #endregion

        public LoginPageViewModel()
        {
            LoginRelayCommand = new RelayCommand(LogOnLogOff.CheckLogin);
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

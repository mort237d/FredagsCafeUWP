using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FredagsCafeUWP
{
    public class AccountSettingsClass
    {
        public void GoToAccountSettings()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(accountSettings));
        }
    }
}

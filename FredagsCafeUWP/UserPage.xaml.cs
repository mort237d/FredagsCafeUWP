using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace FredagsCafeUWP
{
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            this.InitializeComponent();

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ForegroundColor = Windows.UI.Colors.White;
            titleBar.BackgroundColor = Windows.UI.Color.FromArgb(1,108,160,220);
            titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(1, 108, 160, 220);
        }
    }
}

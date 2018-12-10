using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FredagsCafeUWP
{
    public sealed partial class MySplashScreen : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        private double ScaleFactor;
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;

        public MySplashScreen(SplashScreen splashScreen, bool loadState)
        {
            this.InitializeComponent();
            DismissExtendedSplash();
            Window.Current.SizeChanged += Current_SizeChanged;
            ScaleFactor = (double) DisplayInformation.GetForCurrentView().ResolutionScale / 100;
            splash = splashScreen;
            if (splashScreen != null)
            {
                splash.Dismissed += Splash_Dismissed;
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }

            //RestoreStateAsync(loadState);
        }

        //private async void RestoreStateAsync(bool loadState)
        //{
        //    if (loadState)
        //    {
        //        await SuspensionManager.RestoreAsync();
        //    }
        //}

        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.Left);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Top);
            extendedSplashImage.Width = splashImageRect.Width / ScaleFactor;
            extendedSplashImage.Height = splashImageRect.Height / ScaleFactor;
        }

        private void Splash_Dismissed(SplashScreen sender, object args)
        {
            dismissed = true;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }

        private async void DismissExtendedSplash()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            rootFrame = new Frame();
            UserPage userPage = new UserPage();
            rootFrame.Content = userPage;
            Window.Current.Content = rootFrame;
            rootFrame.Navigate(typeof(UserPage));
        }
    }
}

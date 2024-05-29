using System;
using System.Drawing;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel;

using OkayuLoader.Pages;
using System.Runtime.InteropServices;

namespace OkayuLoader
{
    public sealed partial class MainWindow : Window
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public const int DESKTOPVERTRES = 117;
        public const int DESKTOPHORZRES = 118;

        public MainWindow()
        {
            this.InitializeComponent();

            TitleBarTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
            ExtendsContentIntoTitleBar = true;

            IntPtr screenDC = GetDC(IntPtr.Zero);
            int width = GetDeviceCaps(screenDC, DESKTOPHORZRES);
            int height = GetDeviceCaps(screenDC, DESKTOPVERTRES);

            AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 1000, Height = 600 });
            AppWindow.Move(new Windows.Graphics.PointInt32 { X = (width / 2 - 500), Y = (height / 2 - 300) });
        }

        private void NavigationViewInit(object sender, RoutedEventArgs args)
        {
            nvMain.SelectedItem = nvMain.MenuItems[0];
            Type pageType = typeof(HomePage);
            _ = contentFrame.Navigate(pageType);
        }

        private void NavigationViewBehavior(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }
            Type pageType = typeof(HomePage);

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == nv_home.Name)
            {
                pageType = typeof(HomePage);
            }
            else if (selectedItem.Name == nv_accounts.Name)
            {
                pageType = typeof(AccountsPage);
            }
            else if (selectedItem.Name == nv_settings.Name)
            {
                pageType = typeof(SettingsPage);
            }

            _ = contentFrame.Navigate(pageType);
        }
    }
}

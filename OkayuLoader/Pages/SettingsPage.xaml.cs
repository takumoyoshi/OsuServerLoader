using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using OkayuLoader.Services;
using System;
using System.Threading.Tasks;

namespace OkayuLoader.Pages
{
    public sealed partial class SettingsPage : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public SettingsPage()
        {
            this.InitializeComponent();

            uiConfig = configService.ConfigLoad();
            TextBoxPath.Text = uiConfig.customPath;
            CheckBoxDialog.IsChecked = uiConfig.showBuyMsgAgain;

            allInitializated = true;
        }

        private void TextBoxPathHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.customPath = TextBoxPath.Text;
                configService.ConfigSave(uiConfig);
            }
        }

        private void CheckBoxDialogHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.showBuyMsgAgain = (bool)CheckBoxDialog.IsChecked;
                configService.ConfigSave(uiConfig);
            }
        }

        private async void ButtonResetConfigHandler(object sender, RoutedEventArgs e)
        {
            uiConfig.configVersion = 0;
            configService.ConfigSave(uiConfig);

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Config reseted!";
            dialog.PrimaryButtonText = "Exit";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "Okayu Loader must to be restarted!";
            var dialogResultButton = await dialog.ShowAsync();

            await Task.Delay(250);
            System.Environment.Exit(0);
        }
    }
}

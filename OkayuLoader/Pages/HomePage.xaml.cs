using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

using Windows.Storage;

using OkayuLoader.Services;

namespace OkayuLoader.Pages
{
    public sealed partial class HomePage : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public HomePage()
        {            
            this.InitializeComponent();

            uiConfig = configService.ConfigLoad();
            ComboBoxServerList.SelectedIndex = uiConfig.serverIndex;
            ToggleSwitchPatcher.IsOn = uiConfig.isPatcherEnabled;

            if (ComboBoxServerList.SelectedIndex == 0)
            {
                SettingsCardPatcher.IsEnabled = false;
                InfoBarSecurity.IsOpen = true;
            } else
            {
                SettingsCardPatcher.IsEnabled = true;
                InfoBarSecurity.IsOpen = false;
            }

            allInitializated = true;
        }

        private void ComboBoxServerListHandler(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxServerList.SelectedIndex == 0 & allInitializated)
            {
                SettingsCardPatcher.IsEnabled = false;
                ToggleSwitchPatcher.IsOn = false;
                InfoBarSecurity.IsOpen = true;
            } 
            else if (ComboBoxServerList.SelectedIndex > 0 & allInitializated)
            {
                SettingsCardPatcher.IsEnabled = true;
                InfoBarSecurity.IsOpen = false;
            }

            if (allInitializated)
            {
                uiConfig.serverIndex = ComboBoxServerList.SelectedIndex;
                configService.ConfigSave(uiConfig);
            }
        }

        private void ToggleSwitchPatcherHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                if (ToggleSwitchPatcher.IsOn == true) uiConfig.isPatcherEnabled = true;
                if (ToggleSwitchPatcher.IsOn == false) uiConfig.isPatcherEnabled = false;
                configService.ConfigSave(uiConfig);
            }
        }

        private async void ButtonPlayClickHandler(object sender, RoutedEventArgs e)
        {
            PlayOsuButton.Content = "Starting osu!...";
            PlayOsuButton.IsEnabled = false;
            string osuFolderPath;
            if (uiConfig.customPath != "")
            {
                osuFolderPath = uiConfig.customPath;
            } else
            {
                osuFolderPath = Environment.ExpandEnvironmentVariables("%localappdata%\\osu!");
            }

            await Task.Delay(1000);

            var osuProcessHandler = new Process();
            osuProcessHandler.StartInfo.FileName = osuFolderPath + "\\osu!.exe";
            osuProcessHandler.StartInfo.Arguments = GlobalVars.serverDevFlags[ComboBoxServerList.SelectedIndex];
            osuProcessHandler.Start();

            if (ToggleSwitchPatcher.IsOn == true)
            {
                await Task.Delay(1000);
                var patcherProcessHandler = new Process();
                patcherProcessHandler.StartInfo.FileName = osuFolderPath + "\\Patcher\\osu!.patcher.exe";
                patcherProcessHandler.Start();
            }

            if (uiConfig.showBuyMsgAgain)
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.XamlRoot;
                dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
                dialog.Title = "Okayu Loader finished its work!";
                dialog.PrimaryButtonText = "OK";
                dialog.SecondaryButtonText = "Don't show this window again";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = "You can close this window by clicking \"Ok\" button. Osu! will be running without loader. Thank you for using my loader!";
                var dialogResultButton = await dialog.ShowAsync();

                if (dialogResultButton == ContentDialogResult.Secondary)
                {
                    uiConfig.showBuyMsgAgain = false;
                    configService.ConfigSave(uiConfig);
                }
            }
            
            await Task.Delay(250);
            System.Environment.Exit(0);
        }
    }
}

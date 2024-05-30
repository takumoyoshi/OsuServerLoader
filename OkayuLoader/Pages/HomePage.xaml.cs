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
            TextBoxCustomServer.Text = uiConfig.customServer;
            ToggleSwitchCustomServer.IsOn = uiConfig.useCustomServer;
            SettingsCardCustomServer.IsEnabled = uiConfig.useCustomServer;

            if (ComboBoxServerList.SelectedIndex == 0)
            {
                SettingsCardPatcher.IsEnabled = false;
                InfoBarSecurity.IsOpen = true;
            } 
            else
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

        private void ToggleSwitchCustomServerHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                if (ToggleSwitchCustomServer.IsOn == true)
                {
                    uiConfig.useCustomServer = true;
                    SettingsCardCustomServer.IsEnabled = true;
                }
                if (ToggleSwitchCustomServer.IsOn == false) 
                { 
                    uiConfig.useCustomServer = false;
                    SettingsCardCustomServer.IsEnabled = false;
                }
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

        private void TextBoxCustomServerHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.customServer = TextBoxCustomServer.Text;
                configService.ConfigSave(uiConfig);
            }
        }

        private async void ButtonPlayClickHandler(object sender, RoutedEventArgs e)
        {
            PlayOsuButton.Content = "Starting osu!...";
            PlayOsuButton.IsEnabled = false;
            string osuFolderPath;
            string devserverFlag;

            if (uiConfig.customPath != "")
            {
                osuFolderPath = uiConfig.customPath;
            } 
            else
            {
                osuFolderPath = Environment.ExpandEnvironmentVariables("%localappdata%\\osu!");
            }
            if (uiConfig.customServer != "" & uiConfig.useCustomServer)
            {
                devserverFlag = "-devserver " + uiConfig.customServer;
            } 
            else
            {
                devserverFlag = GlobalVars.serverDevFlags[ComboBoxServerList.SelectedIndex];
            }

            await Task.Delay(1000);

            var osuProcessHandler = new Process();
            osuProcessHandler.StartInfo.FileName = osuFolderPath + "\\osu!.exe";
            osuProcessHandler.StartInfo.Arguments = devserverFlag;
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

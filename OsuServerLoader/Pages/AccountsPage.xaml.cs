using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OsuServerLoader.Services;
using OsuServerLoader.Classes;
using System.Collections.Generic;
using Windows.UI.Notifications;

namespace OsuServerLoader.Pages
{
    public sealed partial class AccountsPage : Page
    {
        private bool allInitializated = false;

        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        DataService dataService = new DataService();
        List<Account> accounts = new List<Account>();

        public AccountsPage()
        {
            this.InitializeComponent();

            uiConfig = configService.Load();
            accounts = dataService.LoadAccounts();

            foreach (Account account in accounts)
            {
                Classes.ComboBoxItem comboBoxItem = new Classes.ComboBoxItem();
                comboBoxItem.Text = account.tag;
                ComboBoxAccount.Items.Add(comboBoxItem);
            }

            ToggleSwitchAccount.IsOn = uiConfig.useCustomAccount;
            ComboBoxAccount.SelectedIndex = uiConfig.accountIndex;

            if (accounts.Count > 0)
            {
                SettingsCardDelete.IsEnabled = true;

                uiConfig.selectedAccountTag = ComboBoxAccount.Items[uiConfig.accountIndex].ToString();
                configService.Save(uiConfig);
            }
            else { SettingsCardDelete.IsEnabled = false; }

            if (uiConfig.useCustomAccount) { SettingsCardCustomAccount.IsEnabled = true; }
            else { SettingsCardCustomAccount.IsEnabled = false; }

            if ((uiConfig.customAccountTag.Length > 20 || uiConfig.customAccountName.Length > 20 || uiConfig.customAccountPassword.Length > 20) & uiConfig.showErrorAccount == true)
            {
                InfoBarAccount.Title = "Value is too long!";
                InfoBarAccount.Message = "Please, input value shorter than 20 symbols!";
                InfoBarAccount.IsOpen = true;
                uiConfig.showErrorAccount = false;
            }
            if ((uiConfig.customAccountTag.Length == 0 || uiConfig.customAccountName.Length == 0 || uiConfig.customAccountPassword.Length == 0) & uiConfig.showErrorAccount == true)
            {
                InfoBarAccount.Title = "Value is empty!";
                InfoBarAccount.Message = "Please, input some data in the text boxes!";
                InfoBarAccount.IsOpen = true;
                uiConfig.showErrorAccount = false;
            }
            configService.Save(uiConfig);

            allInitializated = true;
        }

        private void ComboBoxAccountListHandler(object sender, SelectionChangedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.accountIndex = ComboBoxAccount.SelectedIndex;
                uiConfig.selectedAccountTag = ComboBoxAccount.Items[uiConfig.accountIndex].ToString();
                configService.Save(uiConfig);
            }  
        }

        private void ToggleSwitchCustomAccountHandler(object sender, RoutedEventArgs e)
        {
                if (ToggleSwitchAccount.IsOn == true)
                {
                    uiConfig.useCustomAccount = true;
                    SettingsCardCustomAccount.IsEnabled = true;
                }
                else
                {
                    uiConfig.useCustomAccount = false;
                    SettingsCardCustomAccount.IsEnabled = false;
                }
            configService.Save(uiConfig);
        }

        private async void ButtonDeleteAccountHandler(object sender, RoutedEventArgs e)
        {
            dataService.DeleteRow(ComboBoxAccount.Items[uiConfig.accountIndex].ToString());

            uiConfig.accountIndex = 0;
            configService.Save(uiConfig);

            this.Frame.Navigate(typeof(AccountsPage));
        }

        private async void ButtonAddAccountHandler(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Add new account";
            dialog.PrimaryButtonText = "Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AccountsDialogContent();
            var dialogResultButton = await dialog.ShowAsync();

            if (dialogResultButton == ContentDialogResult.Primary)
            {
                uiConfig = configService.Load();
                bool dontDo = false;

                if (uiConfig.customAccountTag.Length > 20 || uiConfig.customAccountName.Length > 20 || uiConfig.customAccountPassword.Length > 20) 
                { 
                    dontDo = true;
                    uiConfig.showErrorAccount = true;
                }
                if (uiConfig.customAccountTag.Length == 0 || uiConfig.customAccountName.Length == 0 || uiConfig.customAccountPassword.Length == 0) 
                { 
                    dontDo = true;
                    uiConfig.showErrorAccount = true;
                }

                if (!dontDo)
                {
                    Services.Account account = new Account();
                    account.id = uiConfig.accountId;
                    account.tag = uiConfig.customAccountTag;
                    account.name = uiConfig.customAccountName;
                    account.password = uiConfig.customAccountPassword;
                    dataService.AddRow(account);

                    uiConfig.accountId = uiConfig.accountId + 1;
                }

                configService.Save(uiConfig);
                this.Frame.Navigate(typeof(AccountsPage));
            }
        }
    }
}

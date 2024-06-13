using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OsuServerLoader.Services;

namespace OsuServerLoader.Pages
{
    public sealed partial class AccountsDialogContent : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public AccountsDialogContent()
        {
            this.InitializeComponent();

            uiConfig = configService.Load();

            allInitializated = true;
        }

        private void TextBoxAccountTagHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.customAccountTag = TextBoxAccountTag.Text.ToString();
                configService.Save(uiConfig);
            }
        }

        private void TextBoxAccountNameHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.customAccountName = TextBoxAccountName.Text.ToString();
                configService.Save(uiConfig);
            }
        }

        private void PasswordBoxAccountPasswordHandler(object sender, RoutedEventArgs e)
        {
            if (allInitializated)
            {
                uiConfig.customAccountPassword = PasswordBoxAccountPassword.Password.ToString();
                configService.Save(uiConfig);
            }
        }
    }
}

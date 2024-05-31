using Microsoft.UI.Xaml.Controls;

using OkayuLoader.Services;

namespace OkayuLoader.Pages
{
    public sealed partial class AccountsPage : Page
    {
        private bool allInitializated = false;
        ConfigService configService = new ConfigService();
        Services.UiSettings uiConfig;

        public AccountsPage()
        {
            this.InitializeComponent();

            uiConfig = configService.ConfigLoad();
        }
    }
}

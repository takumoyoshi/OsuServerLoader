using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using OkayuLoader.Services;

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
    }
}

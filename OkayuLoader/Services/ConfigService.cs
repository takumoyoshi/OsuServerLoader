using Microsoft.UI.Xaml.Shapes;
using System;
using System.IO;
using System.Text.Json;

namespace OkayuLoader.Services
{
    public class UiSettings
    {
        public int serverIndex { get; set; }
        public string customPath { get; set; }
        public string customServer { get; set; }
        public bool isPatcherEnabled { get; set; }
        public bool showBuyMsgAgain { get; set; }
        public int configVersion { get; set; }
    }

    public class ConfigService
    {
        const int reqVerisonConfig = 2;

        public void ConfigCreate()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFolder = System.IO.Path.Combine(userFolderPath, ".OkayuLoader");
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            var uiSettings = new UiSettings
            {
                serverIndex = 0,
                customPath = "",
                customServer = "",
                isPatcherEnabled = false,
                showBuyMsgAgain = true,
                configVersion = reqVerisonConfig,
            };
            string jsonConfig = JsonSerializer.Serialize<UiSettings>(uiSettings);

            Directory.CreateDirectory(pathConfigFolder);
            File.WriteAllText(pathConfigFile, jsonConfig);
        }

        public UiSettings ConfigLoad() 
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            bool fileExists = File.Exists(pathConfigFile);
            if (fileExists == false)
            {
                ConfigCreate();
            }

            var config = JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            if (config.configVersion < reqVerisonConfig)
            {
                ConfigCreate();
                return JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            }
            return config;
        }

        public void ConfigSave(UiSettings currentSettings)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            File.Delete(pathConfigFile);
            string jsonConfig = JsonSerializer.Serialize(currentSettings);
            File.WriteAllText(pathConfigFile, jsonConfig);
        }
    }
}

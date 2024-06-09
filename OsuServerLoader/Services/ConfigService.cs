using System;
using System.IO;
using System.Text.Json;

namespace OkayuLoader.Services
{

    public class UiSettings
    {
        public int serverIndex { get; set; }
        public int accountIndex { get; set; }
        public int accountId { get; set; }
        public string customPath { get; set; }
        public string customServer { get; set; }
        public string selectedAccountTag { get; set; }
        public string customAccountTag { get; set; }
        public string customAccountName { get; set; }
        public string customAccountPassword { get; set; }
        public bool isPatcherEnabled { get; set; }
        public bool showBuyMsgAgain { get; set; }
        public bool useCustomServer { get; set; }
        public bool useCustomAccount { get; set; }
        public bool showErrorAccount { get; set; }
        public int configVersion { get; set; }
    }

    public class ConfigService
    {
        const int reqVerisonConfig = 8;
        DataService dataService = new DataService();

        public void CreateConfigFile()
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFolder = System.IO.Path.Combine(userFolderPath, ".OkayuLoader");
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            if (Directory.Exists(pathConfigFolder))
            {
                Directory.Delete(pathConfigFolder, true);
            }

            var uiSettings = new UiSettings
            {
                serverIndex = 0,
                accountIndex = 0,
                accountId = 0,
                customPath = "",
                customServer = "",
                selectedAccountTag = "",
                customAccountTag = "",
                customAccountName = "",
                customAccountPassword = "",
                isPatcherEnabled = false,
                showBuyMsgAgain = true,
                useCustomServer = false,
                useCustomAccount = false,
                showErrorAccount = false,
                configVersion = reqVerisonConfig
            };
            string jsonConfig = JsonSerializer.Serialize<UiSettings>(uiSettings);

            Directory.CreateDirectory(pathConfigFolder);
            File.WriteAllText(pathConfigFile, jsonConfig);
            dataService.CreateDataFile();
        }

        public UiSettings Load() 
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            bool fileExists = File.Exists(pathConfigFile);
            if (fileExists == false)
            {
                CreateConfigFile();
            }

            var config = JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            if (config.configVersion < reqVerisonConfig)
            {
                CreateConfigFile();
                return JsonSerializer.Deserialize<UiSettings>(File.ReadAllText(pathConfigFile));
            }
            return config;
        }

        public void Save(UiSettings currentSettings)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathConfigFile = System.IO.Path.Combine(userFolderPath, ".OkayuLoader\\UiSettings.cfg");

            File.Delete(pathConfigFile);
            string jsonConfig = JsonSerializer.Serialize(currentSettings);
            File.WriteAllText(pathConfigFile, jsonConfig);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Core.Services
{
    public class SettingManager : ISettingManager
    {

        private readonly ILogger _logger;

        public SettingManager(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        private static readonly string GameConfigurationFolder = "./GameConfiguration";
        private static readonly string GameConfigurationsPath = Path.Combine(GameConfigurationFolder, "gameConfigs.json");

        public List<Setting> Settings { get; private set; }

        public async Task LoadAsync()
        {
            IEnumerable<Setting> settings = Array.Empty<Setting>();

            if (!Directory.Exists(GameConfigurationFolder))
                Directory.CreateDirectory(GameConfigurationFolder);

            if (File.Exists(GameConfigurationsPath))
            {
                var jsonLoadText = await File.ReadAllTextAsync(GameConfigurationsPath);
                settings = JsonConvert.DeserializeObject<List<Setting>>(jsonLoadText);
            }

            // Windows Only
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Loading the last used configurations for hammer
                var rk = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Hammer\General");
                if (rk != null)
                {
                    var binFolder = (string)rk.GetValue("Directory")!;

                    try
                    {
                        settings = SettingParser.Parse(binFolder);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "{Message}", e.Message);
                    }
                }
            }

            // Remove duplicates
            Settings = settings?.GroupBy(g => (g.Name, g.GameFolder)).Select(grp => grp.First()).ToList();

            await SaveAsync();

        }

        public async Task SaveAsync()
        {
            var jsonSaveText = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            await File.WriteAllTextAsync(GameConfigurationsPath, jsonSaveText);
        }

        public string SubstituteValues(Setting setting, string text, string mapFile)
        {
            if (setting == null) throw new ArgumentNullException("setting");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");
            if (string.IsNullOrWhiteSpace(mapFile)) throw new ArgumentNullException("mapFile");

            text = text.Replace("$vmfFile$", $"\"{mapFile}\"");
            text = text.Replace("$map$", $"\"{Path.GetFileNameWithoutExtension(mapFile)}\"");
            text = text.Replace("$bsp$", $"\"{Path.ChangeExtension(mapFile, "bsp")}\"");

            text = text.Replace("$mapCopyLocation$", $"\"{Path.Combine(setting.MapFolder, Path.ChangeExtension(Path.GetFileName(mapFile), "bsp"))}\"");

            text = text.Replace("$game$", $"\"{setting.GameFolder}\"");
            text = text.Replace("$gameEXE$", $"\"{setting.GameEXE}\"");
            text = text.Replace("$binFolder$", $"\"{setting.BinFolder}\"");
            text = text.Replace("$mapFolder$", $"\"{setting.MapFolder}\"");
            text = text.Replace("$gameName$", $"\"{setting.Name}\"");
            text = text.Replace("$sdkFolder$", $"\"{setting.SDKMapFolder}\"");

            text = text.Replace("$vbsp$", $"\"{setting.VBSP}\"");
            text = text.Replace("$vvis$", $"\"{setting.VVIS}\"");
            text = text.Replace("$vrad$", $"\"{setting.VRAD}\"");

            text = text.Replace("$bspZip$", $"\"{setting.BSPZip}\"");
            text = text.Replace("$vbspInfo$", $"\"{setting.VBSPInfo}\"");

            return text;
        }

    }
}

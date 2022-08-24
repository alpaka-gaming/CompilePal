using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CompilePalX.Compiling;

namespace CompilePalX {
    class GameConfigurationParser
    {
        public static List<GameConfiguration> Parse(string binFolder)
        {
            // prioritize hammer++ configs, fallback to hammer if it doesn't exist
            string filename = Path.Combine(binFolder, "hammerplusplus", "hammerplusplus_gameconfig.txt");
            if (!File.Exists(filename))
                filename = Path.Combine(binFolder, "GameConfig.txt");

            var gameInfos = new List<GameConfiguration>();

            var data = new KV.FileData(filename);
            foreach (KV.DataBlock gamedb in data.headnode.GetFirstByName(new[] { "\"Configs\"", "\"GameConfig.txt\"" })
                         .GetFirstByName("\"Games\"").subBlocks)
            {
                KV.DataBlock hdb = gamedb.GetFirstByName(new[] { "\"Hammer\"", "\"hammer\"" });

                CompilePalLogger.LogLineDebug($"Gamedb: {gamedb}");
                GameConfiguration game = new GameConfiguration
                {
                    Name = gamedb.name.Replace("\"", ""),
                    BinFolder = binFolder,
                    GameFolder = GetFullPath(gamedb.TryGetStringValue("GameDir"), binFolder),
                    GameEXE = GetFullPath(hdb.TryGetStringValue("GameExe"), binFolder),
                    SDKMapFolder = GetFullPath(hdb.TryGetStringValue("MapDir"), binFolder),
                    VBSP = GetFullPath(hdb.TryGetStringValue("BSP"), binFolder),
                    VVIS = GetFullPath(hdb.TryGetStringValue("Vis"), binFolder),
                    VRAD = GetFullPath(hdb.TryGetStringValue("Light"), binFolder),
                    MapFolder = GetFullPath(hdb.TryGetStringValue("BSPDir"), binFolder),
                    BSPZip = Path.Combine(binFolder, "bspzip.exe"),
                    VBSPInfo = Path.Combine(binFolder, "vbspinfo.exe"),
                    VPK = Path.Combine(binFolder, "vpk.exe"),
                };

                game.SteamAppID = GetSteamAppID(game);

                gameInfos.Add(game);
            }

            return gameInfos;
        }

        private static string GetFullPath(string line, string gameInfoDir)
        {
            if (!line.StartsWith("..") || !line.StartsWith(""))
                return line;

            string fullPath = Path.GetFullPath(Path.Combine(gameInfoDir, line));
            return fullPath;
        }

        private static int? GetSteamAppID(GameConfiguration config)
        {
            if (!File.Exists(config.GameInfoPath)) return null;

            foreach (var line in File.ReadLines(config.GameInfoPath))
            {
                // ignore commented out lines
                if (line.TrimStart().StartsWith("//") || string.IsNullOrWhiteSpace(line))
                    continue;

                if (!line.Contains("SteamAppId")) continue;

                // sometimes gameinfo contains tabs, replace with spaces and filter them out
                var splitLine = line.Replace('\t', ' ').Split(' ').Where(c => c != String.Empty).ToList();

                // bad format
                if (splitLine.Count < 2)
                    continue;

                Int32.TryParse(splitLine[1], out int appID);
                return appID;
            }

            return null;
        }
    }
}
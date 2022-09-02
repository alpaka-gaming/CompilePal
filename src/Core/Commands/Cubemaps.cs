using System;
using System.Diagnostics;
using System.Threading;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Commands
{
    public class Cubemaps : Generic
    {
        public Cubemaps(ILoggerFactory loggerFactory, ICompileContext compileContext) : base(loggerFactory, compileContext) { }

        private string bspFile;
        private string vbspInfo;
        public override void Run(CancellationToken cancellationToken)
        {
            // if (!CanRun())
            //     return;
            //
            // if (cancellationToken.IsCancellationRequested)
            //     return;
            //
            // vbspInfo = _configurationManager.CurrentSetting.VBSPInfo;
            // bspFile = _compileContext.CopyLocation;
            //
            // // listen for cancellations
            // cancellationToken.Register(() =>
            // {
            //     try
            //     {
            //         Cancel();
            //     }
            //     catch (InvalidOperationException) { }
            //     catch (Exception e)
            //     {
            //         _logger.LogError(e, "{Message}", e.Message);
            //     }
            // });
            //
            // try
            // {
            //     _logger.LogInformation("Cubemap Generator");
            //
            //     if (!File.Exists(_compileContext.CopyLocation))
            //         throw new FileNotFoundException();
            //
            //     var addtionalParameters = Regex.Replace(GetParameterString(), "\b-hidd3en\b", "");
            //     var hidden = GetParameterString().Contains("-hidden");
            //     FetchHDRLevels();
            //
            //     var mapname = Path.GetFileName(_compileContext.CopyLocation)?.Replace(".bsp", "");
            //
            //     var args = $"-steam -game \"{_configurationManager.CurrentSetting.GameFolder}\" -windowed -novid -nosound +mat_specular 0 %HDRevel% +map {mapname} -buildcubemaps {addtionalParameters}";
            //     if (hidden) args += " -noborder -x 4000 -y 2000";
            //
            //     if (HDR && LDR)
            //     {
            //         _logger.LogInformation("Map requires two sets of cubemaps");
            //
            //         if (cancellationToken.IsCancellationRequested) return;
            //         _logger.LogInformation("Compiling LDR cubemaps...");
            //         RunCubemaps(_configurationManager.CurrentSetting.GameEXE, args.Replace("%HDRevel%", "+mat_hdr_level 0"));
            //
            //         if (cancellationToken.IsCancellationRequested) return;
            //         _logger.LogInformation("Compiling HDR cubemaps...");
            //         RunCubemaps(_configurationManager.CurrentSetting.GameEXE, args.Replace("%HDRevel%", "+mat_hdr_level 2"));
            //     }
            //     else
            //     {
            //         if (cancellationToken.IsCancellationRequested) return;
            //         _logger.LogInformation("Map requires one set of cubemaps");
            //         _logger.LogInformation("Compiling cubemaps...");
            //         RunCubemaps(_configurationManager.CurrentSetting.GameEXE, args.Replace("%HDRevel%", ""));
            //     }
            //
            //     if (cancellationToken.IsCancellationRequested)
            //         return;
            //
            //     _logger.LogInformation("Cubemaps compiled");
            //
            // }
            // catch (FileNotFoundException)
            // {
            //     _logger.LogError("Could not find file: {CopyLocation}", _compileContext.CopyLocation);
            // }
            // catch (Exception e)
            // {
            //     _logger.LogError(e, "{Message}", e.Message);
            // }

        }
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        private bool HDR;
        private bool LDR;
        private void FetchHDRLevels()
        {
            // _logger.LogInformation("Detecting HDR levels...");
            // var startInfo = new ProcessStartInfo(vbspInfo, "\"" + bspFile + "\"") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true };
            //
            // Core.Process = new System.Diagnostics.Process { StartInfo = startInfo };
            // try
            // {
            //     Core.Process.Start();
            // }
            // catch (Exception e)
            // {
            //     _logger.LogError(e, "Failed to run executable: {Message}", e.Message);
            //     _logger.LogError("Could not read HDR levels, defaulting to one");
            //     return;
            // }
            //
            // var output = Core.Process.StandardOutput.ReadToEnd();
            //
            // if (Core.Process.ExitCode != 0)
            // {
            //     _logger.LogWarning("Could not read HDR levels, defaulting to one");
            // }
            // else
            // {
            //     var re = new Regex(@"^LDR\sworldlights\s+.*", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //     var LDRStats = re.Match(output).Value.Trim();
            //     re = new Regex(@"^HDR\sworldlights\s+.*", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //     var HDRStats = re.Match(output).Value.Trim();
            //     LDR = !LDRStats.Contains(" 0/");
            //     HDR = !HDRStats.Contains(" 0/");
            // }
        }

        private void RunCubemaps(string gameEXE, string args)
        {
            var startInfo = new ProcessStartInfo(gameEXE, args);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;

            // Core.Process = new System.Diagnostics.Process { StartInfo = startInfo };
            // Core.Process.Start();
            // Core.Process.WaitForExit();
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Commands
{
    public abstract class Generic : ICommand
    {
        private protected readonly ICompileContext _compileContext;
        private protected Dictionary<Preset, ObservableCollection<Config>> _presetDictionary = new();
        protected readonly ILogger _logger;

        public ICompileContext Context => _compileContext;
        public Generic(ILoggerFactory loggerFactory, ICompileContext compileContext)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _compileContext = compileContext;
            // Metadata = new Metadata();
            // Process = new System.Diagnostics.Process();
        }
        
        public string Name { get; set; }
        public async Task StartAsync()
        {
            Process.Start();
            await Process.WaitForExitAsync();
        }

        // public List<Exception> CompileExceptions { get; private set; } = new();
        public System.Diagnostics.Process Process { get; private protected set; }
        

        // public Metadata Metadata { get; private set; }
        //
        // //private string _parameterFolder = "./Parameters";
        //
        // public ObservableCollection<Config> ParameterList { get; private protected set; } = new();
        // public Dictionary<Preset, ObservableCollection<Config>> PresetDictionary => _presetDictionary;
        //
        //
        //
        // public string PresetFile => Metadata.Name + ".csv";
        //
        // public double Ordering => Metadata.Order;
        //
        // public bool DoRun
        // {
        //     get => Metadata.DoRun;
        //     set => Metadata.DoRun = value;
        // }
        // public string Name => Metadata.Name;
        // public string Description => Metadata.Description;
        // public string Warning => Metadata.Warning;
        // public bool SupportsBSP => Metadata.SupportsBSP;
        //
        // public virtual bool CanRun()
        // {
        //     if (_compileContext.Map.IsBSP && !SupportsBSP)
        //     {
        //         _logger.LogDebug("Map is BSP, skipping process {Name}", Name);
        //         return false;
        //     }
        //     return true;
        // }
        //
        public abstract void Run(CancellationToken cancellationToken);
        public abstract void Cancel();

        //
        // public virtual void Cancel()
        // {
        //     if (Process is null || Process.Id == 0 || Process.HasExited)
        //         return;
        //
        //     Process.Kill();
        //     _logger.LogCritical("Killed {Name}", Metadata.Name);
        // }
        //
        // public string GetParameterString()
        // {
        //     var parameters = Metadata.Arguments;
        //
        //     if (_configurationManager.CurrentPreset != null)
        //     {
        //         foreach (var parameter in PresetDictionary[_configurationManager.CurrentPreset])
        //         {
        //             parameters += parameter.Parameter;
        //
        //             if (parameter.CanHaveValue && !string.IsNullOrEmpty(parameter.Value))
        //             {
        //                 //Handle additional parameters in CUSTOM process
        //                 if (parameter.Name == "Run Program")
        //                 {
        //                     //Add args
        //                     parameters += " " + parameter.Value;
        //
        //                     //Read Ouput
        //                     if (parameter.ReadOutput)
        //                     {
        //                         parameters += " " + parameter.ReadOutput;
        //                     }
        //                 }
        //                 else
        //                     // protect filepaths in quotes, since they can contain -
        //                 if (parameter.ValueIsFile || parameter.Value2IsFile)
        //                 {
        //                     parameters += $" \"{parameter.Value}\"";
        //                 }
        //                 else
        //                 {
        //                     parameters += " " + parameter.Value;
        //                 }
        //             }
        //         }
        //     }
        //
        //     parameters += Metadata.BasisString;
        //
        //     return parameters;
        // }
        //
        // public override string ToString()
        // {
        //     return Metadata.Name;
        // }

    }
}

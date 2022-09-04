using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class CompileContext : ICompileContext, INotifyPropertyChanged
    {
        public IParameterManager ParameterManager { get; }
        public IPresetManager PresetManager { get; }

        private Map _currentMap;
        public Map CurrentMap
        {
            get => _currentMap;
            set
            {
                SetField(ref _currentMap, value);
            }
        }

        private Preset _currentPreset;
        public Preset CurrentPreset
        {
            get => _currentPreset;
            set
            {
                SetField(ref _currentPreset, value);
            }
        }
        
        private ICommand _currentCommand;
        public ICommand CurrentCommand
        {
            get => _currentCommand;
            set
            {
                SetField(ref _currentCommand, value);
            }
        }

        public ISettingManager SettingManager { get; }

        private readonly ILogger _logger;

        public CompileContext(ILoggerFactory loggerFactory, IPresetManager presetManager, IParameterManager parameterManager, ISettingManager settingManager)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            SettingManager = settingManager;
            PresetManager = presetManager;
            ParameterManager = parameterManager;
        }

        public string CopyLocation { get; set; }

        public void Compile(string mapFile, Action<Preset> preset)
        {
            Compile(new Map(mapFile), preset);
        }
        public void Compile(string mapFile)
        {
            Compile(new Map(mapFile));
        }

        public async void Compile(Map map, Action<Preset> preset)
        {
            var model = new Preset();
            preset?.Invoke(model);
            model.AdHoc = true;
            PresetManager.Add(model);
            PresetManager.Set(map.Name, model.Name);

            var executionPlan = buildExecutionPlan(map, model);
            var sw1 = Stopwatch.StartNew();
            foreach (var item in executionPlan)
            {
                CurrentCommand = item.Value;
                
                _logger.LogInformation("Executing {Key}", item.Key);
                var sw2 = Stopwatch.StartNew();
                //TODO: DO SOME WORK
                await item.Value.StartAsync();
                
                sw2.Stop();
            }
            sw1.Stop();

            throw new NotImplementedException();
        }
        public void Compile(Map map)
        {
            Compile(map, null);
        }

        private IDictionary<string, ICommand> buildExecutionPlan(Map map, Preset preset)
        {
            
            
            throw new NotImplementedException();
        }

        public event ProgressChangedEventHandler ProgressChanged;

        #region IDisposable


        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }
        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing) { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CompileContext()
        {
            Dispose(false);
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

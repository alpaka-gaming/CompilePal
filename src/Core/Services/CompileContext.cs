using System;
using System.Collections.Generic;
using System.Diagnostics;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class CompileContext : ICompileContext
    {
        public IParameterManager ParameterManager { get; }
        public IPresetManager PresetManager { get; }

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

        public void Compile(Map map, Action<Preset> preset)
        {
            var model = new Preset();
            preset?.Invoke(model);
            model.AdHoc = true;
            PresetManager.Add(model);
            PresetManager.Set(map.MapName, model.Name);

            var executionPlan = buildExecutionPlan(map, model);
            var sw1 = Stopwatch.StartNew();
            foreach (var item in executionPlan)
            {
                _logger.LogInformation("Executing {Key}", item.Key);
                var sw2 = Stopwatch.StartNew();
                //TODO: DO SOME WORK
                item.Value.Start();
                sw2.Stop();
            }
            sw1.Stop();

            throw new NotImplementedException();
        }
        private IDictionary<string, System.Diagnostics.Process> buildExecutionPlan(Map map, Preset preset)
        {
            throw new NotImplementedException();
        }

        public void Compile(Map map)
        {
            throw new NotImplementedException();
        }

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

    }
}

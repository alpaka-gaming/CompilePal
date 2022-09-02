using System;
using Core.Models;

namespace Core.Interfaces
{
    public interface ICompileContext : IDisposable
    {
        void Compile(string mapFile, Action<Preset> preset);
        void Compile(Map map, Action<Preset> preset);
        void Compile(string mapFile);
        void Compile(Map map);
        
        IParameterManager ParameterManager { get; }
        IPresetManager PresetManager { get; }
    }
}

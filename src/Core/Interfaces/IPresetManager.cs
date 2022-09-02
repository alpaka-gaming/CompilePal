using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IPresetManager
    {
        Dictionary<string, Preset> Items { get; }
        void Add(Preset model);
        void Set(string mapName, string presetName);
        Preset Get(string mapName);
    }
}

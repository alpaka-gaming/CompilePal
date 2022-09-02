using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class PresetManager : IPresetManager
    {
        public Dictionary<string, Preset> Items { get; } = new();
        public void Add(Preset model)
        {
            if (Items.ContainsKey(model.Name)) Items.Remove(model.Name);
            Items.Add(model.Name, model);
        }
        public void Set(string mapName, string presetName)
        {
            if (!Items.ContainsKey(presetName)) throw new KeyNotFoundException($"Preset {presetName} was not found");
            if (MapsPresets.ContainsKey(mapName)) MapsPresets[mapName] = presetName;
            else MapsPresets.Add(mapName, presetName);
        }
        public Preset Get(string mapName)
        {
            string presetName;
            if (MapsPresets.ContainsKey(mapName))
                presetName = MapsPresets[mapName];
            else
                presetName = Items.FirstOrDefault().Key;
            
            if (!Items.ContainsKey(presetName)) throw new KeyNotFoundException($"Preset {presetName} was not found");
            if (Items.ContainsKey(presetName)) return Items[presetName];
            
            return null;
        }

        private Dictionary<string, string> MapsPresets { get; } = new();

    }
}

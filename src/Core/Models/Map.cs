using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Core.Records;

namespace Core.Models
{
    public class Map : INotifyPropertyChanged
    {

        private string _file;
        private IEnumerable<Trail> Trails { get; } = Array.Empty<Trail>();

        public Map(string file)
        {
            File = file;
        }

        public string File
        {
            get => _file;
            set
            {
                _file = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Map name without version identifiers
        /// </summary>
        public string Name
        {
            get
            {
                var fullMapName = Path.GetFileNameWithoutExtension(_file);

                // try removing version identifier
                return Regex.Replace(fullMapName ?? string.Empty, @"_[^_]+\d$", "");
            }
        }

        public bool IsBSP => Path.GetExtension(_file)?.Equals(".bsp", StringComparison.InvariantCultureIgnoreCase) ?? false;

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

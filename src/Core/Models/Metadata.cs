using System.Collections.Generic;

namespace Core.Models
{
    public class Metadata
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public float Order { get; set; }

        public bool DoRun { get; set; }
        public bool ReadOutput { get; set; }

        public string Description { get; set; }
        public string Warning { get; set; }
        public bool PresetDefault { get; set; } = false;
        public string BasisString { get; set; }
        public bool SupportsBSP { get; set; } = false;
        public HashSet<int> IncompatibleGames { get; set; }
        public HashSet<int> CompatibleGames { get; set; }
    }
}

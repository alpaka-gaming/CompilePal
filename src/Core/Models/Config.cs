using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Models
{
    public class Config : ICloneable
    {
        public string Name { get; set; }
        public string Parameter { get; set; }
        public string Description { get; set; }

        public string Value { get; set; }
        public bool ValueIsFile { get; set; }
        public bool ValueIsFolder { get; set; }
        public string Value2 { get; set; }
        public bool Value2IsFile { get; set; }
        public bool Value2IsFolder { get; set; }

        public bool ReadOutput { get; set; }

        public bool WaitForExit { get; set; }

        public bool CanHaveValue { get; set; }

        public string Warning { get; set; }

        public bool CanBeUsedMoreThanOnce { get; set; }
        public HashSet<int> IncompatibleGames { get; set; }
        public HashSet<int> CompatibleGames { get; set; }

        public object Clone()
        {
            var result = new Config();
            var all = this.GetType().GetRuntimeProperties()
                .All(p =>
                {
                    p.SetValue(result, p.GetValue(this));
                    return true;
                });
            return (all ? result : null) ?? throw new InvalidOperationException();
        }
    }
}

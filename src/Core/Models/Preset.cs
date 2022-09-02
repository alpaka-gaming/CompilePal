using System;
using System.Collections.Generic;
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Core.Models
{
    public class Preset : IEquatable<Preset>
    {
        public string Name { get; set; }
        public bool AdHoc { get; set; }
        public IEnumerable<string> Commands { get; set; }
        public bool CustomOrder { get; set; }

        public bool Equals(Preset other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Preset)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

    }
}

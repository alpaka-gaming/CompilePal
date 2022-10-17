﻿using System.Globalization;
using System.Windows.Data;

namespace CompilePalX
{
    internal class IsCompatiblePropertyGroup : PropertyGroupDescription
    {
        // Split processes into 2 groups, IsCompatible and Incompatible
        public override object GroupNameFromItem(object item, int level, CultureInfo culture)
        {
            // dynamically get IsCompatible property
            bool? res = item.GetType().GetProperty("IsCompatible")?.GetValue(item, null) as bool?;

            return res == true ? "IsCompatible" : "Incompatible";
        }
    }
}

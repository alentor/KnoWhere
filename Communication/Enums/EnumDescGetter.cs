using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Communication
{

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var info = value.GetType().GetRuntimeField(value.ToString());
            if (info != null)
            {
                var attributes = info.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes != null)
                {
                    foreach (Attribute item in attributes)
                    {
                        if (item is DisplayAttribute)
                            return (item as DisplayAttribute).Description;
                    }
                }
            }
            return value.ToString();
        }
    }
}

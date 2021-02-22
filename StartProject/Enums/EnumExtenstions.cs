using System;
using System.Linq;
using System.Reflection;

namespace StartProject.Enums
{
    static class EnumExtenstions
    {
        public static string GetEnumDescription(this Enum value)
        {
            return value.GetType()
                .GetRuntimeField(value.ToString())
                .GetCustomAttributes<System.ComponentModel.DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;
        }
    }
}

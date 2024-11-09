using Contaminados.Models.Common;
using System.ComponentModel;
using System.Reflection;

namespace Contaminados.Utilities
{
    public static class EnumToString
    {
        public static string GetEnumDescription(RoundsStatus status)
        {
            FieldInfo fi = status.GetType().GetField(status.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : status.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Helpers
{
    public static class ExtensionMethods
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                var type = e.GetType();
                var memberInfo = type.GetMember(e.ToString());
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                return ((DescriptionAttribute)attrs[0]).Description;
            }
            else
                return "This is not an enum";
        }
    }
}

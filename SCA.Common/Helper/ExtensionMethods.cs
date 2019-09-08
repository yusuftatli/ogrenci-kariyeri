using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace SCA.Common
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

        public static T GetSessionData<T>(this HttpContext context, string sessionName) where T : class
        {
            return JsonConvert.DeserializeObject<T>(context.Session.GetString("userInfo") ?? "");
        }

        public static string FriendlyUrl(this string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            url = url.ToLower();
            url = url.Trim();
            if (url.Length > 100)
            {
                url = url.Substring(0, 100);
            }
            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "o");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "u");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            url = r.Replace(url, "-");
            while (url.IndexOf("--") > -1)
                url = url.Replace("--", "-");
            return url;
        }
    }
}

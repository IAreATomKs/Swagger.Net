using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Swagger.Net.Extensions
{
    public static class StringExtensions
    {
        public static string RegexReplace(this string source, string pattern, string replacement)
        {
            return Regex.Replace(source, pattern, replacement);
        }

        public static string TruncateFrom(this string source, string value)
        {
            var index = source.IndexOf(value);
            return index > 0 ? source.Substring(0, index) : source;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Omega.Common.Helpers.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Elimina los espacios en un string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Despace(this string value)
        {
            StringBuilder sb = new StringBuilder();
            bool lastWasSpace = true; // True to eliminate leading spaces

            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsWhiteSpace(value[i]) && lastWasSpace)
                {
                    continue;
                }

                lastWasSpace = Char.IsWhiteSpace(value[i]);

                sb.Append(value[i]);
            }

            // The last character might be a space
            if (Char.IsWhiteSpace(sb[sb.Length - 1]))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the input string with the first character converted to uppercase
        /// </summary>
        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("Elemento nulo!, no se puede obtener letra inicial en mayuscula!");

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string RemoveInvalidUrlCharacters(this string value)
        {
            List<string> invalidCharacters = new List<string>()
            {
                "<",
                ">",
                "*",
                "%",
                "&",
                ":",
                "#",
                @"\",
                ","
            };

            foreach (var invalidChar in invalidCharacters)
            {
                value = value.Replace(invalidChar, string.Empty);
            }

            return value;
        }

    }
}

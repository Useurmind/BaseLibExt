using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibExt.Utils
{
    /// <summary>
    ///     Utility functions for strings.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Trims all whitespaces from the string.
        /// </summary>
        /// <param name="s">The string to trim.</param>
        /// <returns>The trimmed string.</returns>
        public static string TrimWhitespaces(this string s)
        {
            return s.Trim().Trim(Environment.NewLine.ToCharArray()).Trim();
        }
    }
}

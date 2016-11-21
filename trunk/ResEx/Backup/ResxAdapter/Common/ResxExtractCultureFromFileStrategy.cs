using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ResEx.Core;

namespace ResEx.StandardAdapters.Common
{
    public class ResxExtractCultureFromFileStrategy : IExtractCultureFromFileStrategy
    {
        /// <summary>
        /// Regular expression that extracts the culture string out of a resex file name.
        /// </summary>
        /// <remarks></remarks>
        private const string RegExCulture = "(?i:)(?<=\\.)\\D\\D(?:-\\D{2,4}?(?:-\\D\\D\\D\\D)?)?(?={extension})";

        public string GetCulture(string fileName)
        {
            Match cultureMatch = Regex.Match(fileName, GetRegularExpression(fileName));
            if (cultureMatch == null || !cultureMatch.Success)
            {
                return ResourceSet.NeutralCulture;
            }

            return cultureMatch.Value;
        }

        public string ReplaceCulture(string fileName, string value)
        {
            return Regex.Replace(fileName, GetRegularExpression(fileName), value);
        }

        private static string GetRegularExpression(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return RegExCulture.Replace("{extension}", fileInfo.Extension);
        }
    }
}
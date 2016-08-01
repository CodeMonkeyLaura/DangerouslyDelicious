using System.Collections.Generic;

namespace DangerouslyDelicious.Utilities
{
    public class InputStringFormatting
    {
        public static string RemoveUnsafeCharacters(string searchString)
        {
            var badList = new List<string> {";", "<", ">", "#", "+", "=", "[", "]"};

            foreach (var badChar in badList)
            {
                searchString = searchString.Replace(badChar, "");
            }
            
            return searchString;
        }
    }
}
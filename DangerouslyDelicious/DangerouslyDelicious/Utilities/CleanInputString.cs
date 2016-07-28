using System.Collections.Generic;

namespace DangerouslyDelicious.Utilities
{
    public class CleanInputString
    {
        public static string RemoveBadCharacters(string searchString)
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
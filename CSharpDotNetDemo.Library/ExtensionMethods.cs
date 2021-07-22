using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Library
{
    public static class ExtensionMethods
    {
        public static string ChangeFirstLetterCase(this string inputString)
        {
            if (inputString.Length > 0)
            {
                char[] charArray = inputString.ToCharArray();
                charArray[0] = char.IsUpper(charArray[0]) ? charArray[0] : char.ToUpper(charArray[0]);
                string outputString = new string(charArray);
                return outputString;
            }

            else return inputString;
        }

    }
}

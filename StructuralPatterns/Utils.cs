using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralPatterns;

public class Utils
{
    public static int CountSubstringOccurrences(string mainString, string substring)
    {
        if (string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int position = 0;

        while ((position = mainString.IndexOf(substring, position)) != -1)
        {
            count++;
            // Move the search position past the found occurrence
            position += substring.Length;
        }

        return count;
    }
}

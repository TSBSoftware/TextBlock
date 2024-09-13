namespace TextBlock;

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public static class TextBlockExtentions
{
    /// <summary>
    /// Auto trims extra indentation space characters to the level of the leftmost line.
    /// </summary>
    /// <param name="content">String being blocked.</param>
    /// <param name="indent">Number indentation to be added to each line.</param>
    /// <param name="indentChar">Alternate indentation character. Default is space.</param>
    /// <returns></returns>
    public static string TextBlock(this string content, int indent = 0, char indentChar = ' ')
    {
        // Skip the first item to omit the initial newline in the string.
        var lines = splitToLines(content).Skip(1).ToList();

        var indentSize =
            lines.Where(line => Regex.IsMatch(line, @"^[\s]+"))
                 .Min(line => line.TakeWhile(char.IsWhiteSpace).Count());

        var blockedLines =
            lines
                .Select(line => trimStart(line, indentSize))
                .Select(line => line.TrimEnd())
                .Select(line => line.TrimEnd('|'))
                .ToList();

        if (blockedLines.Count > 0)
        {
            // Remove the final newline.
            blockedLines.RemoveAt(lines.Count - 1);
        }

        if (blockedLines.Count == 0)
        {
            return "";
        }
        else
        {
            var joined =
                blockedLines
                    .Aggregate((l, r) =>
                    {
                        if (l.EndsWith(@"\"))
                        {
                            return l.TrimEnd('\\') + r;
                        }
                        else
                        {
                            var paddedRight = r.PadLeft(r.Length + indent, indentChar);
                            return l + Environment.NewLine + paddedRight;
                        }
                    });
            return joined.PadLeft(joined.Length + indent, indentChar);
        }
    }

    /// <summary>
    /// Support method for more efficient splitting of the content string.
    /// </summary>
    static IEnumerable<string> splitToLines(string content)
    {
        using var reader = new StringReader(content);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }

    // Handle special case of trimming the start of the string when embedded
    // newline characters are in the content string. Without this, the lines
    // are not properly trimmed and content can be lost.
    static string trimStart(string line, int trimSize)
    {
        if (line.StartsWith("".PadLeft(trimSize, ' ')) ||
            line.StartsWith("".PadLeft(trimSize, '\t')))
        {
            return line.Substring(trimSize);
        }
        else
        {
            return line.TrimStart();
        }
    }
}

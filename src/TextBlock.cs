namespace TextBlock;

using System;
using System.Linq;

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
        var lines = splitToLines(content).ToList();
        int indentSize = findIndentSize(lines);

        var blockedLines =
            lines
                .Select(line => trimStart(line, indentSize))
                .Select(line => line.TrimEnd())
                .Select(line => line.TrimEnd('|'))
                .ToList();

        if (blockedLines.Count > 1 && blockedLines[blockedLines.Count - 1] == "")
        {
            blockedLines.RemoveAt(blockedLines.Count - 1);
        }

        if (blockedLines.Count == 0)
        {
            return "";
        }
        else if (blockedLines.Count == 1)
        {
            var value = blockedLines[0];
            return value.PadLeft(value.Length + indent, indentChar);
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
    /// Find the smallest indent size that isn't zero. If only zero is found, then
    /// the actual indent size is zero.
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    static int findIndentSize(IEnumerable<string> lines)
    {
        try
        {
            return lines
                .Select(line => line.TakeWhile(char.IsWhiteSpace).Count())
                .Where(count => count > 0)
                .Min();
        }
        catch (Exception _)
        {
            return 0;
        }
    }

    /// <summary>
    /// Support method for more efficient splitting of the content string.
    /// </summary>
    static IEnumerable<string> splitToLines(string content)
    {
        //using var reader = new StringReader(content);
        //string line;
        //while ((line = reader.ReadLine()) != null)
        //{
        //    yield return line;
        //}
        var stripped =
            content.StartsWith(Environment.NewLine)
            ? content.Substring(Environment.NewLine.Length)
            : content;
        return stripped.Split([Environment.NewLine], StringSplitOptions.None);
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

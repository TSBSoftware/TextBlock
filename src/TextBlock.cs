namespace TextBlock;

using System;
using System.Linq;

public static class TextBlockExtentions
{
    public static string TextBlock(this string content)
    {
        return TextBlock(content, 0, ' ');
    }

    public static string TextBlock(this string content, int indent = 4, char indentChar = ' ')
    {
        var lines =
            content
                .Split(Environment.NewLine)
                .Skip(1)
                .ToList();

        var indentSize =
            lines.Where(line => !string.IsNullOrEmpty(line))
                 .Min(line => line.TakeWhile(char.IsWhiteSpace).Count());

        var blockedLines =
            lines
                .Select(line => line[indentSize..])
                .Select(line => line.TrimEnd())
                .Select(line => line.TrimEnd('|'))
                .Select(line => line.Replace(@"\s", " "))
                .ToList();

        if (blockedLines.Count > 0)
        {
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
                        if (l.EndsWith('\\'))
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
}

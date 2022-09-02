using System.IO;
using System.Text;

namespace Core.Helpers
{
    public static class StringUtil
    {
        public static string GetUnquotedMaterial(string quoted)
        {
            var sgts = quoted.Split('\"');
            var unquoted = "";
            var i = 0;
            foreach (var s in sgts)
            {
                if (i++ % 2 != 0)
                {
                    continue;
                }
                unquoted += s;
            }
            return unquoted;
        }

        public static string GetFullPath(string line, string gameInfoDir)
        {
            if (!line.StartsWith("..") || !line.StartsWith(""))
            {
                return line;
            }

            var fullPath = Path.GetFullPath(Path.Combine(gameInfoDir, line));
            return fullPath;
        }

        /// <summary>
        /// Cleans up an improperly formatted KV string
        /// </summary>
        /// <param name="kv">KV String to format</param>
        /// <returns>Formatted KV string</returns>
        public static string GetFormattedKVString(string kv)
        {
            var formatted = new StringBuilder();

            var startIndex = 0;
            var lineQuoteCount = 0;
            for (var i = 0; i < kv.Length; i++)
            {
                var c = kv[i];
                if (c == '{')
                {
                    if (i > startIndex)
                    {
                        formatted.AppendLine(kv.Substring(startIndex, i - startIndex));
                    }

                    formatted.AppendLine("{");

                    startIndex = i + 1;
                    lineQuoteCount = 0;
                }
                else if (c == '}')
                {
                    if (i > startIndex)
                    {
                        var beforeCloseBraceText = kv.Substring(startIndex, i - startIndex).Trim();
                        if (beforeCloseBraceText != "")
                        {
                            formatted.AppendLine(beforeCloseBraceText);
                        }
                    }

                    formatted.AppendLine("}");

                    startIndex = i + 1;
                    lineQuoteCount = 0;
                }
                else if (c == '\"')
                {
                    lineQuoteCount += 1;
                    if (lineQuoteCount == 2)
                    {
                        if (i > startIndex)
                        {
                            formatted.Append(kv.Substring(startIndex, i - startIndex + 1).Trim());
                        }
                        startIndex = i + 1;
                    }
                    else if (lineQuoteCount == 4)
                    {
                        if (i > startIndex)
                        {
                            formatted.AppendLine(kv.Substring(startIndex, i - startIndex + 1).Trim());
                        }

                        startIndex = i + 1;
                        lineQuoteCount = 0;
                    }
                }
                else if (c == '\n')
                {
                    startIndex = i + 1;
                    lineQuoteCount = 0;
                }
            }

            return formatted.ToString();
        }
    }
}

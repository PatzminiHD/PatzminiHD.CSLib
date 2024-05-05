using System.Diagnostics.Contracts;

namespace PatzminiHD.CSLib.Types
{
    /// <summary>
    /// A Colored String
    /// </summary>
    public class ColoredString
    {
        private List<(string content, ConsoleColor foregroundColor, ConsoleColor backgroundColor)> content = new();

        /// <summary>
        /// The Content of the Colored String
        /// </summary>
        public List<(string content, ConsoleColor foregroundColor, ConsoleColor backgroundColor)> Content
        {
            get { return content; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException("value can not be null");
                content = value;
            }
        }
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ColoredString() { }
        /// <summary>
        /// Instantiate a new ColoredString Object with string with default colors
        /// </summary>
        /// <param name="content">The content</param>
        public ColoredString(string content)
        {
            Add(content);
        }
        /// <summary>
        /// Instantiate a new ColoredString Object with string with specified colors
        /// </summary>
        /// <param name="content">The content</param>
        /// <param name="foregroundColor">The foreground Color</param>
        /// <param name="backgroundColor">The background Color</param>
        public ColoredString(string content, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Add(content, foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Add content to the colored string with the default ConsoleColors
        /// </summary>
        /// <param name="content">The string to add</param>
        /// <exception cref="ArgumentNullException">If content is null</exception>
        public void Add(string content)
        {
            if(content == null) throw new ArgumentNullException(nameof(content));
            var defaultConsoleColor = Environment.Get.GetDefaultColor();
            Add(content, defaultConsoleColor.foregroundColor, defaultConsoleColor.backgroundColor);
        }
        /// <summary>
        /// Add content to the colored string with specified ConsoleColors
        /// </summary>
        /// <param name="content">The string to add</param>
        /// <param name="foregroundColor">The foreground color of the part of the string</param>
        /// <param name="backgroundColor">The background color of the part of the string</param>
        /// <exception cref="ArgumentNullException">If content is null</exception>
        public void Add(string content, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            if(content == null) throw new ArgumentNullException(nameof(content));

            Content.Add((content, foregroundColor, backgroundColor));
        }
        /// <summary>
        /// Get just the string of the <see cref="ColoredString"/>
        /// </summary>
        /// <returns>The string</returns>
        [Pure]
        public override string ToString()
        {
            string result = "";
            foreach (var item in Content)
            {
                result += item.content;
            }
            return result;
        }
        /// <summary>
        /// Write the Colored String
        /// </summary>
        public void Write()
        {
            ConsoleColor tmpForeground = Console.ForegroundColor;
            ConsoleColor tmpBackground = Console.BackgroundColor;
            foreach (var item in Content)
            {
                Console.ForegroundColor = item.foregroundColor;
                Console.BackgroundColor = item.backgroundColor;
                Console.Write(item.content);
            }
            Console.ForegroundColor = tmpForeground;
            Console.BackgroundColor = tmpBackground;
        }
        /// <summary>
        /// Write the Colored String with a newline at the end
        /// </summary>
        public void WriteLine()
        {
            Write();
            Console.WriteLine();
        }
        /// <summary>
        /// Write the Colored String, skipping the first startPos characters
        /// </summary>
        /// <param name="startPos">How many characters to skip</param>
        public void Write(uint startPos)
        {
            var tmpContent = Content;
            for (int i = 0; i < tmpContent.Count(); i++)
            {
                foreach (var character in tmpContent[i].content)
                {
                    if (startPos <= 0)
                        break;
                    tmpContent[i] = (tmpContent[i].content.Substring(1), tmpContent[i].foregroundColor, tmpContent[i].backgroundColor);
                    startPos--;
                }
                if (startPos <= 0)
                {
                    break;
                }
                tmpContent.Remove(tmpContent[i]);
            }
            ConsoleColor tmpForeground = Console.ForegroundColor;
            ConsoleColor tmpBackground = Console.BackgroundColor;
            foreach (var item in Content)
            {
                    Console.ForegroundColor = item.foregroundColor;
                    Console.BackgroundColor = item.backgroundColor;
                    Console.Write(item.content);
            }
            Console.ForegroundColor = tmpForeground;
            Console.BackgroundColor = tmpBackground;
        }
        /// <summary>
        /// Write the Colored String, skipping the first startPos characters with a newline at the end
        /// </summary>
        /// <param name="startPos">How many characters to skip</param>
        public void WriteLine(uint startPos)
        {
            Write(startPos);
            Console.WriteLine();
        }
        /// <summary>
        /// Write the Colored String, skipping the first startPos characters, taking length characters
        /// </summary>
        /// <param name="startPos">How many characters to skip</param>
        /// <param name="length">How many characters to take</param>
        public void Write(uint startPos, uint length)
        {
            var tmpContent = Content;
            for(int i = 0; i < tmpContent.Count(); i++)
            {
                foreach(var character in tmpContent[i].content)
                {
                    if (startPos <= 0)
                        break;
                    tmpContent[i] = (tmpContent[i].content.Substring(1), tmpContent[i].foregroundColor, tmpContent[i].backgroundColor);
                    startPos--;
                }
                if(startPos <= 0)
                {
                    break;
                }
                tmpContent.Remove(tmpContent[i]);
            }
            ConsoleColor tmpForeground = Console.ForegroundColor;
            ConsoleColor tmpBackground = Console.BackgroundColor;
            foreach (var item in Content)
            {
                if(length > 0)
                {
                    Console.ForegroundColor = item.foregroundColor;
                    Console.BackgroundColor = item.backgroundColor;
                    foreach (var character in item.content)
                    {
                        Console.Write(character);
                        length--;
                        if(length <= 0)
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
            Console.ForegroundColor = tmpForeground;
            Console.BackgroundColor = tmpBackground;
        }
        /// <summary>
        /// Write the Colored String, skipping the first startPos characters, taking length characters with a newline at the end
        /// </summary>
        /// <param name="startPos">How many characters to skip</param>
        /// <param name="length">How many characters to take</param>
        public void WriteLine(uint startPos, uint length)
        {
            Write(startPos, length);
            Console.WriteLine();
        }
    }
}

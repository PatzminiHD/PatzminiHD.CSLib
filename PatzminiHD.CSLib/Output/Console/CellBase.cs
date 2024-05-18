namespace PatzminiHD.CSLib.Output.Console
{
    /// <summary>
    /// 
    /// </summary>
    public class CellBase
    {
        private Types.ColoredString content = new();
        private uint leftPos, topPos, width, height = 1;

        /// <summary>
        /// The Content of the cell
        /// </summary>
        public Types.ColoredString Content
        {
            get { return content; }
            set
            {
                if(value == null) throw new ArgumentNullException("value can not be null");
                content = value;
            }
        }
        /// <summary>
        /// The Content of the cell as a string<br/>When setting, the content has the default Colors
        /// </summary>
        public string ContentString
        {
            get { return content.ToString(); }
            set { content = new(value); }
        }

        /// <summary>
        /// The X position of the Cell
        /// </summary>
        public uint LeftPos
        {
            get { return leftPos; }
            set { leftPos = value; }
        }
        /// <summary>
        /// The Y position of the Cell
        /// </summary>
        public uint TopPos
        {
            get { return topPos; }
            set { topPos = value; }
        }
        /// <summary>
        /// The Width of the Cell
        /// </summary>
        public uint Width
        {
            get { return width; }
            set { width = value; }
        }
        /// <summary>
        /// The Height of the Cell
        /// </summary>
        public uint Height
        {
            get { return height; }
            set { height = value; }
        }
        /// <summary>
        /// Draw the content of the cell
        /// </summary>
        public void Draw()
        {
            if (content.IsNullOrEmpty())
                return;
            //Draw Content
            for (uint i = 0; i < Height; i++)
            {
                System.Console.SetCursorPosition((int)leftPos, (int)(topPos + i));
                content.Write(i * width, width);

                //Fill remaining space
                ConsoleColor tmpForeground = System.Console.ForegroundColor;
                ConsoleColor tmpBackground = System.Console.BackgroundColor;
                System.Console.ForegroundColor = content.Content[0].foregroundColor;
                System.Console.BackgroundColor = content.Content[0].backgroundColor;
                for (int j = System.Console.CursorLeft; j < LeftPos + Width; j++)
                {
                    System.Console.Write(' ');
                }
                System.Console.ForegroundColor = tmpForeground;
                System.Console.BackgroundColor = tmpBackground;
            }
        }
        /// <summary>
        /// Clear the Content
        /// </summary>
        public void Clear()
        {
            Content = new();
        }
    }
}

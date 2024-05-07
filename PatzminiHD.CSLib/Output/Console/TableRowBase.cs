namespace PatzminiHD.CSLib.Output.Console
{
    /// <summary>
    /// Base Class for a table row
    /// </summary>
    public class TableRowBase
    {
        private List<(object, Type, uint)> rowValues = new();
        private List<TableCell> cells = new();
        private bool isEvenRow = false, isColored = true;
        private uint height = 1, topPos, leftPos;
        private int highlightedCell = -1;
        private ConsoleColor foregroundColor = Environment.Get.GetDefaultColor().foregroundColor;
        private ConsoleColor backgroundColorEven = Environment.Get.GetDefaultColor().backgroundColor;
        private ConsoleColor backgroundColorOdd = ConsoleColor.DarkBlue;
        private ConsoleColor highlightForegroundColor = ConsoleColor.Black;
        private ConsoleColor highlightBackgroundColor = ConsoleColor.White;
        /// <summary>
        /// The Values in this Row<br/>Tuple of object CellValue, Type TypeOfCellValue, uint Width
        /// </summary>
        public List<(object, Type, uint)> RowValues
        {
            get { return rowValues; }
            set
            {
                rowValues = value;
                PopulateCells();
            }
        }
        /// <summary>
        /// If the row is even or odd<br/>(Only used when <see cref="IsColored"/> is true)
        /// </summary>
        public bool IsEvenRow
        {
            get { return isEvenRow; }
            set { isEvenRow = value; }
        }
        /// <summary>
        /// True if the Color should be switched between each Row
        /// </summary>
        public bool IsColored
        {
            get { return isColored; }
            set {  isColored = value; }
        }
        /// <summary>
        /// The Height of the Row
        /// </summary>
        public uint Height
        {
            get { return height; }
            set { height = value; }
        }
        /// <summary>
        /// The Top position of the Row
        /// </summary>
        public uint TopPos
        {
            get { return topPos; }
            set { topPos = value; }
        }
        /// <summary>
        /// The left position of the Row
        /// </summary>
        public uint LeftPos
        {
            get { return leftPos; }
            set { leftPos = value; }
        }
        /// <summary>
        /// The Foreground Color of the cells
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }
        /// <summary>
        /// The Background Color for even cells
        /// </summary>
        public ConsoleColor BackgroundColorEven
        {
            get { return backgroundColorEven; }
            set { backgroundColorEven = value; }
        }
        /// <summary>
        /// The Background Color for odd cells
        /// </summary>
        public ConsoleColor BackgroundColorOdd
        {
            get { return backgroundColorOdd; }
            set { backgroundColorOdd = value; }
        }
        /// <summary>
        /// The Foreground Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; }
        }
        /// <summary>
        /// The Background Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; }
        }


        /// <summary>
        /// The Cell that is highlighted<br/>-1 if no cell should be highlighted
        /// </summary>
        public int HighlightedCell
        {
            get { return highlightedCell; }
            set {
                if (value >= rowValues.Count)
                    throw new ArgumentException(nameof(HighlightedCell) + " can not be larger then number of row values");
                highlightedCell = value;
            }
        }
        /// <summary>
        /// Instantiate a new object of TableRowBase class
        /// </summary>
        public TableRowBase()
        {
        }
        private void PopulateCells()
        {
            cells = new();

            int i = 0;
            if(!IsEvenRow)
                i = 1;

            uint j = 0;

            foreach(var column in RowValues)
            {
                TableCell cell = new TableCell();
                cell.Width = column.Item3;
                cell.Height = Height;
                cell.LeftPos = LeftPos + j;
                cell.TopPos = TopPos;
                cell.ForegroundColor = ForegroundColor;
                j += column.Item3;

                if (i % 2 == 0)
                {
                    cell.BackgroundColor = BackgroundColorEven;
                }
                else
                {
                    cell.BackgroundColor = BackgroundColorOdd;
                }

                if (column.Item2 == typeof(string))
                {
                    cell.ContentString = (string)column.Item1;
                }
                else if(column.Item2 == typeof(int))
                {
                    cell.ContentInt = (int)column.Item1;
                }
                else if (column.Item2 == typeof(double))
                {
                    cell.ContentDouble = (double)column.Item1;
                }
                else if (column.Item2 == typeof(DateTime))
                {
                    cell.ContentDateTime = (DateTime)column.Item1;
                }
                else
                {
                    continue;
                }
                cells.Add(cell);
                i++;
            }
        }
        /// <summary>
        /// Draw all cells in the Row
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (i == HighlightedCell)
                    cells[i].IsHighlighted = true;
                else
                    cells[i].IsHighlighted = false;

                cells[i].Draw();
            }
        }
    }
}

namespace PatzminiHD.CSLib.Output.Console.Table
{
    /// <summary>
    /// Base Class for a table row
    /// </summary>
    public class RowBase
    {
        private List<(Entry, uint)> rowValues = new();
        private List<Cell> cells = new();
        private bool isEvenRow = false, isColored = true, autoDraw = false;
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
        public List<(Entry, uint)> RowValues
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
            set { isEvenRow = value; PopulateCells(); }
        }
        /// <summary>
        /// True if the Color should be switched between each Row
        /// </summary>
        public bool IsColored
        {
            get { return isColored; }
            set { isColored = value; PopulateCells(); }
        }
        /// <summary>
        /// True to automatically redraw the row when a property changes
        /// </summary>
        public bool AutoDraw
        {
            get { return autoDraw; }
            set { autoDraw = value; PopulateCells(); }
        }
        /// <summary>
        /// The Height of the Row
        /// </summary>
        public uint Height
        {
            get { return height; }
            set { height = value; PopulateCells(); }
        }
        /// <summary>
        /// The Top position of the Row
        /// </summary>
        public uint TopPos
        {
            get { return topPos; }
            set { topPos = value; PopulateCells(); }
        }
        /// <summary>
        /// The left position of the Row
        /// </summary>
        public uint LeftPos
        {
            get { return leftPos; }
            set { leftPos = value; PopulateCells(); }
        }
        /// <summary>
        /// The Foreground Color of the cells
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; PopulateCells(); }
        }
        /// <summary>
        /// The Background Color for even cells
        /// </summary>
        public ConsoleColor BackgroundColorEven
        {
            get { return backgroundColorEven; }
            set { backgroundColorEven = value; PopulateCells(); }
        }
        /// <summary>
        /// The Background Color for odd cells
        /// </summary>
        public ConsoleColor BackgroundColorOdd
        {
            get { return backgroundColorOdd; }
            set { backgroundColorOdd = value; PopulateCells(); }
        }
        /// <summary>
        /// The Foreground Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; PopulateCells(); }
        }
        /// <summary>
        /// The Background Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; PopulateCells(); }
        }


        /// <summary>
        /// The Cell that is highlighted<br/>-1 if no cell should be highlighted
        /// </summary>
        public int HighlightedCell
        {
            get { return highlightedCell; }
            set
            {
                if (value >= rowValues.Count)
                    throw new ArgumentException(nameof(HighlightedCell) + " can not be larger then number of row values");
                highlightedCell = value;
                PopulateCells();
            }
        }
        /// <summary>
        /// Instantiate a new object of TableRowBase class
        /// </summary>
        public RowBase()
        {
        }
        private void AutoDrawMethod()
        {
            if (!AutoDraw)
                return;
            Draw();
        }
        private void PopulateCells()
        {
            cells = new();
            if (RowValues == null || RowValues.Count == 0)
                return;

            int i = 0;
            if (!IsEvenRow)
                i = 1;

            uint j = 0;

            foreach (var column in RowValues)
            {
                Cell cell = new Cell();
                cell.Width = column.Item2;
                cell.Height = Height;
                cell.LeftPos = LeftPos + j;
                cell.TopPos = TopPos;
                cell.ForegroundColor = ForegroundColor;
                cell.AutoDraw = AutoDraw;
                j += column.Item2;

                if (i % 2 == 0)
                {
                    cell.BackgroundColor = BackgroundColorEven;
                }
                else
                {
                    cell.BackgroundColor = BackgroundColorOdd;
                }

                if (column.Item1.Type == typeof(string))
                {
                    cell.ContentString = (string)column.Item1.Value;
                }
                else if (column.Item1.Type == typeof(int))
                {
                    cell.ContentInt = (int)column.Item1.Value;
                }
                else if (column.Item1.Type == typeof(double))
                {
                    cell.ContentDouble = (double)column.Item1.Value;
                }
                else if (column.Item1.Type == typeof(DateTime))
                {
                    cell.ContentDateTime = (DateTime)column.Item1.Value;
                }
                else if (column.Item1.Type == typeof(TimeSpan))
                {
                    cell.ContentTimeSpan= (TimeSpan?)column.Item1.Value;
                }
                else
                {
                    continue;
                }
                cells.Add(cell);
                i++;
            }
            AutoDrawMethod();
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

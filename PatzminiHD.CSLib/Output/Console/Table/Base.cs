using PatzminiHD.CSLib.Environment;

namespace PatzminiHD.CSLib.Output.Console.Table
{
    /// <summary>
    /// Base class for a table
    /// </summary>
    public class Base
    {
        private List<RowBase> rows = new();
        private List<(List<(Entry, uint)>, uint)> tableValues = new();
        private (List<(Entry, uint)>, uint) columnHeaders = new();
        private uint topPos = 0, leftPos = 0;
        private int highlightedRow = -1, highlightedColumn = -1;
        private bool isColored = true, autoDraw = false;
        private ConsoleColor foregroundColor = Get.GetDefaultColor().foregroundColor;
        private ConsoleColor backgroundColorEven = Get.GetDefaultColor().backgroundColor;
        private ConsoleColor backgroundColorOdd = ConsoleColor.DarkGray;
        private ConsoleColor highlightForegroundColor = ConsoleColor.Black;
        private ConsoleColor highlightBackgroundColor = ConsoleColor.White;

        /// <summary>
        /// The values in the table
        /// </summary>
        public List<(List<(Entry, uint)>, uint)> TableValues
        {
            get { return tableValues; }
            set
            {
                tableValues = value;
                PopulateTableRows();
            }
        }
        /// <summary>
        /// The Headers of the table columns
        /// </summary>
        public (List<(Entry, uint)>, uint) ColumnHeaders
        {
            get { return columnHeaders; }
            set { columnHeaders = value; }
        }
        /// <summary>
        /// The Top Position of the table
        /// </summary>
        public uint TopPos
        {
            get { return topPos; }
            set { topPos = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Left Position of the table
        /// </summary>
        public uint LeftPos
        {
            get { return leftPos; }
            set { leftPos = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Row that is highlighted <br/>-1 for no highlighting
        /// </summary>
        public int HighlightedRow
        {
            get { return highlightedRow; }
            set
            {
                if (rows == null || rows.Count == 0)
                    return;
                if (value >= rows.Count)
                    throw new ArgumentException(nameof(HighlightedRow) + " can not be larger then number of rows");
                highlightedRow = value;
                PopulateTableRows();
            }
        }
        /// <summary>
        /// The Column that is highlighted <br/>-1 for no highlighting
        /// </summary>
        public int HighlightedColumn
        {
            get { return highlightedColumn; }
            set
            {
                if (rows == null || rows.Count == 0)
                    return;
                if (rows.Count == 0 || value >= rows[0].RowValues.Count)
                    throw new ArgumentException(nameof(HighlightedColumn) + "can not be larger then length of rows");
                highlightedColumn = value;
                PopulateTableRows();
            }
        }
        /// <summary>
        /// The Foreground Color for cells
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Background Color for even cells
        /// </summary>
        public ConsoleColor BackgroundColorEven
        {
            get { return backgroundColorEven; }
            set { backgroundColorEven = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Background Color for odd cells
        /// </summary>
        public ConsoleColor BackgroundColorOdd
        {
            get { return backgroundColorOdd; }
            set { backgroundColorOdd = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Foreground Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; PopulateTableRows(); }
        }
        /// <summary>
        /// The Background Color for highlighted cells
        /// </summary>
        public ConsoleColor HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; PopulateTableRows(); }
        }
        /// <summary>
        /// True if the Cells should be colored
        /// </summary>
        public bool IsColored
        {
            get { return isColored; }
            set { isColored = value; PopulateTableRows(); }
        }
        /// <summary>
        /// True if the table should automatically redraw when a property changes
        /// </summary>
        public bool AutoDraw
        {
            get { return autoDraw; }
            set { autoDraw = value; PopulateTableRows(); }
        }

        /// <summary>
        /// Constructor for the TableBase, initialising only the table values
        /// </summary>
        public Base(List<(List<(Entry, uint)>, uint)> tableValues)
        {
            TableValues = tableValues;
        }
        /// <summary>
        /// Constructor for the TableBase, initialising the table values as well as headers
        /// </summary>
        public Base(List<(List<(Entry, uint)>, uint)> tableValues, (List<(Entry, uint)>, uint) columnHeaders)
        {
            TableValues = tableValues;
            ColumnHeaders = columnHeaders;
        }
        private void AutoDrawMethod()
        {
            if (!AutoDraw)
                return;
            Draw();
        }

        private void PopulateTableRows()
        {
            rows = new();
            if (TableValues == null || TableValues.Count == 0)
                return;
            uint i = 0, j = 0;

            if (ColumnHeaders.Item1 != null && ColumnHeaders.Item1.Count > 0)
            {
                RowBase headers = new()
                {
                    IsEvenRow = i % 2 == 0,
                    IsColored = IsColored,
                    Height = ColumnHeaders.Item2,
                    TopPos = j + TopPos,
                    LeftPos = LeftPos,
                    ForegroundColor = ForegroundColor,
                    BackgroundColorEven = BackgroundColorEven,
                    BackgroundColorOdd = BackgroundColorOdd,
                    HighlightForegroundColor = HighlightForegroundColor,
                    HighlightBackgroundColor = HighlightBackgroundColor,
                    RowValues = ColumnHeaders.Item1,
                };

                rows.Add(headers);
                i++;
                j += ColumnHeaders.Item2;
            }

            foreach (var value in TableValues)
            {
                RowBase row = new()
                {
                    IsEvenRow = i % 2 == 0,
                    IsColored = IsColored,
                    Height = value.Item2,
                    TopPos = j + TopPos,
                    LeftPos = LeftPos,
                    ForegroundColor = ForegroundColor,
                    BackgroundColorEven = BackgroundColorEven,
                    BackgroundColorOdd = BackgroundColorOdd,
                    HighlightForegroundColor = HighlightForegroundColor,
                    HighlightBackgroundColor = HighlightBackgroundColor,
                    RowValues = value.Item1,
                };

                rows.Add(row);
                i++;
                j += value.Item2;
            }
            AutoDrawMethod();
        }
        /// <summary>
        /// Draw every Cell in the Table
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if(ColumnHeaders.Item1 == null || ColumnHeaders.Item1.Count == 0)
                {
                    if(i == HighlightedRow)
                    {
                        rows[i].HighlightedCell = HighlightedColumn;
                    }
                }
                else if (i - 1 == HighlightedRow)
                {
                    rows[i].HighlightedCell = HighlightedColumn;
                }
                else
                {
                    rows[i].HighlightedCell = -1;
                }
                rows[i].Draw();
            }
        }
    }
}

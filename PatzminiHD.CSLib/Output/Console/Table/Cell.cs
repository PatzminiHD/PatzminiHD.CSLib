using PatzminiHD.CSLib.ExtensionMethods;

namespace PatzminiHD.CSLib.Output.Console.Table
{
    /// <summary>
    /// Base Class for a Table Cell
    /// </summary>
    public class Cell : CellBase
    {
        private ConsoleColor foregroundColor = Environment.Get.GetDefaultColor().foregroundColor;
        private ConsoleColor backgroundColor = Environment.Get.GetDefaultColor().backgroundColor;
        private ConsoleColor highlightForegroundColor = ConsoleColor.Black;
        private ConsoleColor highlightBackgroundColor = ConsoleColor.White;
        private bool isHighlighted = false, autoDraw = false;
        /// <summary> The ForegroundColor of the Cell </summary>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; AutoDrawMethod(); }
        }
        /// <summary> The BackgroundColor of the Cell </summary>
        public ConsoleColor BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; AutoDrawMethod(); }
        }
        /// <summary> The HighlightForegroundColor of the Cell </summary>
        public ConsoleColor HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; AutoDrawMethod(); }
        }
        /// <summary> The HighlightBackgroundColor of the Cell </summary>
        public ConsoleColor HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; AutoDrawMethod(); }
        }
        /// <summary>
        /// True if the cell is highlighted
        /// </summary>
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; AutoDrawMethod(); }
        }
        /// <summary>
        /// True to automatically redraw the cell when a property changes
        /// </summary>
        public bool AutoDraw
        {
            get { return autoDraw; }
            set { autoDraw = value; AutoDrawMethod(); }
        }
        /// <summary>
        /// The Content as an String
        /// </summary>
        public new string ContentString
        {
            get
            {
                return base.ContentString;
            }
            set
            {
                Clear();
                Content.Add(value, ForegroundColor, BackgroundColor);
                AutoDrawMethod();
            }
        }
        /// <summary>
        /// The Content as an Integer
        /// </summary>
        public int? ContentInt
        {
            get
            {
                if (int.TryParse(ContentString, out int value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                Clear();
                if (!value.HasValue)
                    return;
                string? stringValue = value.ToString();
                if (stringValue == null)
                    return;
                Content.Add(stringValue, ForegroundColor, BackgroundColor);
                AutoDrawMethod();
            }
        }
        /// <summary>
        /// The Content as a Double
        /// </summary>
        public double? ContentDouble
        {
            get
            {
                if (double.TryParse(ContentString, out double value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                Clear();
                if (!value.HasValue)
                    return;
                string? stringValue = value.ToString();
                if (stringValue == null)
                    return;
                Content.Add(stringValue, ForegroundColor, BackgroundColor);
                AutoDrawMethod();
            }
        }
        /// <summary>
        /// The Content as a DateTime
        /// </summary>
        public DateTime? ContentDateTime
        {
            get
            {
                if (DateTime.TryParse(ContentString, out DateTime value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                Clear();
                if (!value.HasValue)
                    return;
                string? stringValue = value.ToString();
                if (stringValue == null)
                    return;
                Content.Add(stringValue, ForegroundColor, BackgroundColor);
                AutoDrawMethod();
            }
        }
        /// <summary>
        /// The Content as a TimeSpan
        /// </summary>
        public TimeSpan? ContentTimeSpan
        {
            get
            {
                if (TimeSpan.TryParse(ContentString, out TimeSpan value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                Clear();
                if (!value.HasValue)
                    return;
                string? stringValue = value.ToString("DDd, HH:MM:SS");
                if (stringValue == null)
                    return;
                Content.Add(stringValue, ForegroundColor, BackgroundColor);
                AutoDrawMethod();
            }
        }
        private void AutoDrawMethod()
        {
            if (!AutoDraw)
                return;
            Draw();
        }
        /// <summary>
        /// Draw the cell
        /// </summary>
        public new void Draw()
        {
            if (Content != null && Content.Content != null && Content.Content.Count > 0)
            {
                if (IsHighlighted)
                {
                    //Set Highlight Color
                    var tmp = Content.Content[0];
                    tmp.foregroundColor = HighlightForegroundColor;
                    tmp.backgroundColor = HighlightBackgroundColor;
                    Content.Content[0] = tmp;
                }
                base.Draw();
                if (IsHighlighted)
                {
                    //Set Highlight Color
                    var tmp = Content.Content[0];
                    tmp.foregroundColor = ForegroundColor;
                    tmp.backgroundColor = BackgroundColor;
                    Content.Content[0] = tmp;
                }
            }
        }
    }
}

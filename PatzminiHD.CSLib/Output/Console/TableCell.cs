﻿namespace PatzminiHD.CSLib.Output.Console
{
    /// <summary>
    /// Base Class for a Table Cell
    /// </summary>
    public class TableCell : CellBase
    {
        private ConsoleColor foregroundColor = Environment.Get.GetDefaultColor().foregroundColor;
        private ConsoleColor backgroundColor = Environment.Get.GetDefaultColor().backgroundColor;
        private ConsoleColor highlightForegroundColor = ConsoleColor.Black;
        private ConsoleColor highlightBackgroundColor = ConsoleColor.White;
        private bool isHighlighted = false;
        /// <summary> The ForegroundColor of the Cell </summary>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }
        /// <summary> The BackgroundColor of the Cell </summary>
        public ConsoleColor BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        /// <summary> The HighlightForegroundColor of the Cell </summary>
        public ConsoleColor HighlightForegroundColor
        {
            get { return highlightForegroundColor; }
            set { highlightForegroundColor = value; }
        }
        /// <summary> The HighlightBackgroundColor of the Cell </summary>
        public ConsoleColor HighlightBackgroundColor
        {
            get { return highlightBackgroundColor; }
            set { highlightBackgroundColor = value; }
        }
        /// <summary>
        /// True if the cell is highlighted
        /// </summary>
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { isHighlighted = value; }
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
            }
        }
        /// <summary>
        /// The Content as an Integer
        /// </summary>
        public int? ContentInt
        {
            get
            {
                if(int.TryParse(ContentString, out int value))
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
            }
        }
        /// <summary>
        /// Draw the 
        /// </summary>
        public new void Draw()
        {
            if(Content != null && Content.Content != null && Content.Content.Count > 0)
            {
                if(IsHighlighted)
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

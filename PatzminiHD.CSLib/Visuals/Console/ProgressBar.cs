using PatzminiHD.CSLib.Math.Extensions;

namespace PatzminiHD.CSLib.Visuals.Console
{
    /// <summary>
    /// A class for displaying a progress bar in the form of "87%  [######### ]"
    /// </summary>
    public class ProgressBar
    {
        private int minValue, maxValue, value, left, top;
        private byte prevPercent;
        private uint length;
        private bool autoDraw, preventOverdraw;

        /// <summary>
        /// The minimum value that <see cref="Value"/> can be
        /// </summary>
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; DrawIfAutoDraw(); }
        }
        /// <summary>
        /// The maximum value that <see cref="Value"/> can be
        /// </summary>
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; DrawIfAutoDraw(); }
        }
        /// <summary>
        /// The distance of the progress bar from the left of the console
        /// </summary>
        public int Left
        {
            get { return left; }
            set { left = value; DrawIfAutoDraw(); }
        }
        /// <summary>
        /// The distance of the progress bar from the top of the console
        /// </summary>
        public int Top
        {
            get { return top; }
            set { top = value; DrawIfAutoDraw(); }
        }
        /// <summary>
        /// The current value of the progress. Values outside of <see cref="MinValue"/>
        /// <see cref="MaxValue"/> will get clamped between those two
        /// </summary>
        public int Value
        {
            get { return value; }
            set {
                this.value = int.Clamp(value, minValue, maxValue);
                DrawIfAutoDraw();
            }
        }
        /// <summary>
        /// The length of the progress bar. Can not be less than 8
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public uint Length
        {
            get { return length; }
            set {
                if (length < 8)
                    throw new ArgumentException($"{nameof(Length)} can not be less then 8");
                length = value;
                DrawIfAutoDraw();
            }
        }

        /// <summary>
        /// True if the Progressbar should automatically be redrawn when properties are updated
        /// </summary>
        public bool AutoDraw
        {
            get { return autoDraw; }
            set { autoDraw = value; }
        }

        /// <summary>
        /// True if the progress bar should not be redrawn when the percentage does not change
        /// (might lead to a choppy animation of the bar if length is >100)
        /// </summary>
        public bool PreventOverdraw
        {
            get { return preventOverdraw; }
            set { preventOverdraw = value; }
        }

        /// <summary>
        /// Constructor for the ProgressBar Object
        /// </summary>
        /// <param name="min">The minimum value that <see cref="Value"/> can be</param>
        /// <param name="max">The maximum value that <see cref="Value"/> can be</param>
        /// <param name="posLeft">The distance of the progress bar from the left of the console</param>
        /// <param name="posTop">The distance of the progress bar from the top of the console</param>
        /// <param name="maxLength">The length of the progress bar. Can not be less than 8</param>
        /// <param name="autoDraw">True if the Progressbar should automatically be redrawn when properties are updated</param>
        /// <param name="preventOverdraw">True if the progress bar should not be redrawn when the percentage does not change (might lead to a choppy animation of the bar if length is >100)</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="max"/> is less than <paramref name="min"/></exception>
        public ProgressBar(int min, int max, int posLeft, int posTop, uint maxLength, bool autoDraw, bool preventOverdraw = true)
        {
            if (max < min)
                throw new ArgumentException($"{nameof(max)} can not be less then {nameof(min)}");
            this.autoDraw = false;
            minValue = min;
            maxValue = max;
            left = posLeft;
            top = posTop;
            length = maxLength;
            value = 0;
            this.autoDraw = autoDraw;
            this.preventOverdraw = preventOverdraw;
        }

        private void DrawIfAutoDraw()
        {
            byte percent = (byte)Number.Map(this.value, this.minValue, this.maxValue, 0, 100);
            //If no change happens, there is no need to redraw
            if (autoDraw && (percent != prevPercent || !preventOverdraw))
                Draw();
        }

        /// <summary>
        /// Draw the progressbar
        /// </summary>
        public void Draw()
        {
            Draw(this.left, this.top);
        }

        /// <summary>
        /// Draw the progressbar at a specific location
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void Draw(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
            byte percent = (byte)Number.Map(this.value, this.minValue, this.maxValue, 0, 100);
            
            System.Console.Write($"{percent}%");
            System.Console.SetCursorPosition(left + 5, top);
            System.Console.Write("[");
            for (int i = 6; i < Number.Map(this.Value, this.MinValue, this.MaxValue, 6, (int)this.Length); i++)
            {
                System.Console.Write("#");
            }
            for (int i = Number.Map(this.Value, this.MinValue, this.MaxValue, 0, (int)this.Length - 6); i < this.Length - 6; i++)
            {
                System.Console.Write(" ");
            }
            System.Console.Write("]");

            prevPercent = percent;
        }
    }
}

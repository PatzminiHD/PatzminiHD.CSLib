using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Input.Console
{
    /// <summary>
    /// Contains Methods for showing a drop down menu
    /// </summary>
    public static class DropDownMenu
    {
        private static void WriteSelections((int left, int top) startingPosition, List<string> selections, int readerOffset, int xEnd)
        {
            if (System.Console.WindowHeight - startingPosition.top > selections.Count())
            {
                //No scrolling needed
                Output.Console.Color.SwapForegroundBackgroundColor();

                for (int i = 0; i < selections.Count(); i++)
                {
                    if (readerOffset != -1 && (i == readerOffset || i == readerOffset + 1))
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    System.Console.SetCursorPosition(startingPosition.left, startingPosition.top + i + 1);
                    System.Console.Write(selections[i]);

                    if (i != readerOffset)
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    for (int j = System.Console.CursorLeft; j < xEnd; j++)
                    {
                        System.Console.Write(' ');
                    }
                    if (i != readerOffset)
                        Output.Console.Color.SwapForegroundBackgroundColor();
                }
            }
            else if (startingPosition.top > selections.Count())
            {
                //Drop up
                Output.Console.Color.SwapForegroundBackgroundColor();

                for (int i = 0; i < selections.Count(); i++)
                {
                    if (readerOffset != -1 && (i == readerOffset || i == readerOffset + 1))
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    System.Console.SetCursorPosition(startingPosition.left, startingPosition.top - selections.Count() + i);
                    System.Console.Write(selections[i]);

                    if (i != readerOffset)
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    for (int j = System.Console.CursorLeft; j < xEnd; j++)
                    {
                        System.Console.Write(' ');
                    }
                    if (i != readerOffset)
                        Output.Console.Color.SwapForegroundBackgroundColor();
                }
            }
            else if (System.Console.WindowHeight - startingPosition.top > startingPosition.top)
            {
                //Drop down with scrolling
                Output.Console.Color.SwapForegroundBackgroundColor();

                for (int i = 0; i < System.Console.WindowHeight - startingPosition.top - 1; i++)
                {
                    int lastPos = i + readerOffset - (System.Console.WindowHeight - startingPosition.top - 2);
                    System.Console.SetCursorPosition(startingPosition.left, startingPosition.top + i + 1);
                    if (readerOffset > System.Console.WindowHeight - startingPosition.top - 2)
                    {
                        if (readerOffset != -1 && (lastPos == readerOffset || lastPos == readerOffset + 1))
                            Output.Console.Color.SwapForegroundBackgroundColor();
                        System.Console.Write(selections[lastPos]);
                    }
                    else
                    {
                        if (readerOffset != -1 && (i == readerOffset || i == readerOffset + 1))
                            Output.Console.Color.SwapForegroundBackgroundColor();
                        System.Console.Write(selections[i]);
                    }

                    if (i != readerOffset && (readerOffset <= System.Console.WindowHeight - startingPosition.top - 2 || lastPos != readerOffset))
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    for (int j = System.Console.CursorLeft; j < xEnd; j++)
                    {
                        System.Console.Write(' ');
                    }
                    if (i != readerOffset && (readerOffset <= System.Console.WindowHeight - startingPosition.top - 2 || lastPos != readerOffset))
                        Output.Console.Color.SwapForegroundBackgroundColor();
                }
            }
            else
            {
                //Drop up with scrolling
                Output.Console.Color.SwapForegroundBackgroundColor();

                for (int i = 0; i < startingPosition.top; i++)
                {
                    System.Console.SetCursorPosition(startingPosition.left, i);

                    int lastPos = i + readerOffset - (selections.Count() - startingPosition.top);

                    if (readerOffset < selections.Count() - startingPosition.top + 1)
                    {
                        if (readerOffset != -1 && (i == readerOffset || i == readerOffset + 1))
                            Output.Console.Color.SwapForegroundBackgroundColor();
                        System.Console.Write(selections[i]);
                    }
                    else
                    {
                        if (readerOffset != -1 && (lastPos == readerOffset || lastPos == readerOffset + 1))
                            Output.Console.Color.SwapForegroundBackgroundColor();
                        System.Console.Write(selections[lastPos]);
                    }


                    if (i != readerOffset && (i != startingPosition.top - 1 || readerOffset < selections.Count() - startingPosition.top + 1))
                        Output.Console.Color.SwapForegroundBackgroundColor();
                    for (int j = System.Console.CursorLeft; j < xEnd; j++)
                    {
                        System.Console.Write(' ');
                    }
                    if (i != readerOffset && i != startingPosition.top - 1)
                        Output.Console.Color.SwapForegroundBackgroundColor();
                }
            }

            Output.Console.Color.ResetColor();
        }
        /// <summary>
        /// Show a DropDownMenu
        /// </summary>
        /// <param name="options"></param>
        /// <param name="defaultSelection"></param>
        /// <returns></returns>
        public static int Show(List<string> options, int defaultSelection = -1)
        {
            var startingPosition = System.Console.GetCursorPosition();

            for (int i = 0; i < options.Count(); i++)
            {
                if (options[i].Length > System.Console.WindowWidth - startingPosition.Left)
                    options[i] = options[i].Substring(0, System.Console.WindowWidth - startingPosition.Left - 4) + "...";
            }

            int xEnd = 0;
            foreach (string selection in options)
            {
                if (selection.Length > xEnd)
                    xEnd = selection.Length;
            }
            if (xEnd < "<Select>".Length)
                xEnd = "<Select>".Length;

            xEnd += startingPosition.Left;

            bool flag = true;
            int readerOffset = defaultSelection;

            while (flag)
            {
                System.Console.SetCursorPosition(startingPosition.Left, startingPosition.Top);
                if (readerOffset >= 0 && readerOffset < options.Count())
                    System.Console.Write(options[readerOffset]);
                else
                    System.Console.Write("<Select>");

                for (int i = System.Console.CursorLeft; i < xEnd; i++)
                {
                    System.Console.Write(' ');
                }

                WriteSelections(startingPosition, options, readerOffset, xEnd);

                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        flag = false;
                        break;
                    case ConsoleKey.Escape:
                        readerOffset = defaultSelection;
                        flag = false;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.K:
                        if (readerOffset > 0)
                            readerOffset--;
                        else if (readerOffset <= 0)
                            readerOffset = options.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                        if (readerOffset < options.Count - 1)
                            readerOffset++;
                        else
                            readerOffset = 0;
                        break;
                    case ConsoleKey.PageUp:
                        readerOffset = 0;
                        break;
                    case ConsoleKey.PageDown:
                        readerOffset = options.Count - 1;
                        break;
                }
            }

            return readerOffset;
        }
    }
}

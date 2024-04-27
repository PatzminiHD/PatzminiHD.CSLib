using System.Numerics;
using System.Security.AccessControl;

namespace PatzminiHD.CSLib.Input
{
    /// <summary>
    /// Input Methods used in the Console
    /// </summary>
    public class Console
    {
        /// <summary>
        /// Get an integer from the user
        /// </summary>
        /// <param name="value">The value the user entered. 0 if the user cancelled the input</param>
        /// <param name="message">The message to display to the user</param>
        /// <param name="emptyToCancel">True if the user can enter nothing to cancel the input</param>
        /// <returns>True if the input was valid<br/>False if the input was cancelled</returns>
        public static bool TryGetInt(out int value, string message, bool emptyToCancel = true)
        {
            value = 0;
            System.Console.Write(message);
            var userInput = System.Console.ReadLine();
            while (!int.TryParse(userInput, out value))
            {
                if ((userInput == null || userInput == "") && emptyToCancel)
                    break;
                System.Console.Write("Invalid input. " + message);
                userInput = System.Console.ReadLine();
            }

            return userInput == null || userInput == "" ? false : true;
        }

        /// <summary>
        /// Get an double from the user
        /// </summary>
        /// <param name="value">The value the user entered. 0 if the user cancelled the input</param>
        /// <param name="message">The message to display to the user</param>
        /// <param name="emptyToCancel">True if the user can enter nothing to cancel the input</param>
        /// <returns>True if the input was valid<br/>False if the input was cancelled</returns>
        public static bool TryGetDouble(out double value, string message, bool emptyToCancel = true)
        {
            value = 0;
            System.Console.Write(message);
            var userInput = System.Console.ReadLine();
            while (!double.TryParse(userInput, out value))
            {
                if ((userInput == null || userInput == "") && emptyToCancel)
                    break;
                System.Console.Write("Invalid input. " + message);
                userInput = System.Console.ReadLine();
            }

            return userInput == null || userInput == "" ? false : true;
        }

        /// <summary>
        /// Class containing members for showing MessageBoxes
        /// </summary>
        public static class MessageBox
        {
            /// <summary>
            /// Valid Response Types
            /// </summary>
            public enum ResponseOptions
            {
                /// <summary> Only an OK respone option </summary>
                OK,
                /// <summary> OK and Cancel respone options </summary>
                OK_CANCEL,
                /// <summary> Yes and No response options </summary>
                YES_NO,
                /// <summary> Yes, No and Cancel response options </summary>
                YES_NO_CANCEL,
            }

            /// <summary> Responses that are returned from the message box </summary>
            public enum Response
            {
                /// <summary> User responded OK </summary>
                OK,
                /// <summary> User responded Cancel </summary>
                CANCEL,
                /// <summary> User responded Yes </summary>
                YES,
                /// <summary> User responded No </summary>
                NO
            }

            private static void ClearSpace(int x1, int y1, int x2, int y2)
            {
                for (int i = y1; i < y2 ; i++)
                {
                    System.Console.SetCursorPosition(x1, i);
                    for (int j = x1; j < x2 ; j++)
                    {
                        System.Console.Write(" ");
                    }
                }
            }
            private static void DrawFrame(int x1, int y1, int x2, int y2)
            {
                // ┌┐├┤─    ╔╗╠╣═
                // └┘┴┬│    ╚╝╩╦║

                ClearSpace(x1 + 1, y1 + 1, x2 - 1, y2 - 1);

                System.Console.SetCursorPosition(x1, y1);
                System.Console.Write('╔');
                for (int i = x1 + 1; i < x2 - 1; i++)
                {
                    System.Console.Write('═');
                }
                System.Console.Write('╗');

                for(int i = y1 + 1; i < y2 - 3; i++)
                {
                    System.Console.SetCursorPosition(x1, i);
                    System.Console.Write('║');
                    System.Console.SetCursorPosition(x2 - 1, i);
                    System.Console.Write('║');
                }

                System.Console.SetCursorPosition(x1, y2 - 3);
                System.Console.Write('╠');
                for (int i = x1 + 1; i < x2 - 1; i++)
                {
                    System.Console.Write('═');
                }
                System.Console.Write('╣');

                System.Console.SetCursorPosition(x1, y2 - 2);
                System.Console.Write('║');
                System.Console.SetCursorPosition(x2 - 1, y2 - 2);
                System.Console.Write('║');

                System.Console.SetCursorPosition(x1, y2 - 1);
                System.Console.Write('╚');
                for (int i = x1 + 1; i < x2 - 1; i++)
                {
                    System.Console.Write('═');
                }
                System.Console.Write('╝');
            }

            private static void SwapForegroundBackgroundColor()
            {
                var tmp = System.Console.ForegroundColor;
                System.Console.ForegroundColor = System.Console.BackgroundColor;
                System.Console.BackgroundColor = tmp;
            }

            private static void DrawResponses(int xLeft, int y, ResponseOptions responseOptions, Response selectedResponse)
            {
                switch(responseOptions)
                {
                    case ResponseOptions.OK:
                        switch(selectedResponse)
                        {
                            case Response.OK:
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 3, y);
                                System.Console.Write("OK");
                                System.Console.ResetColor();
                                break;
                            default:
                                System.Console.SetCursorPosition(xLeft - 3, y);
                                System.Console.Write("OK");
                                break;
                        }
                        break;
                    case ResponseOptions.OK_CANCEL:
                        switch (selectedResponse)
                        {
                            case Response.OK:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("OK");
                                System.Console.ResetColor();
                                break;
                            case Response.CANCEL:
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("OK");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                System.Console.ResetColor();
                                break;
                            default:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("OK");
                                break;
                        }
                        break;
                    case ResponseOptions.YES_NO:
                        switch(selectedResponse)
                        {
                            case Response.NO:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Yes");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 3, y);
                                System.Console.Write("No");
                                System.Console.ResetColor();
                                break;
                            case Response.YES:
                                System.Console.SetCursorPosition(xLeft - 3, y);
                                System.Console.Write("No");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Yes");
                                System.Console.ResetColor();
                                break;
                            default:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Yes");
                                System.Console.SetCursorPosition(xLeft - 3, y);
                                System.Console.Write("No");
                                break;
                        }
                        break;
                    case ResponseOptions.YES_NO_CANCEL:
                        switch(selectedResponse)
                        {
                            case Response.CANCEL:
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("No");
                                System.Console.SetCursorPosition(xLeft - 14, y);
                                System.Console.Write("Yes");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                System.Console.ResetColor();
                                break;
                            case Response.NO:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                System.Console.SetCursorPosition(xLeft - 14, y);
                                System.Console.Write("Yes");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("No");
                                System.Console.ResetColor();
                                break;
                            case Response.YES:
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("No");
                                SwapForegroundBackgroundColor();
                                System.Console.SetCursorPosition(xLeft - 14, y);
                                System.Console.Write("Yes");
                                System.Console.ResetColor();
                                break;
                            default:
                                System.Console.SetCursorPosition(xLeft - 10, y);
                                System.Console.Write("No");
                                System.Console.SetCursorPosition(xLeft - 14, y);
                                System.Console.Write("Yes");
                                System.Console.SetCursorPosition(xLeft - 7, y);
                                System.Console.Write("Cancel");
                                break;
                        }
                        break;
                }
            }

            private static List<string> SplitMessage(string message, int maxWidth)
            {
                List<string > result = new();

                foreach (string Line in message.Split('\n'))
                {
                    string line = Line;
                    while (true)
                    {
                        string temp;
                        if (line.Length > maxWidth)
                        {
                            temp = line;
                            while (temp.Length > maxWidth)
                            {
                                temp = line.Substring(0, maxWidth);
                                while (!char.IsWhiteSpace(temp.Last()))
                                {
                                    temp = temp.Remove(temp.Length - 1);
                                }
                                temp = temp.Remove(temp.Length - 1);

                                if (temp.Length <= 0)
                                {
                                    temp = line;
                                }
                            }
                            line = line.Substring(1);
                        }
                        else
                            temp = line;

                        line = line.Remove(0, temp.Length);

                        result.Add(temp);

                        if (line == "")
                            break;
                    }
                }

                return result;
            }

            /// <summary>
            /// Show a Message Box with only an OK respone option
            /// </summary>
            /// <param name="message">The Message to show</param>
            /// <param name="title">Title, shown in the top right of the box</param>
            public static Response Show(string message, string title = "")
            {
                return Show(message, title, ResponseOptions.OK, 4);
            }

            /// <summary>
            /// Show a message box
            /// </summary>
            /// <param name="message">The message to show</param>
            /// <param name="title">Title, shown in the top right of the box</param>
            /// <param name="responseType">Response option to give the player</param>
            /// <param name="edgeDistance">Distance between the edge of the console and the frame of the message box</param>
            public static Response Show(string message, string title, ResponseOptions responseType, int edgeDistance = 4)
            {
                int maxWidth = System.Console.WindowWidth - edgeDistance*2, maxHight = System.Console.WindowHeight - edgeDistance*2;
                if (maxWidth < 17 || maxHight < 5)
                    throw new ArgumentException("Not enough space to display Message Box");

                message = message.Replace("\t", "    ");

                List<string> messages = SplitMessage(message, maxWidth - 4);
                int x1, y1, x2, y2, readerStart = 0;
                Response selectedResponse;

                if (messages.Count == 1)
                {
                    x1 = (System.Console.WindowWidth / 2) - ((messages[0].Length + 4) / 2);
                    x2 = x1 + messages[0].Length + 4;
                    y1 = (System.Console.WindowHeight / 2) - (7 / 2);
                    y2 = y1 + 7;
                }
                else
                {
                    int longestMessageLine = 0;
                    foreach (string line in messages)
                    {
                        if(line.Length + 4 > longestMessageLine)
                            longestMessageLine = line.Length + 4;
                    }
                    if(longestMessageLine < 17)
                        longestMessageLine = 17;

                    x1 = (System.Console.WindowWidth / 2) - (longestMessageLine / 2);
                    x2 = x1 + longestMessageLine;

                    if(messages.Count + 5 <= maxHight)
                    {
                        y1 = (System.Console.WindowHeight / 2) - ((messages.Count + 6) / 2);
                        y2 = y1 + (messages.Count + 6);
                    }
                    else
                    {
                        y1 = (System.Console.WindowHeight / 2) - (maxHight / 2);
                        y2 = y1 + maxHight;
                    }
                }

                switch (responseType)
                {
                    case ResponseOptions.OK:
                    case ResponseOptions.OK_CANCEL:
                        selectedResponse = Response.OK;
                        break;

                    case ResponseOptions.YES_NO:
                    case ResponseOptions.YES_NO_CANCEL:
                        selectedResponse = Response.YES;
                        break;

                    default:
                        throw new ArgumentException($"{nameof(responseType)} is not valid");
                }

                DrawFrame(x1, y1, x2, y2);

                bool flag = true;
                while(flag)
                {
                    ClearSpace(x1 + 2, y1 + 2, x2 - 2, y2 - 3);
                    for (int i = 0; i < (y2 - y1) - 6; i++)
                    {
                        System.Console.SetCursorPosition(x1 + 2, y1 + i + 2);
                        if (i + readerStart >= messages.Count)
                        {
                            break;
                        }
                        System.Console.Write(messages[i + readerStart]);
                    }

                    DrawResponses(x2 - 1, y2 - 2, responseType, selectedResponse);

                    switch(System.Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            flag = false;
                            break;
                        case ConsoleKey.Escape:
                            switch(responseType)
                            {
                                case ResponseOptions.YES_NO_CANCEL:
                                case ResponseOptions.OK_CANCEL:
                                    selectedResponse = Response.CANCEL;
                                    break;
                                case ResponseOptions.YES_NO:
                                    selectedResponse = Response.NO;
                                    break;
                            }
                            flag = false;
                            break;

                        case ConsoleKey.UpArrow:
                        case ConsoleKey.K:
                            if(readerStart > 0)
                                readerStart--;
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.J:
                            if(readerStart < messages.Count - 1)
                                readerStart++;
                            break;

                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.H:
                            switch(responseType)
                            {
                                case ResponseOptions.YES_NO_CANCEL:
                                    if(selectedResponse == Response.CANCEL)
                                        selectedResponse = Response.NO;
                                    else if(selectedResponse == Response.NO)
                                        selectedResponse = Response.YES;
                                    break;
                                case ResponseOptions.YES_NO:
                                    if (selectedResponse == Response.NO)
                                        selectedResponse = Response.YES;
                                    break;
                                case ResponseOptions.OK_CANCEL:
                                    if (selectedResponse == Response.CANCEL)
                                        selectedResponse = Response.OK;
                                    break;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                        case ConsoleKey.L:
                            switch (responseType)
                            {
                                case ResponseOptions.YES_NO_CANCEL:
                                    if (selectedResponse == Response.YES)
                                        selectedResponse = Response.NO;
                                    else if (selectedResponse == Response.NO)
                                        selectedResponse = Response.CANCEL;
                                    break;
                                case ResponseOptions.YES_NO:
                                    if (selectedResponse == Response.YES)
                                        selectedResponse = Response.NO;
                                    break;
                                case ResponseOptions.OK_CANCEL:
                                    if (selectedResponse == Response.OK)
                                        selectedResponse = Response.CANCEL;
                                    break;
                            }
                            break;
                    }
                    DrawResponses(x2 - 1, y2 - 2, responseType, selectedResponse);
                }

                return selectedResponse;
            }
        }
        
    }
}

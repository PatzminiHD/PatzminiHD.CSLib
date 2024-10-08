﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Input.Console
{
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
            //Iterate over every line
            for (int i = y1; i < y2; i++)
            {
                System.Console.SetCursorPosition(x1, i);
                //Iterate over every character
                for (int j = x1; j < x2; j++)
                {
                    System.Console.Write(" ");
                }
            }
        }
        private static void DrawFrame(int x1, int y1, int x2, int y2, string title)
        {
            // ┌┐├┤─    ╔╗╠╣═
            // └┘┴┬│    ╚╝╩╦║

            ClearSpace(x1 + 1, y1 + 1, x2 - 1, y2 - 1);

            //Draw top Line
            System.Console.SetCursorPosition(x1, y1);
            System.Console.Write('╔');
            for (int i = x1 + 1; i < x2 - 1; i++)
            {
                System.Console.Write('═');
            }
            System.Console.Write('╗');

            //Replace section of first line with title
            System.Console.SetCursorPosition(x1 + 2, y1);
            if (title.Length > x2 - x1 - 7)
                System.Console.Write(title.Substring(0, x2 - x1 - 7) + "...");
            else
                System.Console.Write(title);

            //Draw side frame
            for (int i = y1 + 1; i < y2 - 3; i++)
            {
                System.Console.SetCursorPosition(x1, i);
                System.Console.Write('║');
                System.Console.SetCursorPosition(x2 - 1, i);
                System.Console.Write('║');
            }

            //Draw line between message and response section
            System.Console.SetCursorPosition(x1, y2 - 3);
            System.Console.Write('╠');
            for (int i = x1 + 1; i < x2 - 1; i++)
            {
                System.Console.Write('═');
            }
            System.Console.Write('╣');

            //Draw vertical frame of response section
            System.Console.SetCursorPosition(x1, y2 - 2);
            System.Console.Write('║');
            System.Console.SetCursorPosition(x2 - 1, y2 - 2);
            System.Console.Write('║');

            //Draw bottom line
            System.Console.SetCursorPosition(x1, y2 - 1);
            System.Console.Write('╚');
            for (int i = x1 + 1; i < x2 - 1; i++)
            {
                System.Console.Write('═');
            }
            System.Console.Write('╝');
        }

        private static void DrawResponses(int xLeft, int y, ResponseOptions responseOptions, Response selectedResponse)
        {
            switch (responseOptions)
            {
                case ResponseOptions.OK:
                    switch (selectedResponse)
                    {
                        case Response.OK:
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 3, y);
                            System.Console.Write("OK");
                            Output.Console.Color.ResetColor();
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
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 10, y);
                            System.Console.Write("OK");
                            Output.Console.Color.ResetColor();
                            break;
                        case Response.CANCEL:
                            System.Console.SetCursorPosition(xLeft - 10, y);
                            System.Console.Write("OK");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Cancel");
                            Output.Console.Color.ResetColor();
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
                    switch (selectedResponse)
                    {
                        case Response.NO:
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Yes");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 3, y);
                            System.Console.Write("No");
                            Output.Console.Color.ResetColor();
                            break;
                        case Response.YES:
                            System.Console.SetCursorPosition(xLeft - 3, y);
                            System.Console.Write("No");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Yes");
                            Output.Console.Color.ResetColor();
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
                    switch (selectedResponse)
                    {
                        case Response.CANCEL:
                            System.Console.SetCursorPosition(xLeft - 10, y);
                            System.Console.Write("No");
                            System.Console.SetCursorPosition(xLeft - 14, y);
                            System.Console.Write("Yes");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Cancel");
                            Output.Console.Color.ResetColor();
                            break;
                        case Response.NO:
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Cancel");
                            System.Console.SetCursorPosition(xLeft - 14, y);
                            System.Console.Write("Yes");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 10, y);
                            System.Console.Write("No");
                            Output.Console.Color.ResetColor();
                            break;
                        case Response.YES:
                            System.Console.SetCursorPosition(xLeft - 7, y);
                            System.Console.Write("Cancel");
                            System.Console.SetCursorPosition(xLeft - 10, y);
                            System.Console.Write("No");
                            Output.Console.Color.SwapForegroundBackgroundColor();
                            System.Console.SetCursorPosition(xLeft - 14, y);
                            System.Console.Write("Yes");
                            Output.Console.Color.ResetColor();
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
            List<string> result = new();

            //Iterate over every line in the message
            foreach (string Line in message.Split('\n'))
            {
                string line = Line;
                while (true)
                {
                    string temp;
                    //If line is longer then the allowed length
                    if (line.Length > maxWidth)
                    {
                        temp = line;
                        while (temp.Length > maxWidth)
                        {
                            //Cut the line to exactly the allowed length
                            temp = line.Substring(0, maxWidth);
                            //Remove the last word
                            while (temp.Length > 0 && !char.IsWhiteSpace(temp.Last()))
                            {
                                temp = temp.Remove(temp.Length - 1);
                            }

                            if (temp.Length > 0)
                            {
                                //Remove the last whitespace
                                temp = temp.Remove(temp.Length - 1);
                            }
                            else
                            {
                                //If the whole line was one word, just cut the word
                                temp = line.Substring(0, maxWidth);
                            }
                        }
                    }
                    else    //If the line is shorter then the max allowed length, just take the whole line
                        temp = line;

                    //Remove the part of the line we took from the whole line
                    line = line.Remove(0, temp.Length);

                    //Cut line by one to prevent starting the next line with a space
                    if (line.StartsWith(' '))
                        line = line.Substring(1);

                    //Add the part of the line we took to the result
                    result.Add(temp);

                    //If there is nothing more in the current line, proceed with the next one
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
            int maxWidth = System.Console.WindowWidth - edgeDistance * 2, maxHight = System.Console.WindowHeight - edgeDistance * 2;

            if (maxWidth < 17 || maxHight < 6)
                throw new ArgumentException("Not enough space to display Message Box");

            //Replace tabulator with 4 space to fix SplitMessage function
            message = message.Replace("\t", "    ");

            List<string> messages = SplitMessage(message, maxWidth - 4);

            int x1, y1, x2, y2, readerStart = 0;
            Response selectedResponse;

            //Set Top left and bottom right point of MessageBox
            if (messages.Count == 1)
            {
                x1 = (System.Console.WindowWidth / 2) - ((messages[0].Length + 4) / 2);
                x2 = x1 + messages[0].Length + 4;
                y1 = (System.Console.WindowHeight / 2) - (7 / 2);
                y2 = y1 + 7;
            }
            else
            {
                //Get the longest line in the message
                int longestMessageLine = 0;
                foreach (string line in messages)
                {
                    if (line.Length + 4 > longestMessageLine)
                        longestMessageLine = line.Length + 4;
                }
                if (longestMessageLine < 17)
                    longestMessageLine = 17;

                x1 = (System.Console.WindowWidth / 2) - (longestMessageLine / 2);
                x2 = x1 + longestMessageLine;

                if (messages.Count + 5 <= maxHight)
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

            //Select default response
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

            DrawFrame(x1, y1, x2, y2, title);

            bool flag = true;
            while (flag)
            {
                ClearSpace(x1 + 1, y1 + 1, x2 - 1, y2 - 3);
                //Write indicator if top part of message is cut off
                if (readerStart > 0)
                {
                    System.Console.SetCursorPosition(x2 - 5, y1 + 1);
                    System.Console.Write("...");
                }
                //Write message
                for (int i = 0; i < (y2 - y1) - 6; i++)
                {
                    System.Console.SetCursorPosition(x1 + 2, y1 + i + 2);
                    if (i + readerStart >= messages.Count)
                    {
                        break;
                    }
                    System.Console.Write(messages[i + readerStart]);
                }
                //Write indicator if the bottom part of the message is cut off
                if ((y2 - y1) - 5 + readerStart <= messages.Count)
                {
                    System.Console.SetCursorPosition(x2 - 5, y2 - 4);
                    System.Console.Write("...");
                }

                DrawResponses(x2 - 1, y2 - 2, responseType, selectedResponse);

                //Get input from user
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        //Break out of loop
                        flag = false;
                        break;
                    case ConsoleKey.Escape:
                        //If escape is pressed, return a corresponding value
                        switch (responseType)
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
                        if (readerStart > 0)
                            readerStart--;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                        if (readerStart < messages.Count - 1)
                            readerStart++;
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.H:
                        //Scroll through responses
                        switch (responseType)
                        {
                            case ResponseOptions.YES_NO_CANCEL:
                                if (selectedResponse == Response.CANCEL)
                                    selectedResponse = Response.NO;
                                else if (selectedResponse == Response.NO)
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
                        //Scroll through responses
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

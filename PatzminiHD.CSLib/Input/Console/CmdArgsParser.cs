using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatzminiHD.CSLib.ExtensionMethods;

namespace PatzminiHD.CSLib.Input.Console
{
    /// <summary>
    /// A class for parsing command lines arguments
    /// </summary>
    public class CmdArgsParser
    {
        private string[] args;
        private List<(List<string> names, ArgType type)> acceptedArgs;


        /// <summary>
        /// The Types an argument can have
        /// </summary>
        public enum ArgType
        {
            /// <summary> An argument that can only be set and does not expect any arguments afterwards </summary>
            SET,
            /// <summary> An argument that expects either "true" or "flase" to come afterwards </summary>
            BOOL,
            /// <summary> An argument that expects an unsigned integer afterwards </summary>
            UINT,
            /// <summary> An argument that expects an integer afterwards </summary>
            INT,
            /// <summary> An argument that expect a double (seperator '.' or ',') afterwards </summary>
            DOUBLE,
            /// <summary> An argument that expects a string afterwards.<br/>
            ///           Strings with spaces have to be put in quotation marks </summary>
            STRING,
        }

        /// <summary>
        /// Initialise a new <see cref="CmdArgsParser"/> object with an array of args and a list of accepted args
        /// </summary>
        /// <param name="args"></param>
        /// <param name="acceptedArgs"></param>
        public CmdArgsParser(string[] args, List<(List<string> names, ArgType type)> acceptedArgs)
        {
            var duplicates = CheckDuplicates(acceptedArgs);
            if (duplicates != null)
                throw new ArgumentException($"Different arguments can not have the same name (\"{duplicates[0]}\").");
            this.args = ParseArgsInQuotes(args);
            this.acceptedArgs = acceptedArgs;
        }

        /// <summary>
        /// Parse the arguments that were given to the object in the constructor
        /// </summary>
        /// <returns>A Dictionary with<br/>
        ///          the key being the List of names given to the constructor as acceptedArgs<br/>
        ///          and<br/>
        ///          the value being a tuple of Type and object, with the type specifying the type of the object</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public Dictionary<List<string>, (Type? type, object? value)> Parse()
        {
            Dictionary<List<string>, (Type? type, object? value)> parsedArgs = new();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Length > 2 && !args[i].StartsWith("--"))
                    throw new ArgumentException($"Multi letter arguments have to start with a double dash ('--'). (argument: {args[i]})");

                if(args[i].Length == 2 && !args[i].StartsWith("-"))
                    throw new ArgumentException($"Single letter arguments have to start with single dash ('-'). (argument: {args[i]})");

                if (args[i].Length == 1)
                    throw new ArgumentException($"All arguments have to start with a dash ('-'). (argument: {args[i]})");

                for (int j = 0; j < acceptedArgs.Count; j++)
                {
                    if ((args[i].Length > 2 && acceptedArgs[j].names.Contains(args[i].Substring(2))) ||
                        (args[i].Length == 2 && acceptedArgs[j].names.Contains(args[i].Substring(1))))
                    {
                        Type? parsedArgType = null;
                        object? parsedArgValue = null;

                        switch (acceptedArgs[j].type)
                        {
                            case ArgType.SET:
                                //Nothing to do, just has to be set
                                break;
                            case ArgType.BOOL:
                                parsedArgType = typeof(bool);
                                if (args.Length - 1 <= i)
                                    throw new ArgumentException($"Expected 'true' or 'false' after argument '{args[i]}'");

                                if (args[i + 1].ToLower() == "true")
                                    parsedArgValue = true;
                                else if (args[i + 1].ToLower() == "false")
                                    parsedArgValue = false;
                                else
                                    throw new ArgumentException($"Argument '{args[i]}' can only be followed with 'true' or 'false'");
                                i++;
                                break;
                            case ArgType.UINT:
                                parsedArgType = typeof(uint);
                                if (args.Length - 1 <= i)
                                    throw new ArgumentException($"Expected value after argument '{args[i]}'");

                                if (!uint.TryParse(args[i + 1], out uint uintTmp))
                                    throw new ArgumentException($"Argument '{args[i]}' only supports uint values");
                                parsedArgValue = uintTmp;
                                i++;

                                break;
                            case ArgType.INT:
                                parsedArgType = typeof(int);
                                if (args.Length - 1 <= i)
                                    throw new ArgumentException($"Expected value after argument '{args[i]}'");

                                if (!int.TryParse(args[i + 1], out int intTmp))
                                    throw new ArgumentException($"Argument '{args[i]}' only supports int values");
                                parsedArgValue = intTmp;
                                i++;

                                break;
                            case ArgType.DOUBLE:
                                parsedArgType = typeof(double);
                                if (args.Length - 1 <= i)
                                    throw new ArgumentException($"Expected value after argument '{args[i]}'");

                                //Accept comma and dot decimal seperators
                                args[i] = args[i].Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                                args[i] = args[i].Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                                if (!double.TryParse(args[i + 1], out double doubleTmp))
                                    throw new ArgumentException($"Argument '{args[i]}' only supports double values");
                                parsedArgValue = doubleTmp;
                                i++;

                                break;
                            case ArgType.STRING:
                                parsedArgType = typeof(string);
                                if (args.Length - 1 <= i)
                                    throw new ArgumentException($"Expected value after argument '{args[i]}'");

                                parsedArgValue = args[i + 1];
                                i++;

                                break;
                            default:
                                throw new Exception("The specified ArgType is not implemented");
                        }

                        if (parsedArgs.ContainsKey(acceptedArgs[j].names))
                            throw new ArgumentException($"Argument '{args[i]}' has already been specified");
                        parsedArgs.Add(acceptedArgs[j].names, (parsedArgType, parsedArgValue));
                        break;
                    }

                    if(j >= acceptedArgs.Count - 1)
                        throw new ArgumentException($"Argument '{args[i]}' is not a valid argument");
                }
            }

            return parsedArgs;
        }

        private List<string>? CheckDuplicates(List<(List<string> arguments, ArgType argType)> values)
        {
            List<string> allArgs = new List<string>();
            foreach (var value in values)
            {
                allArgs.AddRange(value.arguments);
            }
            var duplicateArgs = allArgs.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            if(duplicateArgs.Any())
                return duplicateArgs;

            return null;
        }

        private string[] ParseArgsInQuotes(string[] args)
        {
            string tmp = "";
            List<string> parsedArgs = new();
            foreach (string arg in args)
            {
                tmp += arg + " ";
            }

            if (!tmp.Contains("\"") && !tmp.Contains("'"))
                return args;    //No parsing needed

            char quoteStart = ' ';
            string tmpArg = "";
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] == quoteStart)
                {
                    parsedArgs.Add(tmpArg);
                    tmpArg = "";
                    quoteStart = ' ';

                    if (tmp[i] == '"' || tmp[i] == '\'')
                        i++;

                    continue;
                }

                if ((tmp[i] == '"' || tmp[i] == '\'') && quoteStart == ' ')
                {
                    quoteStart = tmp[i];
                    continue;
                }

                tmpArg += tmp[i];
            }

            if(quoteStart != ' ')
                throw new ArgumentException("Opened Quotes not closed in argument");

            return parsedArgs.ToArray();

        }
    }
}

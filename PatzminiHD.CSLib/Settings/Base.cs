using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Settings
{
    /// <summary>
    /// Base for saving Application Settings. This class is abstract
    /// </summary>
    public abstract class Base
    {
        /// <summary> The name of the application </summary>
        public string? ApplicationName;
        /// <summary> The Version of the application </summary>
        public string? ApplicationVersion;

        /// <summary> The Path of the settings file </summary>
        public string ApplicationSettingsFilePath
        {
            get
            {
                if (ApplicationName == null)
                    throw new Exception(nameof(ApplicationName) + " has to be set");

                return Path.Combine(Environment.Get.ProgramSettingsDirectory(ApplicationName), "Settings.xml");
            }
        }

        /// <summary> Serialize Settings to a file </summary>
        /// <exception cref="Exception"> When <see cref="ApplicationName"/> is null </exception>
        public void Serialize()
        {
            if (ApplicationName == null || ApplicationName == "")
                throw new Exception(nameof(ApplicationName) + " has to be set");

            if (!Directory.Exists(Environment.Get.ProgramSettingsDirectory(ApplicationName)))
                Directory.CreateDirectory(Environment.Get.ProgramSettingsDirectory(ApplicationName));


            using (StreamWriter write = new(ApplicationSettingsFilePath))
            {
                System.Xml.Serialization.XmlSerializer serializer = new(this.GetType());
                serializer.Serialize(write, this);
            }
        }
        /// <summary> Deserialize Settings from a file </summary>
        /// <typeparam name="T"> Type of settings class </typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"> When deserializing failed </exception>
        public T Deserialze<T>()
        {
            if (ApplicationName == null || ApplicationName == "")
                throw new Exception(nameof(ApplicationName) + " has to be set");

            using (Stream reader = new FileStream(ApplicationSettingsFilePath, FileMode.Open))
            {
                System.Xml.Serialization.XmlSerializer serializer = new(typeof(T));
                var deserializedObject = serializer.Deserialize(reader);

                if (deserializedObject == null)
                    throw new Exception("Deserialized object was null");

                var parsedObject = (T)deserializedObject;

                if (parsedObject == null)
                    throw new Exception("Parsed object was null");

                return parsedObject;
            }
        }

        /// <summary>
        /// Compares two version strings in the format "vX.Y.Z" (the 'v' at the start can be omitted) and returns true if v1 is greater than v2
        /// </summary>
        /// <param name="v1">Version string 1</param>
        /// <param name="v2">Version string 2</param>
        /// <returns>True if v1 is greater than v2, otherwise false</returns>
        /// <exception cref="ArgumentException">If Parsing of version string failed</exception>
        public static bool IsGreaterVersion(string v1, string v2)
        {
            if (v1.ToLower().StartsWith('v'))
                v1 = v1.Substring(1);
            if (v2.ToLower().StartsWith('v'))
                v2 = v2.Substring(1);
            string[] v1Parts = v1.Split('.');
            string[] v2Parts = v2.Split('.');
            if (v1Parts.Length < 1 || v2Parts.Length < 1)
            {
                throw new ArgumentException("Version strings have to have at least one number");
            }
            List<uint> v1Numbers = new();
            List<uint> v2Numbers = new();
            foreach (string part in v1Parts)
            {
                if (uint.TryParse(part, out uint number))
                {
                    v1Numbers.Add(number);
                }
                else
                {
                    throw new ArgumentException("Could not parse version 1");
                }
            }
            foreach (string part in v2Parts)
            {
                if (uint.TryParse(part, out uint number))
                {
                    v2Numbers.Add(number);
                }
                else
                {
                    throw new ArgumentException("Could not parse version 1");
                }
            }
            for (int i = 0; i < v1Numbers.Count; i++)
            {
                if (v2Numbers.Count <= i)
                {
                    return true;
                }
                if (v1Numbers[i] > v2Numbers[i])
                {
                    return true;
                }
                else if (v2Numbers[i] > v1Numbers[i])
                {
                    return false;
                }
            }
            return false;
        }
    }
}

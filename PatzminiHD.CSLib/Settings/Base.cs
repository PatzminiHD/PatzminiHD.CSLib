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
    }
}

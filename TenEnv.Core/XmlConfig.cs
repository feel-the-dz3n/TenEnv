using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetXmlConfig
{
    [Serializable]
    public class XmlConfig
    {
        [XmlIgnore]
        FileInfo File;

        [XmlIgnore]
        public bool SaveInProgress { get; private set; }

        [XmlIgnore]
        private bool _saveOnExit = false;

        [XmlIgnore]
        public bool SaveOnExit
        {
            get => _saveOnExit;
            set
            {
                if (_saveOnExit == value) return;

                _saveOnExit = value;

                if (!value)
                {
                    AppDomain.CurrentDomain.ProcessExit -= ProcessExitSave;
                }
                else
                {
                    AppDomain.CurrentDomain.ProcessExit += ProcessExitSave;
                }
            }
        }

        private void ProcessExitSave(object sender, EventArgs e) => Save();

        public void SaveAsync() => new Thread(Save).Start();

        public void Save()
        {
            if (SaveInProgress)
            {
                while (SaveInProgress)
                    Thread.Sleep(100);

                return;
            }

            SaveInProgress = true;

            try
            {
                XmlSerializer ser = new XmlSerializer(this.GetType());

                using (StreamWriter stream = new StreamWriter(File.FullName))
                    ser.Serialize(stream, this);
            }
            finally
            {
                SaveInProgress = false;
            }
        }

        public static T Load<T>(string file, bool saveOnExit = false) where T : XmlConfig
        {
            T config;
            FileInfo File = new FileInfo(file);

            if (!File.Exists)
            {
                config = (T)Activator.CreateInstance(typeof(T));
                config.File = File;
                config.Save();
            }

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StreamReader stream = new StreamReader(File.FullName))
                config = (T)ser.Deserialize(stream);
            
            config.File = File;
            config.SaveOnExit = saveOnExit;

            return config;
        }

        public XmlConfig(string file, bool saveOnExit = false)
        {
            File = new FileInfo(file);
            this.SaveOnExit = saveOnExit;
        }

        public XmlConfig() { }
    }
}

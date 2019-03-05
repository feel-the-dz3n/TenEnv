using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TenEnv.Core
{
    [Serializable]
    public class XmlConfig
    {
        [XmlIgnore]
        public const string ConfigFileName = "MwoPowerBotConfig.xml";

        public bool Tested = false;
        public System.Windows.Media.Color MainColor = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);

        public string LastLoadedDate = "Unknown Date";
        public string LastSavedDate = "Unknown Date";

        public XmlConfig() { }


        public static XmlConfig LoadConfig()
        {
            if (!File.Exists(ConfigFileName))
                return new XmlConfig();

            XmlConfig Config = null;
            XmlSerializer ser = new XmlSerializer(typeof(XmlConfig));

            using (StreamReader stream = new StreamReader(ConfigFileName))
            {
                Config = (XmlConfig)ser.Deserialize(stream);
            }

            Config.LastLoadedDate = DateTime.Now.ToString();

            return Config;
        }

        public static void Delete()
        {
            if(File.Exists(ConfigFileName))
                File.Delete(ConfigFileName);
        }

        public void SaveSettings()
        {
            LastSavedDate = DateTime.Now.ToString();

            XmlSerializer ser = new XmlSerializer(typeof(XmlConfig));

            using (StreamWriter stream = new StreamWriter(ConfigFileName))
            {
                ser.Serialize(stream, this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DotNetXmlConfig;

namespace TenEnv.Core
{
    public class Settings
    {
        public static BaseSettings Base = XmlConfig.Load<BaseSettings>("TenEnvConfiguration.xml", true);
    }
}

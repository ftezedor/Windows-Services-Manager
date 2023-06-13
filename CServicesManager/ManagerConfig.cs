using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceProcess;

namespace Service.Manager
{
    [XmlRoot(ElementName = "Config")]
    public class Config
    {
        private String serviceName,
            workersFolder,
            stagingFolder,
            rejectsFolder;

        
        public Config()
        {
        }

        [XmlElement(ElementName = "ServiceName")]
        public String ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }

        [XmlElement(ElementName = "WorkersFolder")]
        public String WorkersFolder
        {
            get { return workersFolder; }
            set { workersFolder = value; }
        }

        [XmlElement(ElementName = "StagingFolder")]
        public String StagingFolder
        {
            get { return stagingFolder; }
            set { stagingFolder = value; }
        }

        [XmlElement(ElementName = "RejectsFolder")]
        public String RejectsFolder
        {
            get { return rejectsFolder; }
            set { rejectsFolder = value; }
        }

        public void Save(String file)
        {
            XmlSerializer x = new XmlSerializer(this.GetType());
            using (StreamWriter sw = new StreamWriter(file))
            {
                x.Serialize(sw, this);
            }
        }

        public static Config load(String file)
        {
            XmlSerializer x = new XmlSerializer(typeof(Config));
            using (Stream reader = new FileStream(file, FileMode.Open))
            {
                return (Config)x.Deserialize(reader);
            }
        }
    }

    class ManagerConfig
    {
        private static String configFile = null;

        private static Config config = null;

        private ManagerConfig()
        {
        }

        public static void Initialize(String file)
        {
            if (config != null)
                throw new InvalidOperationException("Component is already initialized");
            config = Config.load(file);
            configFile = file;
        }

        private static String Curate(String path)
         {
            if (!path.StartsWith(".\\")) return path;
            return Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location
                ) + path.Replace(".\\", "\\");
        }

        public static String ConfigurationFile
        {
            get
            {
                if (config == null) return null;
                return configFile;
            }
        }

        public static String ServiceName
        {
            get 
            {
                if (config == null) return null;
                return config.ServiceName; 
            }
        }

        public static String WorkersFolder
        {
            get
            {
                if (config == null) return null; 
                return Curate(config.WorkersFolder);
            }
        }

        public static String StagingFolder
        {
            get
            {
                if (config == null) return null;
                return Curate(config.StagingFolder);
            }
        }

        public static String RejectsFolder
        {
            get
            {
                if (config == null) return null; 
                return Curate(config.RejectsFolder);
            }
        }
    }
}

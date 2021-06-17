using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Markup;

namespace Lab3_WPF_
{
 
    [Serializable]
    public class Settings
    {
      
        [DataMember]
        public List<Line> settingsLines = new List<Line>();
        
        [DataMember]
        public List<Point> settingsPoints = new List<Point>();
     
        [DataMember]
        public int settingsCounter = 0;


        public Settings()
        {
            settingsPoints = MainWindow.getPoints();
            settingsLines = MainWindow.getLines();
            settingsCounter = MainWindow.getCounter();
        }


        public static Settings GetSettings()
        {
            Settings settings = null;
            string filename = "Save.xml";

            //проверка наличия файла
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(Settings));
                    settings = (Settings)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else
            {
                settings = new Settings();
            }

            return settings;
        }




        public void Save()
        {
            const string filename = "Save.xml";


            //if (File.Exists(filename))
            //{
            //    File.Delete(filename);

            //}

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                XmlSerializer xser = new XmlSerializer(typeof(Settings));
                xser.Serialize(fs, this);
                fs.Close();
            }

            //if (!File.Exists(filename))
            //{
            //    throw new FileNotFoundException(nameof(filename));
            //}

            //string a = XamlWriter.Save(settingsCounter);
            //Console.WriteLine(a);
           // XmlTextWriter xw = new XmlTextWriter(filename, Encoding.UTF8);
           // xw.Formatting = Formatting.Indented;

           // XmlSerializer s = new XmlSerializer(typeof(MainWindow));

           // //XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xw);
            

           // s.Serialize(Console.Out, this);
           //// DataContractSerializer ser = new DataContractSerializer(typeof(Settings));
           // //ser.WriteObject(writer, this);
           // //writer.Close();
           // xw.Close();
           // Console.WriteLine("Serialization complete!");


        }



    }


}

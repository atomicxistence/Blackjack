using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace Blackjack
{
    public static class FileIO
    {
        internal static string SavePath()
        {
            string savePath = AppDomain.CurrentDomain.BaseDirectory + "/highrollers.xml";
            return savePath;
        }
        internal static void SaveFile(List<HighRollers> highRollers)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HighRollers>));
            using (StreamWriter writer = new StreamWriter(SavePath(), false))
            {
                serializer.Serialize(writer.BaseStream, highRollers);
            }
        }
        internal static List<HighRollers> LoadSaveFile(List<HighRollers> highRollers)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HighRollers>));
            using (StreamReader reader = new StreamReader(SavePath()))
            {
                highRollers = (List<HighRollers>)serializer.Deserialize(reader);
                return highRollers;
            }
        }
        internal static void VerifySaveFile()
        {
            if (!File.Exists(SavePath()))
            {
                File.CreateText(SavePath());
                SaveFile(HighRollers.DefaultHighRollers());
            }
        }
    }
}

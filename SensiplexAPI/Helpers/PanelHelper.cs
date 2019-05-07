
using Luminex.Models.Implementations;
using Luminex.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Luminex.Spartan.Core.Helpers
{
    public static class PanelHelper
    {
        #region Fields

        readonly static string m_DirName;

        #endregion

        #region Constructor

        static PanelHelper()
        {
            //m_DirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "\\DataFiles\\Panels\\") ;
            m_DirName = @"C:\Users\kabburi\source\repos\SensiplexAPI\SensiplexAPI\DataFiles\Panels\";
            if (!Directory.Exists(m_DirName))
            {
                Directory.CreateDirectory(m_DirName);
            }
        }
        #endregion

        #region Public Methods

        public static IPanelData GetPanelByName(string plateTemplateId)
        {
            IPanelData data;
            try
            {
                data = ReadFromFile(plateTemplateId);
            }
            catch (System.Exception)
            {
                throw;
            }
            return data;
        }

        public static string[] RetrieveAllPanelIds()
        {
            return GetFileNames();
        }

        public static bool SavePanel(IPanelData data)
        {
            return WriteToFile(data);
        }

        #endregion

        #region Private Methods

        private static string[] GetFileNames()
        {
            try
            {
                if (!Directory.Exists(m_DirName))
                {
                    return new string[0];
                }
                return Directory.GetFiles(m_DirName).Select(file => (file != null) ? Path.GetFileNameWithoutExtension(file) : "").ToArray();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static PanelData ReadFromFile(string panelid)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new BeadConverter());
            PanelData Panel = new PanelData();
            try
            {
				string sFullPath = System.IO.Path.Combine(m_DirName, panelid + ".json");
                if (File.Exists(sFullPath))
                {
                    using (StreamReader r = new StreamReader(sFullPath))
                    {
                        string json = r.ReadToEnd();
                        Panel = JsonConvert.DeserializeObject<PanelData>(json, settings);
                    }
                }
				else
				{
					System.Diagnostics.Debug.WriteLine(string.Format("{0} does not exist!", sFullPath));
				}
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return Panel;
        }

        private static bool WriteToFile(IPanelData data)
        {
            bool result = false;
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                System.IO.File.WriteAllText(m_DirName +"\\" + data.PanelId + ".json", json);
                result = true;
            }
            catch (System.Exception)
            {
                throw;
            }
            return result;
        }

        #endregion
    }

    public class BeadConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IBeadData);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, typeof(BeadData));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

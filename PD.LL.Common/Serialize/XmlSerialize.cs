using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PD.LYY.UtilityLib.Serialize
{
    public class XmlSerialize : SerializeBase
    {
        public override string FileExtension { get => "xml"; }

        public override T Deserialize<T>(string data)
        {
          if(string.IsNullOrEmpty(data)) return default(T);

            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(stream);
            }
        }

      

        public override string Serialize<T>(T data)
        {
            if (data == null) return string.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
               XmlSerializer serializer = new XmlSerializer(typeof(T));
                 serializer.Serialize(stream, data);
                return Encoding.UTF8.GetString(stream.GetBuffer());
            }
        }
    }
}

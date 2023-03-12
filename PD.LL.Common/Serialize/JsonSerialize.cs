using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PD.LYY.UtilityLib.Serialize
{
    public class JsonSerialize : SerializeBase
    {
        public override string FileExtension { get => "json"; }

        public override T Deserialize<T>(string data)
        {
            if(string.IsNullOrEmpty(data)) return default(T);

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        public override string Serialize<T>(T data)
        {
            if (data == null ) return string.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, data);
                stream.Flush();

                return UTF8Encoding.UTF8.GetString(stream.GetBuffer());
            }
        }
    }
}

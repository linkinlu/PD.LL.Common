using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD.LYY.UtilityLib.Serialize
{
    public class CsvSerialize : ISerializeBase
    {
        public  string FileExtension { get => "csv"; }

        public List<char> StartOfContent => new List<char>();

        public  T Deserialize<T>(string data)
        {
            if(string.IsNullOrEmpty(data)) return default(T);

            return data.CsvToObj<T>();


        }

      
        public  string Serialize<T>(T data)
        {
          
            return data.ToCsvString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PD.LYY.UtilityLib.Serialize
{
    public class CsvSerialize : SerializeBase
    {
        public override string FileExtension { get => "csv"; }

        public override T Deserialize<T>(string data)
        {
            if(string.IsNullOrEmpty(data)) return default(T);

            return default(T);


        }

      
        public override string Serialize<T>(T data)
        {
            return default(string);
        }
    }
}

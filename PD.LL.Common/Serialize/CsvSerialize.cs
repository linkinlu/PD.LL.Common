using System;
using System.Collections.Generic;
using System.Text;
using PD.LL.Common.Extension;

namespace PD.LL.Common.Serialize
{
    public class CsvSerialize : SerializeBase
    {
        public override string FileExtension { get => "csv"; }

        public  override T Deserialize<T>(string data)
        {
            if(string.IsNullOrEmpty(data)) return default(T);

            return data.CsvToObj<T>();


        }

      
        public override string Serialize<T>(T data)
        {
          
            return data.ToCsvString();
        }
    }
}

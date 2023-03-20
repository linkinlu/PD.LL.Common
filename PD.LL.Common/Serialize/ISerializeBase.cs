using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.LYY.UtilityLib.Serialize
{
    public  interface ISerializeBase
    {
          List<char> StartOfContent { get; }
        //public abstract string ContentType { get; set; }
          string FileExtension { get; }

          T Deserialize<T>(string data);

          string Serialize<T>(T data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.LL.Common.Serialize
{
    public abstract class SerializeBase
    {
        //public abstract string ContentType { get; set; }
        public abstract string FileExtension { get; }

        public abstract T Deserialize<T>(string data);

        public abstract string Serialize<T>(T data);
    }
}

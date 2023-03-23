using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace PD.LYY.UtilityLib.Serialize
{
    public class SerializeManageWrapper
    {
       // private List<SerializeBase> serializeBases= new List<SerializeBase>();

        
        static SerializeManageWrapper() {
          //  serializeBases.AddRange(ReflectionUtils.FindImplementTypes(typeof(SerializeBase)).Cast<SerializeBase>().ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="type">json,xml,csv</param>
        /// <returns></returns>
        public static string Serilize<T>(T data,string type="json" )
        {
            List<ISerializeBase> allImplementTypes = GetImplementSerilizeBases();
            return allImplementTypes.FirstOrDefault(g => g.FileExtension.Equals(type))?.Serialize(data);
        }


        public static T Deserilize<T>(string data,string type)
        {
            if (data.IsEmpty()) return default(T);
            List<ISerializeBase> allImplementTypes = GetImplementSerilizeBases();
            ISerializeBase serilizeType = null;
            if (!type.IsEmpty())
            {
                serilizeType = allImplementTypes.FirstOrDefault(g => g.FileExtension.Equals(type));
            }
            else
            {
                serilizeType = allImplementTypes.FirstOrDefault(g => g.StartOfContent.Contains(data.First()));
            }
            if (serilizeType != null)
            {
                return serilizeType.Deserialize<T>(data);
            }
            else
            {
                return default(T);
            }
        }

        private static List<ISerializeBase> GetImplementSerilizeBases()
        {
            return ReflectionUtils.FindImplementTypes<ISerializeBase>().ToList();
        }

    }
}

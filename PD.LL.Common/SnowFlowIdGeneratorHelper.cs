using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Yitter.IdGenerator;

namespace PD.LYY.UtilityLib
{
    public class SnowFlowIdGeneratorHelper
    {
        static SnowFlowIdGeneratorHelper()
        {
            
            //currently workid is fixed
            var worker =  new IdGeneratorOptions();
            YitIdHelper.SetIdGenerator(worker);
        }


        public static long GetNewId()
        {
            return YitIdHelper.NextId();
        }
       
    }

    public class PcMacUtility
    {
        public static List<NetworkInterface> GetMacAddress() {
            return NetworkInterface.GetAllNetworkInterfaces().ToList();

        }
    }
}
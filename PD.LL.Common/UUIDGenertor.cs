using PD.LYY.UtilityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Yitter.IdGenerator;

namespace PD.LYY.UtilityLib
{

    public abstract class UUIDGenertor<TKey>  where TKey :struct 
    {
      public  abstract TKey GetNewId();

    }



    public class SnowFlowIdGenerator : UUIDGenertor<long>
    {
        static SnowFlowIdGenerator()
        {

            //currently workid is fixed
            var worker = new IdGeneratorOptions();
            YitIdHelper.SetIdGenerator(worker);
        }

        public override long GetNewId()
        {
            return YitIdHelper.NextId();
        }
    }


    public class GuidIdGenerator : UUIDGenertor<Guid>
    {
        public override Guid GetNewId()
        {

            return Guid.NewGuid();
        }
    }




    public class PcMacUtility
    {
        public static List<NetworkInterface> GetMacAddress() {
            return NetworkInterface.GetAllNetworkInterfaces().ToList();

        }
    }
}
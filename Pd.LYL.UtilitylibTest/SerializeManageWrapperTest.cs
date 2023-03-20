using PD.LYY.UtilityLib.Serialize;
using PD.LYY.UtilityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pd.LYL.UtilitylibTest.Tests;
using System.Runtime.Serialization;

namespace Pd.LYL.UtilitylibTest
{
    public class SerializeManageWrapperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var bb = SerializeManageWrapper.Serilize(new List<Temp1>() { new Temp1() { A = "a", B = 2 }, new Temp1() { A = "b" } });
            var cc = SerializeManageWrapper.Serilize(new List<Temp1>() { new Temp1() { A = "a", B = 2 }, new Temp1() { A = "b" } }, type: "xml");
            var dd = SerializeManageWrapper.Serilize(new List<Temp1>() { new Temp1() { A = "a", B = 2 }, new Temp1() { A = "b" } }, type: "csv");


        }
    }
    [Serializable]
    [DataContract]
    public class Temp1 {
        [DataMember]
        public string A { get; set; }
        [DataMember]
        public int? B { get; set; }
    }
}

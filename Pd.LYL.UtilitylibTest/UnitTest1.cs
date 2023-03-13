using PD.LYY.UtilityLib;
using PD.LYY.UtilityLib.Serialize;
using System.Diagnostics.CodeAnalysis;

namespace Pd.LYL.UtilitylibTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var a = FileUtils.ReadToString("D:\\1.txt");
            var b = FileUtils.ReadToString("D:\\2.txt");

            var base1 = new JsonSerialize().Deserialize<List<Root>>(a);
            var base2 = new JsonSerialize().Deserialize<List<Root>>(b);
            var bb = base1.Count == base2.Count;
            var alreadyExisted = new List<string>();
            foreach (var item in base2)
            {
                var found = base1.FirstOrDefault(g => g.systemProfile.name == item.systemProfile.name);
                alreadyExisted.Add(found?.systemProfile.name);
            }

            var not = base2.Where(g=> !alreadyExisted.Contains(g.systemProfile.name));



         
            

        public class EqualityComparerRoot : IEqualityComparer<Root>
        {
            public bool Equals(Root? x, Root? y)
            {
                if (x.systemProfile.name == y.systemProfile.name) return true;
            return false;
            }

            public int GetHashCode([DisallowNull] Root obj)
            {
                return obj.systemProfile.name.GetHashCode();
            }
        }


        public class Root
        {
            public SystemProfile systemProfile { get; set; }
            public object systemProfileValue { get; set; }
        }

        public class SystemProfile
        {
            public string moduleID { get; set; }
            public string propertyDesc { get; set; }
            public string propertyOptions { get; set; }
            public bool isExportable { get; set; }
            public bool canBeInherited { get; set; }
            public int propertyType { get; set; }
            public int isHidden { get; set; }
            public string orderingPos { get; set; }
            public string domain { get; set; }
            public string uniqueID { get; set; }
            public string name { get; set; }
            public string value { get; set; }
        }
    }
}

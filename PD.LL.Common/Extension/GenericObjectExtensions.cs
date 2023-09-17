using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using CsvHelper;
using PD.LL.Common;
using PD.LYY.UtilityLib;

namespace PD.LL.Common.Extension
{

    public static class GenericObjectExtensions
    {

        public static T Check<T>(this T Object, Predicate<T> Predicate, T DefaultValue = default(T))
        {

            return Predicate(Object) ? Object : DefaultValue;
        }

        public static T Check<T>(this T Object, T DefaultValue = default(T))
        {
            return Object.Check(x => x != null, DefaultValue);
        }


        public static DataTable ToDataTable<T>(this T[] list)
        {
            DataTable table = new DataTable();

            PropertyInfo[] propertys = typeof(T).GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                Type pt = pi.PropertyType;
                if ((pt.IsGenericType) && (pt.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    pt = pt.GetGenericArguments()[0];
                }

                table.Columns.Add(new DataColumn(pi.Name, pt));
            }

            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }

                    object[] array = tempList.ToArray();
                    table.LoadDataRow(array, true);
                }
            }

            return table;
        }

        public static string ToCsvString<T>(this T obj)
        {
            var isCollection = ReflectionUtils.IsCollection(typeof(T));
            if (isCollection)
            {
                var data = ((IEnumerable)obj);
                var aa = new int[6];
                using (MemoryStream ms = new MemoryStream())
                {

                    using (var writer = new StreamWriter(ms))
                    using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(data);
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                } //return ((IEnumerable)obj).ToDataTable().ToDelimitedFile();
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    using (var writer = new StreamWriter(ms))
                    using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(new List<T>() { obj });
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                // return new List<T>() { obj }.ToDataTable().ToDelimitedFile();;
            }


        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PD.LYY.UtilityLib.extension
{
    public static class DataTableExtension
    {
        public static string ToDelimitedFile(this DataTable dt, string Delimiter = ",")
        {

            if (null == dt)
                return string.Empty;

            StringBuilder csvText = new StringBuilder();
            StringBuilder csvrowText = new StringBuilder();
            foreach (DataColumn dc in dt.Columns)
            {
                csvrowText.Append(Delimiter);
                csvrowText.Append(dc.ColumnName);
            }
            csvText.AppendLine(csvrowText.ToString().Substring(1));

            foreach (DataRow dr in dt.Rows)
            {
                csvrowText = new StringBuilder();
                foreach (DataColumn dc in dt.Columns)
                {
                    csvrowText.Append(Delimiter);
                    csvrowText.Append(dr[dc.ColumnName].ToString().Replace(',', ' '));
                }
                csvText.AppendLine(csvrowText.ToString().Substring(1));
            }

          return csvText.ToString();
        }


        public static List<T> ToEntities<T>(this DataTable table) where T : new()
        {
            List<T> entities = new List<T>();
            if (table == null)
                return null;
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            Type newType = item.PropertyType;
                        
                            if (newType.IsGenericType
                                    && newType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                          
                                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(newType);
                                newType = nullableConverter.UnderlyingType;
                            }
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], newType), null);
                        }
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PD.LYY.UtilityLib.extension
{
    public class DataTableExtension
    {
        //public static Utilities.IO.FileFormats.Delimited.Delimited ToDelimitedFile(this DataTable Data, string Delimiter = ",")
        //{
        //    var ReturnValue = new Utilities.IO.FileFormats.Delimited.Delimited("", Delimiter);
        //    if (Data == null)
        //        return ReturnValue;
        //    var TempRow = new FileFormats.Delimited.Row(Delimiter);
        //    foreach (DataColumn Column in Data.Columns)
        //    {
        //        TempRow.Add(new Utilities.IO.FileFormats.Delimited.Cell(Column.ColumnName));
        //    }
        //    ReturnValue.Add(TempRow);
        //    foreach (DataRow Row in Data.Rows)
        //    {
        //        TempRow = new Utilities.IO.FileFormats.Delimited.Row(Delimiter);
        //        for (int x = 0; x < Data.Columns.Count; ++x)
        //        {
        //            TempRow.Add(new Utilities.IO.FileFormats.Delimited.Cell(Row.ItemArray[x].ToString()));
        //        }
        //        ReturnValue.Add(TempRow);
        //    }
        //    return ReturnValue;
        //}
    }
}

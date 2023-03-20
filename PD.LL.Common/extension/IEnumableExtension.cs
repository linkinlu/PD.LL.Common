using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IEnumableExtension
{
  
    public static bool IsNotEmpty<T>(this IEnumerable<T> data)
    {
        return data != null && data.Any();
    }


    public static string Join<T>(this IEnumerable<T> values, string quotes = "", string separator = ",")
    {
        if (values == null)
            return string.Empty;
        var result = new StringBuilder();
        foreach (var each in values)
            result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
        return result.ToString().RemoveEnd(separator);
    }

    public static IEnumerable<T> Concat<T>(this IEnumerable<T> data, IEnumerable<T> others)
    {
        if (data.IsNotEmpty() && others.IsNotEmpty())
        {
            return data.Concat(others);
        }
        else
        {
            return data;

        }
    }

    public static Target[] ToArray<Source, Target>(this IEnumerable<Source> List, Func<Source, Target> ConvertingFunction)
    {
        return List.ForEach(ConvertingFunction).ToArray();
    }


    public static DataTable ToDataTable(this IEnumerable List, params string[] Columns)
    {
        var ReturnValue = new DataTable();
        ReturnValue.Locale = CultureInfo.CurrentCulture;
        int Count = 0;
        var i = List.GetEnumerator();
        while (i.MoveNext())
            ++Count;
        if (List == null || Count == 0)
            return ReturnValue;
        var ListEnumerator = List.GetEnumerator();
        ListEnumerator.MoveNext();
        var Properties = ListEnumerator.Current.GetType().GetProperties();
        if (Columns.Length == 0)
            Columns = Properties.ToArray(x => x.Name);
        Columns.ForEach(x => ReturnValue.Columns.Add(x, Properties.FirstOrDefault(z => z.Name == x).PropertyType));
        object[] Row = new object[Columns.Length];
        foreach (object Item in List)
        {
            for (int x = 0; x < Row.Length; ++x)
            {
                Row[x] = Properties.FirstOrDefault(z => z.Name == Columns[x]).GetValue(Item, new object[] { });
            }
            ReturnValue.Rows.Add(Row);
        }
        return ReturnValue;
    }

    public static IEnumerable<T> Distinct<T>(this IEnumerable<T> Enumerable, Func<T, T, bool> Predicate)
    {
        var Results = new List<T>();
        foreach (T Item in Enumerable)
        {
            bool Found = false;
            foreach (T Item2 in Results)
            {
                if (Predicate(Item, Item2))
                {
                    Found = true;
                    break;
                }
            }
            if (!Found)
                Results.Add(Item);
        }
        return Results;
    }


      public static IEnumerable<T> ElementsBetween<T>(this IEnumerable<T> List, int Start, int End)
      {
          if (!List.IsNotEmpty())
              return List;


          if (End > List.Count())
              End = List.Count();


          if (Start < 0)
              Start = 0;


          var ReturnList = new System.Collections.Generic.List<T>();
          for (int x = Start; x < End; ++x)
              ReturnList.Add(List.ElementAt(x));


          return ReturnList;
      }


         public static IEnumerable<T> ForEachParallel<T>(this IEnumerable<T> List, Action<T> Action)
        {
            Parallel.ForEach(List, Action);
            return List;
        }

         public static IEnumerable<T> ForEach<T>(this IEnumerable<T> List, Action<T> Action)
        {
            foreach (T Item in List)
                Action(Item);
            return List;
        }

            public static IEnumerable<R> ForEach<T, R>(this IEnumerable<T> List, Func<T, R> Function)
        {
            var ReturnValues = new List<R>();
            foreach (T Item in List)
                ReturnValues.Add(Function(Item));
            return ReturnValues;
        }

         public static string ToString<T>(this IEnumerable<T> List, Func<T, string> ItemOutput = null, string Seperator = ",")
        {
            Seperator = Seperator.Check("");
            ItemOutput = ItemOutput.Check(x => x.ToString());
            var Builder = new StringBuilder();
            string TempSeperator = "";
            List.ForEach(x =>
            {
                Builder.Append(TempSeperator).Append(ItemOutput(x));
                TempSeperator = Seperator;
            });
            return Builder.ToString();
        }

}
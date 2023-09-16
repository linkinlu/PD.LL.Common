using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IEnumableExtension
{
    public static bool IsNotEmpty<T>(this IEnumerable<T> data)
    {
        return data != null && data.Any();
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
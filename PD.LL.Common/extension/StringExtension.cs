using PD.LYY.UtilityLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;

public static class StringExtension
{

    public static T CsvToObj<T>(this string str)
    {
        if (ReflectionUtils.IsGenericCollection(typeof(T)))
        {

            using (MemoryStream ms = new MemoryStream())
            {
                using (var writer = new StreamReader(ms))
                using (var csvReader = new CsvReader(writer, CultureInfo.InvariantCulture))
                {
                    return (T)csvReader.GetRecords<T>();
                }
            }
        }
        else
        {
            using (MemoryStream ms = new MemoryStream(str.ToBytes()))
            {


                using (var writer = new StreamReader(ms))
                using (var csvReader = new CsvReader(writer, CultureInfo.InvariantCulture))
                {
                    var data = csvReader.GetRecords<T>();
                    return default(T);
                }

            }

        }

    }
    public static byte[] ToBytes(this string str)
    {

        return str.IsNotEmpty() ? Encoding.UTF8.GetBytes(str) : new byte[0];

    }
    public static string MaskLeft(this string Input, int EndPosition = 4, char Mask = '#')
    {
        string Appending = "";
        for (int x = 0; x < EndPosition; ++x)
            Appending += Mask;
        return Appending + Input.Remove(0, EndPosition);
    }

    public static string MaskRight(this string Input, int StartPosition = 4, char Mask = '#')
    {
        if (StartPosition > Input.Length)
            return Input;
        string Appending = "";
        for (int x = 0; x < Input.Length - StartPosition; ++x)
            Appending += Mask;
        return Input.Remove(StartPosition) + Appending;
    }

    public static string FromBase64(this string Input, Encoding EncodingUsing)
    {
        if (string.IsNullOrEmpty(Input))
            return "";
        var TempArray = Convert.FromBase64String(Input);
        return new UTF8Encoding().GetString(TempArray);
    }


    public static string StringTemplate(this string Input, Dictionary<string, string> keyValuePairs, string prefixTemplate = "{{", string endTempalte = "}}")
    {
        if (!string.IsNullOrEmpty(Input) || keyValuePairs == null)
        {
            return Input;
        }
        foreach (var item in keyValuePairs)
        {
            Input = Input.Replace($"{prefixTemplate}{item.Key}{endTempalte}", item.Value);
        }

        return Input;
    }

    public static string Keep(this string Input, string Filter)
    {
        if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(Filter))
            return "";
        var TempRegex = new Regex(Filter);
        var Collection = TempRegex.Matches(Input);
        var Builder = new StringBuilder();
        foreach (Match Match in Collection)
            Builder.Append(Match.Value);
        return Builder.ToString();
    }
    public static string Keep(this string Input, StringFilter Filter)
    {
        if (string.IsNullOrEmpty(Input))
            return "";
        return Input.Keep(BuildFilter(Filter));
    }

    public static string Reverse(this string Input)
    {
        return new string(Input.Reverse<char>().ToArray());
    }

    public static string Center(this string Input, int Length, string Padding = "*")
    {
        if (string.IsNullOrEmpty(Input))
            Input = "";
        string Output = "";
        for (int x = 0; x < (Length - Input.Length) / 2; ++x)
        {
            Output += Padding[x % Padding.Length];
        }
        Output += Input;
        for (int x = 0; x < (Length - Input.Length) / 2; ++x)
        {
            Output += Padding[x % Padding.Length];
        }
        return Output;
    }

    public static string Remove(this string Input, string Filter)
    {
        if (string.IsNullOrEmpty(Input) || string.IsNullOrEmpty(Filter))
            return Input;
        return new Regex(Filter).Replace(Input, "");
    }

    /// <summary>
    /// Removes everything that is in the filter text from the input.
    /// </summary>
    /// <param name="Input">Input text</param>
    /// <param name="Filter">Predefined filter to use (can be combined as they are flags)</param>
    /// <returns>Everything not in the filter text.</returns>
    public static string Remove(this string Input, StringFilter Filter)
    {
        if (string.IsNullOrEmpty(Input))
            return "";
        var Value = BuildFilter(Filter);
        return Input.Remove(Value);
    }


    /// <summary>
    /// Strips illegal characters for XML content
    /// </summary>
    /// <param name="Content">Content</param>
    /// <returns>The stripped string</returns>
    public static string StripIllegalXML(this string Content)
    {
        if (string.IsNullOrEmpty(Content))
            return "";
        var Builder = new StringBuilder();
        foreach (char Char in Content)
        {
            if (Char == 0x9
                || Char == 0xA
                || Char == 0xD
                || (Char >= 0x20 && Char <= 0xD7FF)
                || (Char >= 0xE000 && Char <= 0xFFFD))
                Builder.Append(Char);
        }
        return Builder.ToString().Replace('\u2013', '-').Replace('\u2014', '-')
            .Replace('\u2015', '-').Replace('\u2017', '_').Replace('\u2018', '\'')
            .Replace('\u2019', '\'').Replace('\u201a', ',').Replace('\u201b', '\'')
            .Replace('\u201c', '\"').Replace('\u201d', '\"').Replace('\u201e', '\"')
            .Replace("\u2026", "...").Replace('\u2032', '\'').Replace('\u2033', '\"')
            .Replace("`", "\'")
            .Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
            .Replace("\"", "&quot;").Replace("\'", "&apos;");
    }

    private static string BuildFilter(StringFilter Filter)
    {
        string FilterValue = "";
        string Separator = "";
        if (Filter.HasFlag(StringFilter.Alpha))
        {
            FilterValue += Separator + "[a-zA-Z]";
            Separator = "|";
        }
        if (Filter.HasFlag(StringFilter.Numeric))
        {
            FilterValue += Separator + "[0-9]";
            Separator = "|";
        }
        if (Filter.HasFlag(StringFilter.FloatNumeric))
        {
            FilterValue += Separator + @"[0-9\.]";
            Separator = "|";
        }
        if (Filter.HasFlag(StringFilter.ExtraSpaces))
        {
            FilterValue += Separator + @"[ ]{2,}";
            Separator = "|";
        }
        return FilterValue;
    }

}

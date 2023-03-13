using PD.LYY.UtilityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtension
{

    public static string CsvToObj(this string str)
    {

        return string.Empty;
    }

    public static string Join<T>(IEnumerable<T> values, string quotes = "", string separator = ",")
    {
        if (values == null)
            return string.Empty;
        var result = new StringBuilder();
        foreach (var each in values)
            result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
        return result.ToString().RemoveEnd(separator);
    }


    /// <summary>
    /// ÒÆ³ýÆðÊ¼×Ö·û´®
    /// </summary>
    /// <param name="value">Öµ</param>
    /// <param name="start">ÒªÒÆ³ýµÄÖµ</param>
    public static string RemoveStart(this string value, string start)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;
        if (string.IsNullOrEmpty(start))
            return value;
        if (value.StartsWith(start, StringComparison.Ordinal) == false)
            return value;
        return value.Substring(start.Length, value.Length - start.Length);
    }

  

    /// <summary>
    /// ÒÆ³ýÄ©Î²×Ö·û´®
    /// </summary>
    /// <param name="value">Öµ</param>
    /// <param name="end">ÒªÒÆ³ýµÄÖµ</param>
    public static string RemoveEnd(this string value, string end)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;
        if (string.IsNullOrEmpty(end))
            return value;
        if (value.EndsWith(end, StringComparison.Ordinal) == false)
            return value;
        return value.Substring(0, value.LastIndexOf(end, StringComparison.Ordinal));
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

    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value);
    }

    public static string PinYin(string chineseText)
    {
        if (chineseText.IsEmpty())
            return string.Empty;
        var result = new StringBuilder();
        foreach (char text in chineseText)
            result.Append(ResolvePinYin(text));
        return result.ToString().ToLower();
    }

    private static string ResolvePinYin(char text)
    {
        byte[] charBytes = Encoding.UTF8.GetBytes(text.ToString());
        if (charBytes[0] <= 127)
            return text.ToString();
        var unicode = (ushort)(charBytes[0] * 256 + charBytes[1]);
        string pinYin = ResolveByCode(unicode);
        if (pinYin.IsEmpty() == false)
            return pinYin;
        return ResolveByConst(text.ToString());
    }


    private static string ResolveByCode(ushort unicode)
    {
        if (unicode >= '\uB0A1' && unicode <= '\uB0C4')
            return "A";
        if (unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464)
            return "B";
        if (unicode >= '\uB2C1' && unicode <= '\uB4ED')
            return "C";
        if (unicode >= '\uB4EE' && unicode <= '\uB6E9')
            return "D";
        if (unicode >= '\uB6EA' && unicode <= '\uB7A1')
            return "E";
        if (unicode >= '\uB7A2' && unicode <= '\uB8C0')
            return "F";
        if (unicode >= '\uB8C1' && unicode <= '\uB9FD')
            return "G";
        if (unicode >= '\uB9FE' && unicode <= '\uBBF6')
            return "H";
        if (unicode >= '\uBBF7' && unicode <= '\uBFA5')
            return "J";
        if (unicode >= '\uBFA6' && unicode <= '\uC0AB')
            return "K";
        if (unicode >= '\uC0AC' && unicode <= '\uC2E7')
            return "L";
        if (unicode >= '\uC2E8' && unicode <= '\uC4C2')
            return "M";
        if (unicode >= '\uC4C3' && unicode <= '\uC5B5')
            return "N";
        if (unicode >= '\uC5B6' && unicode <= '\uC5BD')
            return "O";
        if (unicode >= '\uC5BE' && unicode <= '\uC6D9')
            return "P";
        if (unicode >= '\uC6DA' && unicode <= '\uC8BA')
            return "Q";
        if (unicode >= '\uC8BB' && unicode <= '\uC8F5')
            return "R";
        if (unicode >= '\uC8F6' && unicode <= '\uCBF9')
            return "S";
        if (unicode >= '\uCBFA' && unicode <= '\uCDD9')
            return "T";
        if (unicode >= '\uCDDA' && unicode <= '\uCEF3')
            return "W";
        if (unicode >= '\uCEF4' && unicode <= '\uD188')
            return "X";
        if (unicode >= '\uD1B9' && unicode <= '\uD4D0')
            return "Y";
        if (unicode >= '\uD4D1' && unicode <= '\uD7F9')
            return "Z";
        return string.Empty;


    }

    private static string ResolveByConst(string text)
    {
        int index = ChineseConst.ChinesePinYin.IndexOf(text, StringComparison.Ordinal);
        if (index < 0)
            return string.Empty;
        return ChineseConst.ChinesePinYin.Substring(index + 1, 1);
    }
}

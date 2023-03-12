
using System;
using System.ComponentModel;
using System.Globalization;

public static class ConvertManager
{
    public static R To<T, R>(T Item, R DefaultValue = default(R))
    {
        return (R)To(Item, typeof(R), DefaultValue);
    }

    public static object To<T>(T Item, Type ResultType, object DefaultValue = null)
    {
        try
        {
            if (Item == null)
            {
                return (DefaultValue == null && ResultType.IsValueType) ?
                    Activator.CreateInstance(ResultType) :
                    DefaultValue;
            }
            var ObjectType = Item.GetType();
            if (ObjectType == typeof(DBNull))
            {
                return (DefaultValue == null && ResultType.IsValueType) ?
                    Activator.CreateInstance(ResultType) :
                    DefaultValue;
            }
            if (ResultType.IsAssignableFrom(ObjectType))
                return Item;
            if (Item as IConvertible != null && !ObjectType.IsEnum && !ResultType.IsEnum)
                return Convert.ChangeType(Item, ResultType, CultureInfo.InvariantCulture);
            var Converter = TypeDescriptor.GetConverter(Item);
            if (Converter.CanConvertTo(ResultType))
                return Converter.ConvertTo(Item, ResultType);
            Converter = TypeDescriptor.GetConverter(ResultType);
            if (Converter.CanConvertFrom(ObjectType))
                return Converter.ConvertFrom(Item);
            if (ResultType.IsEnum)
            {
                if (ObjectType == ResultType.GetEnumUnderlyingType())
                    return System.Enum.ToObject(ResultType, Item);
                if (ObjectType == typeof(string))
                    return System.Enum.Parse(ResultType, Item as string, true);
            }
            // if (ResultType.IsClass)
            // {
            //     var ReturnValue = Activator.CreateInstance(ResultType);
            //     var TempMapping = ObjectType.MapTo(ResultType);
            //     if (TempMapping == null)
            //         return ReturnValue;
            //     TempMapping
            //         .AutoMap()
            //         .Copy(Item, ReturnValue);
            //     return ReturnValue;
            // }
        }
        catch
        {
        }
        return (DefaultValue == null && ResultType.IsValueType) ?
            Activator.CreateInstance(ResultType) :
            DefaultValue;
    }

}
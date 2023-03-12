using System;

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
}
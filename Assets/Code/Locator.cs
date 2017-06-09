using System;
using System.Collections.Generic;

public static class Locator
{
    private static Dictionary<Type, object> items = new Dictionary<Type, object>(); 
    
    public static void Register<T> (T item)
    {
        items[typeof(T)] = item;
    }
    
    public static T Find<T>()
    {
        object item;
        return items.TryGetValue (typeof (T), out item) ? (T)item : default(T);
    }
    
    public static void Unregister<T>(T item)
    {
        items.Remove (typeof(T));
    }
    
    public static void Unregister<T>()
    {
        items.Remove (typeof(T));
    }
}
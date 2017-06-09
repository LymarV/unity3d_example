using System.Collections;
using System;
using UnityEngine;

public static class CommonExtensions
{
    public static string AsFormatFor(this string format, params object[] args)
    {
        if (string.IsNullOrEmpty(format))
        {
            return string.Empty;
        }
        
        return string.Format(format, args);
    }

    public static void SetActive(this Component comp, bool value)
    {
        comp.gameObject.SetActive(value);
    }
}

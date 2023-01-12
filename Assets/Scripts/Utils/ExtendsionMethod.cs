using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class ExtendsionMethod 
{
    public static string ToKeyString(this eContentType contentType)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(contentType.ToString());
    }
}

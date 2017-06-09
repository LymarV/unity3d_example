using UnityEngine;
using System.Collections;
using System;

public class InspectorButton : Attribute
{
    public string Title { get; private set; }

    public bool Multiedit { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InspectorButtonAttribute"/> class.
    /// </summary>
    /// <param name="title">Text of the button.</param>
    /// <param name="multiedit">If set to <c>true</c> button press applies to all objects in the Selection of same type.</param>
    public InspectorButton(string title, bool multiedit)
    {
        Title = title;
        Multiedit = multiedit;
    }

    public InspectorButton(string title) : this(title, true)
    {

    }
}
using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(Component), true)]
public class BaseEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

        var targetType = target.GetType();

        var methods = targetType.GetMethods();

        var attributesList = new List<object>();
        for (int i = 0; i < methods.Length; i++)
        {
            var m = methods[i];

            if (m.IsPrivate || m.IsStatic)
            {
                continue;
            }

            attributesList.Clear();

            attributesList.AddRange(m.GetCustomAttributes(true));
            var buttonAttribute = attributesList.Find(x => x is InspectorButton) as InspectorButton;

            if (buttonAttribute != null && GUILayout.Button(buttonAttribute.Title))
            {
                if (buttonAttribute.Multiedit)
                {
                    for (int j = 0; j < targets.Length; j++)
                    {
                        m.Invoke(targets[j], null);
                    }
                }
                else
                {
                    m.Invoke(target, null);
                }
            }
        }
	}
}

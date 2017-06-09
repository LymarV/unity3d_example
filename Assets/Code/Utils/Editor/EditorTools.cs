using UnityEngine;
using UnityEditor;

using System.Collections;

public static class EditorTools
{
    [MenuItem("Assets/Copy asset paths to clipcoard")]
    public static void CopyAssetPaths()
    {
        var paths = "";

		for (int i = 0; i < Selection.assetGUIDs.Length; i++)
        {
			var guid = Selection.assetGUIDs[i];
			paths += AssetDatabase.GUIDToAssetPath(guid);

			if (Selection.assetGUIDs.Length > 1)
			{
				paths += "\n";
			}
        }

        EditorGUIUtility.systemCopyBuffer = paths;
    }
}

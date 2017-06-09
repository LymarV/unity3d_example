using UnityEngine;
using System.Collections;
using System;

public static class GameObjectExtensions 
{
	
	#region SmartDestroy
	
	public static void SmartDestroy (this MonoBehaviour script)
	{
		if (script == null) {
			return;
		}
		SmartDestroy(script.gameObject);
	}
	
	public static void SmartDestroy (this GameObject gameObject)
	{
		if (gameObject == null) {
			return;
		}
		if (Application.isEditor && !Application.isPlaying) {
			GameObject.DestroyImmediate (gameObject);
		} else {
			GameObject.Destroy (gameObject);
		}
	}
		
	#endregion
	
	#region Children iterator
	
	public static void DestoryAllChildren (this MonoBehaviour script)
	{
		script.ForEachChild(SmartDestroy);
	} 

	public static void ForEachChild(this MonoBehaviour script, Action<GameObject> action)
	{
		if (script == null || action == null) {
			return;
		}
		
		var transform = script.transform;
		
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			var child = transform.GetChild(i);
			action(child.gameObject);
		}
	}
	
	#endregion

	public static bool IsCloneOf (this MonoBehaviour script, GameObject gameObject)
	{
		return script.name.EndsWith ("(Clone)") && script.name.StartsWith(gameObject.name);
	}
}

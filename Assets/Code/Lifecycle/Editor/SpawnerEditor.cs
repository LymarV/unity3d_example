using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : BaseEditor 
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
        
        var spawner = (Spawner)target;
        
		if (GUILayout.Button("Respawn"))
		{
            spawner.Respawn();
		}
	}
}

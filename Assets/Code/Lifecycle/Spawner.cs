using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{
	public GameObject[] prefabs;

	[Range(0f, 1f)]
	public float emptySpawnChance = 0;
	
	public bool manualSpawn;

	// Use this for initialization
	void Awake () 
	{
		if (!manualSpawn)
		{
			Respawn();
		}
	}
	
	public void Respawn()
	{
		this.DestoryAllChildren();

		if (emptySpawnChance > 0)
		{
			if (Random.Range (0f, 1f) <= emptySpawnChance)
			{
				// Do not spawn
				return;
			}
		}

		if (prefabs != null && prefabs.Length > 0)
		{
			var index = Random.Range (0, prefabs.Length);
			var prefabToSpawn = prefabs [index];
			Spawn (prefabToSpawn);
		}
	}
	
	private void Spawn(GameObject prefab)
	{
		if (prefab == null) {
			return;
		}
		
		var position = transform.position;
		position += prefab.transform.position;
		var go = (GameObject)Instantiate(prefab, position, Quaternion.identity);
		go.transform.parent = transform;
		go.transform.localScale = prefab.transform.localScale;		
	}

	void OnDrawGizmos ()
	{
		var color = Color.blue;
		color.a = 0.4f;
		Gizmos.color = color;
		Gizmos.DrawWireCube (transform.position, new Vector3 (.5f, .5f, 0f));
	}

	[InspectorButton("Remove Empty Entries")]
	public void RemoveEmptyEntries()
	{
		List<GameObject> list = new List<GameObject>();

		for (int i = 0; i < prefabs.Length; i++)
		{
			if (prefabs[i])
			{
				list.Add(prefabs[i]);
			}
		}

		prefabs = list.ToArray();
	}
}

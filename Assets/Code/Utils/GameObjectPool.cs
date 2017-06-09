using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private readonly IDictionary<object, object> pool = new Dictionary<object, object>();

    public GameObject GetOrCreate(GameObject prefab)
    {
        var key = prefab.name; 

        object result;
        if (pool.TryGetValue(key, out result))
        {
            pool.Remove(key);
            
            Debug.Log("Found in pool: " + key);

            var go = (GameObject)result;
            go.SetActive(true);

            var reusable = go.GetComponent<IReusable>();
            if (reusable != null)
            {
                reusable.Reset();
            }

            return go;
        }

		return (GameObject)GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public void Put(GameObject gameObject)
    {
        var key = gameObject.name;
        if (key.EndsWith("(Clone)"))
        {
            key = key.Substring(0, key.Length - "(Clone)".Length);
        }

        // Delete if there is already such object in the pool
        if (pool.ContainsKey(key))
        {
            Debug.Log("GameObject already exist in the pool: " + key);
            gameObject.SmartDestroy();
        }
        else
        {
            gameObject.SetActive(false);
            pool[key] = gameObject;
        }
    }
}
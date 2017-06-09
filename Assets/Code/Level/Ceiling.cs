using UnityEngine;

public class Ceiling: MonoBehaviour
{
    void Start()
    {
        var camera = Camera.main;
        var pos = transform.position;
        pos.y = camera.orthographicSize;
        transform.position = pos;
    }
}
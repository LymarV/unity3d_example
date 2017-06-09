using UnityEngine;

public class Collector: MonoBehaviour
{
    public void SetRadius(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}
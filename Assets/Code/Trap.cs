using UnityEngine;

public class Trap: MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == Tags.Player && other.name.StartsWith("Player"))
        {
            var player = other.GetComponentInParent<PlayerController>();
            if (!player.invulnerable)
            {
                player.Die();
            }
        }
    }
}
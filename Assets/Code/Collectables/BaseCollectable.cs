using UnityEngine;
using System.Collections;

public abstract class BaseCollectable: MonoBehaviour
{
    private bool _collected;

	protected abstract void Collect ();

	void OnTriggerEnter2D(Collider2D other)
	{
        if (!_collected && other.tag == Tags.Player)
		{
            OnPlayerInteraction(other.gameObject);

            _collected = true;

			StartCoroutine (CollectCoroutine());
		}
	}
	
	private IEnumerator CollectCoroutine()
	{
		Collect ();

		yield return Animate();
		
		Destroy (gameObject);
    }

    protected virtual void OnPlayerInteraction(GameObject player)
    {

    }
	
	protected virtual IEnumerator Animate()
	{
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        yield return new WaitForSeconds(2f);

		for (int i = 0; i < 6; i++)
		{
			yield return new WaitForSeconds (0.1f);
			spriteRenderer.enabled = ((i % 2) == 1);
		}
	}    
}
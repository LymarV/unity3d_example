using System.Linq;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;
	public float smoothDampTime = 0.2f;
	public Vector3 cameraOffset;
	public bool lockVerticalAxis;
	
	private Vector3 _smoothDampVelocity;
	
	void Start()
	{
		if (target == null)
		{
			var player = GameObject
				.FindGameObjectsWithTag (Tags.Player)
				.Where(p => p.name.StartsWith("Player"))
				.FirstOrDefault();
			
			if (player != null)
			{
				target = player.transform;
			}
		}
	}

	void LateUpdate()
	{
		Move(true);
	}

	public void InstantMove()
	{
		Move(false);
	}

	private void Move(bool smooth)
	{
		if (target)
		{
			var currentPos = transform.position;

			Vector3 newPos = target.position - cameraOffset;
			if (smooth)
			{
				newPos = Vector3.SmoothDamp( currentPos, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime );
			}
			newPos.z = currentPos.z;
			if (lockVerticalAxis)
			{
				newPos.y = currentPos.y;
			}
			transform.position = newPos;
		}
	}
}

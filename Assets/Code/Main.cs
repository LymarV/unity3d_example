using UnityEngine;

public class Main : MonoBehaviour 
{
	public RootConfig RootConfig;

	
	void Awake()
	{
		Locator.Register(new GameContext());
		Locator.Register(RootConfig);
	}
}

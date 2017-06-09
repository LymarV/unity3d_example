using UnityEngine;

public class Session 
{
	private float scores;
	
	public int Scores
	{
		get { return (int)scores; }
	}
	
	public Session()
	{
		Locator.Register(this);
	}
	
	public void Update () 
	{
		AddScores(Time.deltaTime * 1);
	}
	
	public void AddScores (float scores)
	{
		SetScores (this.scores + scores);
	}
	
	public void Reset()
	{
		SetScores (0);
	}
	
	private void SetScores (float scores)
	{
		this.scores = scores;
	}
}

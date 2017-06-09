
using System;

[Serializable]
public class PlayerConfig
{
    public float runSpeed = 8f;
    public float jumpHeight = 5f;
    public float collectionRadius = 1f;

	public float jumpDuration = 0.7f;
    public float minJumpDurationKoef = 0.75f;

	public float yVelocityReset = 0.2f;
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
	// movement config
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	
	public bool invulnerable = false;

	public PlayerConfig Config { get; private set; }

	public bool IsAlive
	{
		get { return alive; }
	}
	
	public event Action OnDied = delegate {};

	[HideInInspector]
	private float normalizedHorizontalSpeed = 1;

	private Animator _animator;
    private Vector3 _velocity;

    private GameObject  _deathStarsAnimation;

	private bool doJump;
	private bool canDoubleJump;
	private bool doBounceFromCeiling;
	private bool alive = true;
	private bool paused = false;
	
	private Vector3 initialPosition;

	private readonly List<BasePowerup> powerups = new List<BasePowerup>();


	void Awake()
	{
		initialPosition = transform.position;
		
		_animator = GetComponentInChildren<Animator>();
        _deathStarsAnimation = GetComponentInChildren<SpriteRenderer>().gameObject;

	}

	void Start()
	{
		Reset();
	}


	#region Event Listeners

	private void OnCharacterPhysicsCollided( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f ) {
			return;
		}

		if ( hit.normal.y == -1f ) {
			doBounceFromCeiling = true;
		}
			
		if (alive)
		{
			if (invulnerable)
			{
				Jump();
				hit.collider.enabled = false;
			}
			else
			{
				Die();
			}
		}

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _characterPhysics.collisionState + ", hit.normal: " + hit.normal );
	}

	#endregion
	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if (paused) 
		{
			return;
		}
		
		ProcessPowerups();

		Move (Config.runSpeed, Config.jumpHeight);
	}

	public void Configure (PlayerConfig playerConfig)
	{
		Config = playerConfig;
		GetComponentInChildren<Collector>().SetRadius( playerConfig.collectionRadius );
	}

	private void ProcessPowerups()
	{
		for (int i = powerups.Count - 1; i >= 0; i--)
		{
			var powerup = powerups[i];
			
			// Update powerup
			powerup.Update(Time.deltaTime);

			// Remove powerup if finished
			if (powerup.IsFinished)
			{
				powerups.RemoveAt(i);
			}
		}
	}

	private enum JumpDownState
	{
		None,
		Initiated,
		WaitingForGround,
	}

	private JumpDownState jumpDownState;

	private void Move(float xSpeed, float highJumpHeight)
	{
		_velocity.y = 0;
		canDoubleJump = true;
		
		if ( !alive )
		{
			normalizedHorizontalSpeed = 0;
		}

		var gravity = Physics2D.gravity.y;
		
		// we can only jump whilst grounded
		if( doJump )
		{
			doJump = false;
			// choose jump or double-jump koeficient
			var jumpKoef = normalizedHorizontalSpeed > 0 ? 1f : 0.75f;

			_velocity.y = Mathf.Sqrt( jumpKoef * highJumpHeight * -gravity );

			_animator.SetBool("Grounded", false);			
		}

		var speedReduce = gravity * Time.deltaTime; 

		if (jumpTime > 0)
		{
			speedReduce *= (1 - jumpTime);
			jumpTime -= Time.deltaTime;
		}

		// apply gravity before moving
		_velocity.y += speedReduce;

		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * xSpeed, Time.deltaTime);

		if (doBounceFromCeiling)
		{
			doBounceFromCeiling = false;
			_velocity.y = 0.1f * gravity;
		}

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets uf jump down through one way platforms
		if( jumpDownState == JumpDownState.Initiated )
		{
			jumpDownState = JumpDownState.WaitingForGround;
			_velocity.y = 0.3f * gravity;
		}

		if (jumpDownState == JumpDownState.WaitingForGround)
		{
			jumpDownState = JumpDownState.None;
		}
	}

	void FixedUpdate()
	{
		_animator.SetBool("Alive", alive);

		_animator.SetBool("JumpDown", jumpDownState != JumpDownState.None);

		_animator.SetFloat("Speed", _velocity.x);
		_animator.SetFloat("vSpeed", _velocity.y);

        _deathStarsAnimation.SetActive(!alive);
	}
    
	public void Die()
	{
		alive = false;
		_animator.SetBool("Alive", false);
		
		OnDied();
	}
	
	public void Reset()
	{
		transform.position = initialPosition;
		_velocity = Vector3.zero;
		//_characterPhysics.collisionState.reset();

		// fix an issue when cat can't jump on resurrection after dying in the air
		canDoubleJump = true;

		var rootConfig = Locator.Find<RootConfig>();
		PlayerConfig playerConfig = null;
		if (rootConfig != null)
		{
			playerConfig = rootConfig.PlayerConfig;
		}

		Configure( playerConfig ?? new PlayerConfig() );
	}
	
	public void Run()
	{
		alive = true;
		normalizedHorizontalSpeed = 1;
	}
	
	public void Stay()
	{
		normalizedHorizontalSpeed = 0;
	}

	public void FinishJump()
	{
		var rootConfit = Locator.Find<RootConfig>();
		var playerConfig = rootConfit != null ? rootConfit.PlayerConfig : new PlayerConfig();

		var minJumpTime = playerConfig.jumpDuration * playerConfig.minJumpDurationKoef;

		// If releasing during high jump - reset vertical velocity
		if (jumpTime < minJumpTime && jumpTime > 0)
		{
			_velocity.y *= playerConfig.yVelocityReset;
		}

		jumpTime = 0;
	}

	private float jumpTime;

	public void Jump()
	{
		if (!IsRunning) {
			return;
		}

		jumpTime = 0.7f; // 1 second

		if (canDoubleJump)
		{
			doJump = true;
			canDoubleJump = false;

			// HACK: Restart jump animation
			_animator.Play("jump hight", 0, 0);
			_velocity.y = 0; // to ensure FixedUpdate does not change animation state to "Fall"
		}
	}
	
	public void JumpDown()
	{
		if (IsRunning && jumpDownState == JumpDownState.None)
		{
			jumpDownState = JumpDownState.Initiated;
		}
	}
	
	public void Slide()
	{
		if (!IsRunning) {
			return;
		}

		_animator.Play("sliding");
	}

	public void PrepareForJump()
	{
		var currentAnimation = _animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimation.IsName("sliding"))
		{
			_animator.Play("RUN");
		}
	}

	public void Pause(bool pause)
	{
		paused = pause;
		_animator.speed = paused ? 0 : 1;
	}

	private bool IsRunning
	{
		get { return alive && (normalizedHorizontalSpeed == 1) && enabled; }
	}

	public void AddPowerup (BasePowerup powerup)
	{
		if (powerup == null) {
			return;
		}

		var current = powerups.Find(p => p.IsSame(powerup));
		if (current != null)
		{
			// such powerup already exists - just reset it's progress
			current.Reset();
		}
		else
		{
			powerups.Add(powerup);
			powerup.Affect(this);
		}
	}
}
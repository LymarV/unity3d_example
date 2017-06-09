public abstract class BasePowerup
{
    public bool IsFinished
    {
        get { return time > duration; }
    }

    protected float duration;

    private float time;

    private PlayerController playerController;

    protected BasePowerup(float duration)
    {
        this.duration = duration;
    }

    public void Affect (PlayerController player)
    {
        this.playerController = player;
    }

    public void Reset()
    {
        time = 0.001f;
    }

    public void Update(float deltaTime)
    {
        if (time <= 0)
        {
            Begin (playerController);
        }

        time += deltaTime;

        if (IsFinished)
        {
            Finish (playerController);
        }
    }

    protected virtual void Begin(PlayerController player)
    {

    }

    protected virtual void Finish(PlayerController player)
    {

    }

    public virtual bool IsSame(BasePowerup another)
    {
        return another.GetType() == this.GetType();
    }
}
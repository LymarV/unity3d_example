using System.Collections;

public enum InGameMode
{
    None,
    Resume,
    Resurrect,
    StartNew,
}

public class InGameView: BaseView
{
    private GameContext GameContext
    {
        get { return DataContext as GameContext; }
    }
    
    void Awake()
    {
        DataContext = Locator.Find<GameContext>();
    }
    
    void Start()
    {
        GameContext.OnDie += delegate {
            RouteTo<GameOverView>();
        };
    }
    
    public override void PrepareToShow(object parameters)
    {
        var mode = (InGameMode)parameters;
        switch (mode)
        {
            case InGameMode.Resume:
                GameContext.Resume();
                break;
            case InGameMode.Resurrect:
                GameContext.Resurrect();
                break;
            case InGameMode.StartNew:
            case InGameMode.None:
            default:
                GameContext.Restart();
                break;
        }
    }
    
    public override IEnumerator HideCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }
    
    public void Pause()
    {
        GameContext.Pause();
        RouteTo<PauseView>();
    }
}
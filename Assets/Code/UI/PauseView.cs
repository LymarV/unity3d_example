using System.Collections;

public class PauseView: BaseView
{
    private GameContext GameContext
    {
        get { return DataContext as GameContext; }
    }
    
    void Awake()
    {
        DataContext = Locator.Find<GameContext>();
    }

    public override IEnumerator HideCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }
            
    public void Resume()
    {
        RouteTo<InGameView>(InGameMode.Resume);
    }
    
    public void Restart()
    {
        RouteTo<InGameView>(InGameMode.StartNew);
    }
    
    public void Exit()
    {
        GameContext.Exit();

        RouteTo<MainMenuView>();
    }
}
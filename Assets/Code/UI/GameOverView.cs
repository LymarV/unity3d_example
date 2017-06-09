using System.Collections;

public class GameOverView: BaseView
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
        CancelInvoke("AutoHide");
        yield return null;
        gameObject.SetActive(false);
    }
    
    public override IEnumerator ShowCoroutine()
    {
        yield return null;
        //Invoke("AutoHide", 3f);
    }
    
    private void AutoHide()
    {
        RouteTo<AfterGameView>();
    }

    public void Restart()
    {
        RouteTo<InGameView>(InGameMode.StartNew);
    }
    
    public void Resurrect()
    {
        RouteTo<InGameView>(InGameMode.Resurrect);
    }
}
using System.Collections;

public class AfterGameView: BaseView
{
    public override IEnumerator HideCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }
    
    public void TryAgain()
    {
        RouteTo<InGameView>(InGameMode.StartNew);
    }
}
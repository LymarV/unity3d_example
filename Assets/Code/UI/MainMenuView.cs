using System.Collections;
using UnityEngine;

public class MainMenuView: BaseView
{
    public void Play()
    {
        RouteTo<InGameView> (InGameMode.StartNew);
    }

    public void Config()
    {
        RouteTo<ConfigView> ();
    }
    
    public override IEnumerator HideCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }
}
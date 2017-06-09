using System.Collections;
using UnityEngine;

public class ViewRouter: MonoBehaviour
{
    void Awake()
    {
        Locator.Register(this);
    }
    
    public void RouteTo<T>(BaseView fromView, object parameters)
        where T: BaseView
    {
        var toView = GetComponentInChildren<T>(includeInactive: true);
        if (toView == null) 
        {
            Debug.LogError(string.Format("Can't route to '{0}' because there is no such object in the scene", typeof(T)));
        }
        
        StartCoroutine(RouteCoroutine(fromView, toView, parameters));
    }
    
    private IEnumerator RouteCoroutine(BaseView fromView, BaseView toView, object parameters)
    {
        yield return fromView.HideCoroutine();
        
        if (toView != null)
        {
            toView.gameObject.SetActive(true);
            toView.PrepareToShow(parameters);
            yield return toView.ShowCoroutine();
        }
    }
}
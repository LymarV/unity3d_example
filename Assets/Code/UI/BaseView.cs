using System.Collections;
using UnityEngine;

public class BaseView: MonoBehaviour
{
    private object dataContext;
    public object DataContext
    {
        get { return dataContext; }
        set
        {
            if (dataContext == value) {
                return;
            }
            
            dataContext = value;
            UpdateDataContext (dataContext);
        }
    }
    
    protected void RouteTo<T>(object parameters = null)
        where T: BaseView
    {
        var router = Locator.Find<ViewRouter>();
        if (router != null) 
        {
            router.RouteTo<T>(this, parameters);
        }
    }
    
    public virtual void PrepareToShow(object parameters)
    {
    }
    
    public virtual IEnumerator ShowCoroutine()
    {
        yield return null;
    }
    
    public virtual IEnumerator HideCoroutine()
    {
        yield return null;
    }
    
    private void UpdateDataContext(object dataContext)
    {
        var bindings = GetComponentsInChildren<BaseBinding>();
        foreach (var binding in bindings)
        {
            binding.DataContext = dataContext;
        }
    }
}
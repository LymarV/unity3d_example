using System;

public class ActionDisposable : BaseDisposable
{
    private Action onDispose;
    
    public ActionDisposable(Action onDispose)
    {
        this.onDispose = onDispose;
    }
    
    protected override void Dispose(bool disposing)
    {
        onDispose();
    }
}

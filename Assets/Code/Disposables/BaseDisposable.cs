using System;

public abstract class BaseDisposable : IDisposable
{
    ~BaseDisposable()
    {
        Dispose();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Dispose();
        }
    }
    
    public void Dispose()
    {
    }
}
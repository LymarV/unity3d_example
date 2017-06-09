using System;
using System.Collections.Generic;

public class DisposablePack: BaseDisposable
{
    private readonly List<IDisposable> items = new List<IDisposable>();
    
    public void Add(IDisposable item)
    {
        items.Add (item);
    }
    
    protected override void Dispose(bool disposing)
    {
        foreach (var item in items)
        {
            item.Dispose();
        }
        items.Clear();
    }
}
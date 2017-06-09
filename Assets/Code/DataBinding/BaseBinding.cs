using System.ComponentModel;
using UnityEngine;

public class BaseBinding: MonoBehaviour
{
    public string Path;

    private object dataContext;
    public object DataContext 
    { 
        get { return dataContext; }
        set
        {
            var propertyChanged = dataContext as INotifyPropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.PropertyChanged -= OnPropertyChanged;
            }
            
            dataContext = value;
            
            propertyChanged = dataContext as INotifyPropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.PropertyChanged += OnPropertyChanged;
            }
            
            UpdateTarget();
        }
    }
    
    private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == Path) 
        {
            UpdateTarget();
        }
    }
    
    protected object GetPathValue()
    {
        var dataContext = DataContext;
        if (dataContext == null) {
            return null;
        }
        
        var type = dataContext.GetType();
        var propertyInfo = type.GetProperty(Path);
        if (propertyInfo == null) {
            return null;
        }
        
        return propertyInfo.GetValue (dataContext, new object[] {});
    }
    
    protected void SetPathValue(object value)
    {
        var dataContext = DataContext;
        if (dataContext == null) {
            return;
        }
        
        var type = dataContext.GetType();
        var propertyInfo = type.GetProperty(Path);
        if (propertyInfo == null) {
            return;
        }
        
        propertyInfo.SetValue (dataContext, value, new object[] {});
    }

    protected virtual void UpdateTarget()
    {
    }
}
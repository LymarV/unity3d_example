using System.ComponentModel;

public abstract class PropertyChangable: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void RaisePropertyChanged (string propertyName)
    {
        var callback = PropertyChanged;
        if (callback != null)
        {
            callback(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
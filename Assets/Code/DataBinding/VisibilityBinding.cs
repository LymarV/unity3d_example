using System;

public class VisibilityBinding: BaseBinding
{
    public bool Inverse;
    
    protected override void UpdateTarget()
    {
        var value = GetValue();
        gameObject.SetActive(Inverse ? !value : value);
    }
    
    private bool GetValue()
    {
        var value = GetPathValue();
        return value != null ? Convert.ToBoolean (value) : false;
    }
}
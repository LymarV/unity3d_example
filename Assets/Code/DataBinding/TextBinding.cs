using UnityEngine.UI;

public class TextBinding: BaseBinding
{
    private Text control;
    
    private Text Target
    {
        get { return control ?? (control = GetComponent<Text>()); }
    }
    
    public string Format;
    
    protected override void UpdateTarget()
    {
        var value = GetPathValue();
        if (!string.IsNullOrEmpty(Format))
        {
            value = string.Format (Format, value);
        }

        Target.text = value != null ? value.ToString() : null;
    }
}
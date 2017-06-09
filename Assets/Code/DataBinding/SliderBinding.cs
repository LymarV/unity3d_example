using UnityEngine.UI;

public class SliderBinding: BaseBinding
{
    private Slider control;
    
    private Slider Target
    {
        get { return control ?? (control = AttachToTarget()); }
    }
    
    private Slider AttachToTarget()
    {
        var slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        return slider;
    }

    private void OnValueChanged(float value)
    {
        SetPathValue(value);
    }

    protected override void UpdateTarget()
    {
        Target.value = GetValue();
    }
    
    private float GetValue()
    {
        var value = GetPathValue();
        var str = value != null ? value.ToString() : null;

        float result = 0;
        return float.TryParse(str, out result) ? result : 0;
    }
}
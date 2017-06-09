using UnityEngine;
using UnityEngine.UI;

public class SliderView: MonoBehaviour
{
    public Text label;
    public Slider slider;

    public void SetMinValue(float minValue)
    {
        slider.minValue = minValue;
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void BindSlider(string path)
    {
        slider.GetComponent<SliderBinding>().Path = path;
    }

    public void BindText(string path, string format)
    {
        var textBinding = label.GetComponent<TextBinding>();
        textBinding.Path = path;
        textBinding.Format = format;
    }
}
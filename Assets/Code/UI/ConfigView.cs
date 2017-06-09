using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConfigView: BaseView
{
    public ConfigContext ConfigContext
    {
        get { return (ConfigContext)DataContext; }
    }

    public GameObject sliderViewPrefab;
    public GameObject content;

    void Start()
    {
        BuildView();
        DataContext = new ConfigContext();
    }

    public override IEnumerator HideCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }

    public void Close()
    {
        RouteTo<MainMenuView> ();
    }

    private void BuildView()
    {
        const int step = 140;
        int y = 0; 

        AddSlider("Speed", 1, 20, "RunSpeed", y, step);
        y -= step;

        AddSlider("Jump", 1, 20, "JumpHeight", y, step);
        y -= step;

        AddSlider("Gravity", 30, 70, "Gravity", y, step);
        y -= step;

        AddSlider("Collection Radius", 0.5f, 3f, "PlayerCollectionRadius", y, step);
        y -= step;

        AddSlider("Tap detection (seconds)", 0.01f, 0.3f, "TapDetectionTime", y, step);
        y -= step;
    }

    private void AddSlider(string title, float min, float max, string path, int y, int step)
    {
        var go = (GameObject)GameObject.Instantiate(sliderViewPrefab, Vector3.zero, Quaternion.identity);
        var slider = go.GetComponent<SliderView>();

        var format = string.Format("{0}: {{0}}", title);

        if (Mathf.Abs(min - (int)min) > 0.01f
            || Mathf.Abs(max - (int)max) > 0.01f)
        {
            slider.slider.wholeNumbers = false;

            if (Mathf.Abs(max - min) < 1f)
            {
                format = string.Format("{0}: {{0:0.##}}", title);
            }
            else
            {
                format = string.Format("{0}: {{0:0.#}}", title);
            }
        }

        slider.SetMinValue(min);
        slider.SetMaxValue(max);
        slider.BindSlider(path);
        slider.BindText(path, format);
        
        var rect = go.GetComponent<RectTransform>();
        rect.SetParent(content.transform);

        rect.anchoredPosition = new Vector2(0, y);
        rect.sizeDelta = new Vector2(0, step);

    }
}
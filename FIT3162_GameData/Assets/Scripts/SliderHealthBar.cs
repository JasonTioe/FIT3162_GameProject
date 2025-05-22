using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : MonoBehaviour
{

    public Slider slider;
    public Image battery;

    public Sprite battery1;
    public Sprite battery2;
    public Sprite battery3;
    public Sprite battery4;
    public Sprite battery5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBattery(slider.value);
        slider.onValueChanged.AddListener(UpdateBattery);
    }


    void UpdateBattery(float value)
    {
        if (value >= 0.8f)
        {
            battery.sprite = battery5;
        }
        else if (value >= 0.6f)
        {
            battery.sprite = battery4;
        }
        else if (value >= 0.4f)
        {
            battery.sprite = battery3;
        }
        else if (value >= 0.2f)
        {
            battery.sprite = battery2;
        }
        else
        {
            battery.sprite = battery1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

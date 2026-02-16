using UnityEngine;
using UnityEngine.UI;

public class SettingsSliders : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider slider1, slider2, slider3, slider4, slider5;
    public static float AllLoud = 1, MusicLoud = 1, EffectsLoud = 1, Sensitivity = 1, Brightness = 1;
    void Start()
    {
        slider1.GetComponent<Slider>().value = AllLoud;  // Установка текущего положения слайдеров
        slider2.GetComponent<Slider>().value = MusicLoud;
        slider3.GetComponent<Slider>().value = EffectsLoud;
        slider4.GetComponent<Slider>().value = Sensitivity;
        slider5.GetComponent<Slider>().value = Brightness;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAllLoud() //Перезапись новых значений в переменные при изменении слайдера
    {
        AllLoud = slider1.GetComponent<Slider>().value;
    }
    public void ChangeMusicLoud()
    {
        MusicLoud = slider2.GetComponent<Slider>().value;
    }
    public void ChangeEffectLoud()
    {
        EffectsLoud = slider3.GetComponent<Slider>().value;
    }
    public void ChangeSensitivity()
    {
        Sensitivity = slider4.GetComponent<Slider>().value;
    }
    public void ChangeBrightness()
    {
        Brightness = slider5.GetComponent<Slider>().value;
    }
}

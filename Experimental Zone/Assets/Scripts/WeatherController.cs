using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherController : MonoBehaviour
{
    public Material nightMaterial;
    public AnimationCurve fadeCurve;
    public float timeValue;
    public float timeOfDay;
    [SerializeField] private float ToWeatherChange = 0;
    private static System.Random rnd = new System.Random();
    public enum Weather
    {
        Sunny,
        Rain,
    }
    public Weather currentWeather;
    [SerializeField] private GameObject[] WeatherObj;
    public bool WindOn;

    void Update()
    {
        // Имитация смены времени суток
        timeValue = Mathf.PingPong(Time.time / timeOfDay, 1.0f);
        float fadeValue = fadeCurve.Evaluate(timeValue);

        // Установка общей прозрачности
        nightMaterial.SetFloat("_EdgeAlpha", timeValue);
        nightMaterial.SetFloat("_CenterAlpha", timeValue/1.25f);
        if(Mathf.Abs(ToWeatherChange - timeValue) < 0.01f) WeatherChanges();

    }
    private void Start()
    {
        ToWeatherChange = 0.5f+(float)(rnd.NextDouble()*(0.5));
    }

    void WeatherChanges()
    {
        ToWeatherChange = (float)rnd.NextDouble();
        Debug.Log("Погода меняется...");
        Weather on = GetRandomEnumValue();
        Debug.Log(on + " " + (int)on);
        for (int i =0;i < WeatherObj.Length;++i)
        {
           WeatherObj[i].SetActive(i + 1 == (int)on);
            Debug.Log(i + 1 == (int)on);
        }
        
    }

    public static Weather GetRandomEnumValue()
    {
        Array values = Enum.GetValues(typeof(Weather));
        Weather randomValue = (Weather)values.GetValue(rnd.Next(values.Length));
        return randomValue;
    }

}

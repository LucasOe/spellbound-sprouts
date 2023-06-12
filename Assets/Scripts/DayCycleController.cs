using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;

public class DayCycleController : MonoBehaviour
{

    [Range(0, 24)] public float timeOfDay;
    public LightingPreset Preset;
    public float orbitSpeed = 1f;
    public Light sun;
    public Light moon;
    private bool isNight; 

    private Grid grid;

    void Start() {
        grid = GameObject.Find("Field").GetComponent<Grid>();
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * orbitSpeed;
        if (timeOfDay > 24)
            timeOfDay = 0;

        UpdateLighting(timeOfDay / 24f);
        UpdateTime();
    }

    private void OnValidate()
    {
        UpdateTime();
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        sun.color = Preset.DirectionalColor.Evaluate(timePercent);
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150f, 0);

        CheckDayNightTransition();
    }

    private void CheckDayNightTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
        GrowAll();
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }

     private void GrowAll() {
        for(int i = 0; i < grid.tiles.GetLength(0); i++)  {
            for(int j = 0; j < grid.tiles.GetLength(1); j++)  {
                Tile tileObject = grid.tiles[i, j];
                if(tileObject.Plant) {
                    Plant plant = tileObject.Plant.GetComponent<Plant>();
                    plant.Grow();
                }
            }
        }
    }
}
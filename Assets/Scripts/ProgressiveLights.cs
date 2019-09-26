using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveLights : MonoBehaviour
{
    private List<GameObject> allLightObjects = new List<GameObject>();

    public static bool lightsShouldBeDimming = false;
    private float targetDimAmount = -0.4f;
    private float dimGradient = 0.005f;
    private float currentTempDimAmount = 0.0f;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            allLightObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            lightsShouldBeDimming = true;
            Debug.Log("recognizing b press");
            Debug.Log("target dim amount " + targetDimAmount);
            Debug.Log("current temp dim amount " + currentTempDimAmount);
        }
        if (lightsShouldBeDimming && currentTempDimAmount > targetDimAmount)
        {
            MakeAmbientCreepier();
            Debug.Log("lights should be dimming?" + lightsShouldBeDimming);
        }
        if (currentTempDimAmount <= targetDimAmount)
        {
            
            lightsShouldBeDimming = false;
            currentTempDimAmount = 0.0f;
            Debug.Log("target dim amount: " + targetDimAmount);
        }
    }
    public void MakeAmbientCreepier()
    {
        lightsShouldBeDimming = true;
        //RenderSettings.ambientIntensity -= 0.4f;
        for (int i = 0; i < allLightObjects.Count; i++)
        {
            float tempIntensity = allLightObjects[i].GetComponent<Light>().intensity;
            tempIntensity -= 0.3f;
            if (tempIntensity > 0)
            {
                allLightObjects[i].GetComponent<Light>().intensity -= dimGradient;
                currentTempDimAmount -= dimGradient;
            }
        }
    }
}

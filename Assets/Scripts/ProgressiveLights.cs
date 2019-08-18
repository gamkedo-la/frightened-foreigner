using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveLights : MonoBehaviour
{
    private List<GameObject> allLightObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            allLightObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    public void MakeAmbientCreepier()
    {
        RenderSettings.ambientIntensity -= 0.4f;
        for (int i = 0; i < allLightObjects.Count; i++)
        {
            float tempIntensity = allLightObjects[i].GetComponent<Light>().intensity;
            tempIntensity -= 0.3f;
            if (tempIntensity > 0)
            {
                allLightObjects[i].GetComponent<Light>().intensity = tempIntensity;
            }
        }
    }
}

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

    public static bool fogShouldBeGettingFoggier = false;
    private float afterBathroomLightningFogAmount = 20.0f;
    private float fogGradient = 1.0f;

    private float bathroomCutsceneFogAmount = 14.0f;
    private float bathroomCutsceneFogGradient = 0.5f;
    

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
            
        }
        if (lightsShouldBeDimming && currentTempDimAmount > targetDimAmount)
        {
            MakeAmbientCreepier();
            
        }
        if (currentTempDimAmount <= targetDimAmount)
        {
            
            lightsShouldBeDimming = false;
            currentTempDimAmount = 0.0f;
            
        }

        if (fogShouldBeGettingFoggier)
        {
            
            if (PuzzleManagement.PlayerIsDoingBathroomPuzzle && RenderSettings.fogEndDistance > afterBathroomLightningFogAmount)
            {
                RenderSettings.fogEndDistance -= fogGradient;
                
            }
            else if (PuzzleManagement.PlayerIsDoingBathroomPuzzle && RenderSettings.fogEndDistance <= afterBathroomLightningFogAmount)
            {
                fogShouldBeGettingFoggier = false;
            }//end of check fogginess value       
            
            if (PuzzleManagement.PlayerIsDoingCatPuzzle && RenderSettings.fogEndDistance > bathroomCutsceneFogAmount)
            {
                RenderSettings.fogEndDistance -= bathroomCutsceneFogGradient;
                
            }
            else if (PuzzleManagement.PlayerIsDoingCatPuzzle && RenderSettings.fogEndDistance <= bathroomCutsceneFogAmount)
            {
                fogShouldBeGettingFoggier = false;
            }//end of check fogginess value 
        } //end of fogShouldBeGettingFoggier
    }//end of update

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

    public static void turnOnFog()
    {
        RenderSettings.fog = true;
        fogShouldBeGettingFoggier = true;
        
    }
}

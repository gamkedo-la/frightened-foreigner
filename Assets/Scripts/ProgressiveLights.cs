using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressiveLights : MonoBehaviour
{
    private List<GameObject> allLightObjects = new List<GameObject>();

    public static bool lightsShouldBeDimming = false;
    public float targetDimAmount = 0.2f;
    public float targetLightIntensity = 0.8f;
    private float dimGradient = 0.005f;
    private float currentTempDimAmount = 0.0f;

    public static bool fogShouldBeGettingFoggier = false;
    private float afterBathroomLightningFogAmount = 22.5f;
    private float fogGradient = 1.0f;

    private float bathroomCutsceneFogAmount = 18.0f;
    private float bathroomCutsceneFogGradient = 0.5f;

    public GameObject sun;
    public bool rotateSunBathroomAppearance;
    public float tempRotationAmount;
    public float targetRotationAmount;

    public bool rotateSunBathroomCutscene = false;

    public bool lowerExposureBathroomAppearance = false;
    public float targetExposureAmount = 0.0f;
    public float tempExposureAmount = 0.0f;

    public bool lowerExposureBathroomCutscene = false;

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
        if (lightsShouldBeDimming)
        {
            MakeAmbientCreepier();
            
            
        }

        lowerExposure();
        rotateSun();

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
        
        //RenderSettings.ambientIntensity -= 0.4f;
        for (int i = 0; i < allLightObjects.Count; i++)
        {
            float currentLightIntensity = allLightObjects[i].GetComponent<Light>().intensity;
            Debug.Log("currentLightIntensity: " + currentLightIntensity);
            Debug.Log("targetLightIntentisy: " + targetLightIntensity);
            if (currentLightIntensity > targetLightIntensity)
            {
                allLightObjects[i].GetComponent<Light>().intensity -= dimGradient;
                
            } else
            {
                lightsShouldBeDimming = false;
            }
        }
    }

    public static void turnOnFog()
    {
        RenderSettings.fog = true;
        fogShouldBeGettingFoggier = true;
        
    }

    public void rotateSun()
    {
        if (rotateSunBathroomAppearance)
        {
            float rotationGradient = 0.1f;
            targetRotationAmount = 10.0f;
            sun.transform.Rotate(-rotationGradient, 0f, 0f, Space.Self);
            tempRotationAmount += rotationGradient;
            if (tempRotationAmount >= targetRotationAmount)
            {
                rotateSunBathroomAppearance = false;
                tempRotationAmount = 0.0f;
                targetRotationAmount = 0.0f;
            }
        }
        if (rotateSunBathroomCutscene)
        {
            float rotationGradient = 0.1f;
            targetRotationAmount = 17.5f;
            sun.transform.Rotate(-rotationGradient, 0f, 0f, Space.Self);
            if (tempRotationAmount >= targetRotationAmount)
            {
                rotateSunBathroomCutscene = false;
                tempRotationAmount = 0.0f;
                targetRotationAmount = 0.0f;
            }
        }
    }

    public void lowerExposure()
    {

        if (lowerExposureBathroomAppearance)
        {
            float exposureGradient = 0.01f;
            targetExposureAmount = 0.6f;
            float currentExposure = 0.0f;

            currentExposure = RenderSettings.skybox.GetFloat("_Exposure");
            RenderSettings.skybox.SetFloat("_Exposure", currentExposure - exposureGradient);
            tempExposureAmount += exposureGradient;
            if (tempExposureAmount >= targetExposureAmount)
            {
                lowerExposureBathroomAppearance = false;
                targetExposureAmount = 0.0f;
                tempExposureAmount = 0.0f;
            }
        }

        if (lowerExposureBathroomCutscene)
        {
            float exposureGradient = 0.01f;
            targetExposureAmount = 1.2f;
            float currentExposure = 0.0f;
            currentExposure = RenderSettings.skybox.GetFloat("_Exposure");
            //Debug.Log("currentExposure: " + currentExposure);
            if (tempExposureAmount < targetExposureAmount)
            {
                RenderSettings.skybox.SetFloat("_Exposure", currentExposure - exposureGradient);
                tempExposureAmount += exposureGradient;

            }
            else if (tempExposureAmount >= targetExposureAmount)
            {
                
                lowerExposureBathroomCutscene = false;
                targetExposureAmount = 0.0f;
                tempExposureAmount = 0.0f;
            }
        }
    }
}

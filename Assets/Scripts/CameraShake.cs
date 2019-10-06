﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform CamerasTransform;

    private float shakeDuration = 0f;

    private float shakeAmount = 0.4f;
    private float decreaseFactor = 2.0f;

    Vector3 originalPosition;

    public GameObject gateSideClouds;
    private StormControls gateSideStormControlScript;
    public GameObject sicknessSideClouds;
    private StormControls sicknessSideStormControlsScript;
    public GameObject playerGraveSideClouds;
    private StormControls playerGraveSideStormControlsScript;
    public GameObject bathroomSideClouds;
    private StormControls bathroomSideStormControlsScript;

    public GameObject generalRainSoundHolder;
    private GeneralRainSoundsScript generalRainSoundScript;

    private void Awake()
    {
        CamerasTransform = GetComponent(typeof(Transform)) as Transform;
    }

    
    void Start()
    {
        gateSideStormControlScript = gateSideClouds.GetComponent<StormControls>();
        sicknessSideStormControlsScript = sicknessSideClouds.GetComponent<StormControls>();
        playerGraveSideStormControlsScript = playerGraveSideClouds.GetComponent<StormControls>();
        bathroomSideStormControlsScript = bathroomSideClouds.GetComponent<StormControls>();
        generalRainSoundScript = generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ShakeTheCamera();
    }

    public void ResetShakeSettings()
    {
        shakeDuration = 0.35f;
        originalPosition = CamerasTransform.localPosition;
    }

    public void ShakeTheCamera()
    {
        if (shakeDuration > 0)
        {
            CamerasTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            CamerasTransform.localPosition = originalPosition;
        }
    }

    public IEnumerator DelayedCameraShakeForGroundskeeperLightning()
    {
        yield return new WaitForSeconds(6.6f);
        ResetShakeSettings();
        gateSideStormControlScript.activateMe();
        sicknessSideStormControlsScript.activateMe();
        playerGraveSideStormControlsScript.activateMe();
        bathroomSideStormControlsScript.activateMe();
        generalRainSoundScript.generalRainSounds.start();
    }
}

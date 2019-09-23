﻿using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class AudioEventTrigger : MonoBehaviour
{
    
    
    public GameObject secondPuzzleLayerTriggerObject;
    private TriggerPostSecondPuzzleLayers secondPuzzleLayerTriggerScript;

    private FMOD.Studio.EventInstance postBathroomMusic;
    FMOD.Studio.EVENT_CALLBACK callback;

    public GameObject mausoleum;
    private Animation mausoleumShakeAnimation;


    // Start is called before the first frame update
    void Start()
    {
        
        secondPuzzleLayerTriggerScript = secondPuzzleLayerTriggerObject.GetComponent<TriggerPostSecondPuzzleLayers>();
        postBathroomMusic = secondPuzzleLayerTriggerScript.PostBathroomMusic;
        //postBathroomMusic = SceneManagement.PostBathroomMusic;

        callback = new FMOD.Studio.EVENT_CALLBACK(CallBackTriggered);
        postBathroomMusic.setCallback(callback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //audioTrack.EventInstance.setCallback(callback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        mausoleumShakeAnimation = mausoleum.GetComponent<Animation>();

    }

    static FMOD.RESULT CallBackTriggered(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        Debug.Log("Callback triggered");
        //mausoleumShakeAnimation.Play();
        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
        Debug.Log("Parameter name: " + parameter.name);
        return FMOD.RESULT.OK;
    }
}

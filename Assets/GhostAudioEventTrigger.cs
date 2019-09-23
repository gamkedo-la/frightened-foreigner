using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.Runtime.InteropServices;
using System;

public class GhostAudioEventTrigger : MonoBehaviour
{

    public GameObject secondPuzzleLayerTriggerObject;
    private TriggerPostSecondPuzzleLayers secondPuzzleLayerTriggerScript;

    private FMOD.Studio.EventInstance postBathroomMusic;
    FMOD.Studio.EVENT_CALLBACK callback;

    public GameObject ghostLight;
    private Flicker flickerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        secondPuzzleLayerTriggerScript = secondPuzzleLayerTriggerObject.GetComponent<TriggerPostSecondPuzzleLayers>();
        postBathroomMusic = secondPuzzleLayerTriggerScript.PostBathroomMusic;

        callback = new FMOD.Studio.EVENT_CALLBACK(CallBackTriggered);
        postBathroomMusic.setCallback(callback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        flickerScript = ghostLight.GetComponent<Flicker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private FMOD.RESULT CallBackTriggered(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        Debug.Log("Callback triggered");
        
        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
        Debug.Log("Parameter name: " + parameter.name);
        if (parameter.name == "FlickerOn")
        {
            flickerScript.flickerOn();
        }
        if (parameter.name == "FlickerOff")
        {
            flickerScript.flickerOff();
        }
        return FMOD.RESULT.OK;
    }
}

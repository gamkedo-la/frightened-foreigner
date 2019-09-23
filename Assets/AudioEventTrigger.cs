using FMODUnity;
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
    private Animator mausoleumShakeAnimation;

    public GameObject ghostLight;
    private Flicker flickerScript;



    // Start is called before the first frame update
    void Start()
    {

        secondPuzzleLayerTriggerScript = secondPuzzleLayerTriggerObject.GetComponent<TriggerPostSecondPuzzleLayers>();
        postBathroomMusic = secondPuzzleLayerTriggerScript.PostBathroomMusic;
        //postBathroomMusic = SceneManagement.PostBathroomMusic;

        callback = new FMOD.Studio.EVENT_CALLBACK(CallBackTriggered);
        postBathroomMusic.setCallback(callback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //audioTrack.EventInstance.setCallback(callback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        mausoleumShakeAnimation = mausoleum.GetComponent<Animator>();

        flickerScript = ghostLight.GetComponent<Flicker>();
    }

	private FMOD.RESULT CallBackTriggered(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        //Debug.Log("Callback triggered");
        
        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
        Debug.Log("Parameter name: " + parameter.name);
        if (parameter.name == "Percussion Hit")
        {
            mausoleumShakeAnimation.Play(0);
        }
        if (parameter.name == "Flicker On")
        {
            flickerScript.flickerOn();
        }
        if (parameter.name == "Flicker Off")
        {
            flickerScript.flickerOff();
        }
        return FMOD.RESULT.OK;

    }
}

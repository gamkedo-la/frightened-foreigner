using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSoundControls : MonoBehaviour
{

    public FMOD.Studio.EventInstance StormSoundInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        StormSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Storm");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseThunderAndRainIntensityAfterBathroomCutscene()
    {
        StormSoundInstance.setParameterValue("StormIntensity", 1f);
    }
}

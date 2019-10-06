using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralRainSoundsScript : MonoBehaviour
{

    public FMOD.Studio.EventInstance generalRainSounds;

    // Start is called before the first frame update
    void Start()
    {
        generalRainSounds = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Rain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurulSFX : MonoBehaviour
{

    private FMOD.Studio.EventInstance TurulSquawkSound;
    // Start is called before the first frame update
    void Start()
    {
        TurulSquawkSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawk");
        //TurulSquawkSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTurulSquawkSound()
    {
        TurulSquawkSound.start();
    }
}

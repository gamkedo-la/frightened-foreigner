using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISound : MonoBehaviour
{
    private FMOD.Studio.EventInstance GhostUISound;

    private void Awake()
    {
        GhostUISound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UIGhostVoice");
    }
    

    public void PlayGhostUISound()
    {
        GhostUISound.start();
    }
}

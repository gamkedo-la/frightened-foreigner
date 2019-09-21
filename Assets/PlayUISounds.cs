using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISounds : MonoBehaviour
{

    public static FMOD.Studio.EventInstance UISelectionGhostVoiceSound;

    // Start is called before the first frame update
    void Start()
    {
        UISelectionGhostVoiceSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UIGhostVoice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManagement : MonoBehaviour
{
    private FMOD.Studio.EventInstance BackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/titleScreenMusicV1");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
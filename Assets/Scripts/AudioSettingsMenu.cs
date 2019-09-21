using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{

    FMOD.Studio.Bus MasterBus;
    FMOD.Studio.Bus MusicBus;
    FMOD.Studio.Bus DialogueBus;
    FMOD.Studio.Bus SFXBus;

    float MasterBusVolume = 1.0f;
    float MusicBusVolume = 1.0f;
    float DialogueBusVolume = 1.0f;
    float SFXBusVolume = 1.0f;

    FMOD.Studio.EventInstance SFXVolumeTestEvent;
    FMOD.Studio.EventInstance DialogueVolumeTestEvent;



    public GameObject AudioSettingsManagerFromHierarchy;

    public GameObject whatever;

    private void Awake()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        DialogueBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Dialogue");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/RightClickScrollThroughWords");
        DialogueVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/RepeatForint");


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MasterBus.setVolume(MasterBusVolume);
        MusicBus.setVolume(MusicBusVolume);
        DialogueBus.setVolume(DialogueBusVolume);
        SFXBus.setVolume(SFXBusVolume);
    }

    public void GrabMasterVolumeFromGameplay(float newMasterVolume)
    {
        MasterBusVolume = newMasterVolume;
    }
    public void GrabMusicVolumeFromGameplay(float newMusicVolume)
    {
        MusicBusVolume = newMusicVolume;
    }
    public void GrabDialogueVolumeFromGameplay(float newDialogueVolume)
    {
        DialogueBusVolume = newDialogueVolume;
    }
    public void GrabSFXVolumeFromGameplay(float newSFXVolume)
    {
        SFXBusVolume = newSFXVolume;
    }

    public void PlaySFXVolumeTest (float newSFXVolume)
    {
        SFXBusVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE SFXPlaybackState;
        SFXVolumeTestEvent.getPlaybackState(out SFXPlaybackState);
        if (SFXPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }
    }

    public void PlayDialogueVolumeTest(float newWordsVolume)
    {
        DialogueBusVolume = newWordsVolume;

        FMOD.Studio.PLAYBACK_STATE DialoguePlaybackState;
        DialogueVolumeTestEvent.getPlaybackState(out DialoguePlaybackState);
        if (DialoguePlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            DialogueVolumeTestEvent.start();
        }
    }

    public void DontDestroyMe()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

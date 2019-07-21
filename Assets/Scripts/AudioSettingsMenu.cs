using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{

    FMOD.Studio.Bus MasterBus;
    FMOD.Studio.Bus MusicBus;
    FMOD.Studio.Bus WordsBus;
    FMOD.Studio.Bus SFXBus;

    float MasterBusVolume = 1.0f;
    float MusicBusVolume = 1.0f;
    float WordsBusVolume = 1.0f;
    float SFXBusVolume = 1.0f;

    FMOD.Studio.EventInstance SFXVolumeTestEvent;
    FMOD.Studio.EventInstance WordsVolumeTestEvent;

    private void Awake()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        WordsBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Words");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/RightClickScrollThroughWords");
        WordsVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Words/Correct_Answer");
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
        WordsBus.setVolume(WordsBusVolume);
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
    public void GrabWordsVolumeFromGameplay(float newWordsVolume)
    {
        WordsBusVolume = newWordsVolume;
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

    public void PlayWordsVolumeTest(float newWordsVolume)
    {
        WordsBusVolume = newWordsVolume;

        FMOD.Studio.PLAYBACK_STATE WordsPlaybackState;
        WordsVolumeTestEvent.getPlaybackState(out WordsPlaybackState);
        if (WordsPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            WordsVolumeTestEvent.start();
        }
    }
}

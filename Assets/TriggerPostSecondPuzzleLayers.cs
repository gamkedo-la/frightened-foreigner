using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPostSecondPuzzleLayers : MonoBehaviour
{
    private FMOD.Studio.EventInstance PostBathroomMusic;
    private FMOD.Studio.PLAYBACK_STATE PostBathroomMusicPlayback;
    private bool isPostBathroomMusicPlaying;

    void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    private void Start()
    {
        PostBathroomMusic = SceneManagement.PostBathroomMusic;
    }

    private void OnTriggerEnter(Collider other)
    {
        PostBathroomMusic.getPlaybackState(out PostBathroomMusicPlayback);
        isPostBathroomMusicPlaying = PostBathroomMusicPlayback != FMOD.Studio.PLAYBACK_STATE.STOPPED;

        if(isPostBathroomMusicPlaying)
        {
            switch (gameObject.name)
            {
                case "postSecondPuzzlePercLayerTrigger":
                    PostBathroomMusic.setParameterValue("PercIn", 1f);
                    break;
                case "postSecondPuzzleFluteLayerTrigger":
                    PostBathroomMusic.setParameterValue("FluteIn", 1f);
                    break;
                case "postSecondPuzzlePianoLayerTrigger":
                    PostBathroomMusic.setParameterValue("PianoIn", 1f);
                    break;
                case "postSecondPuzzleGhostBellsLayerTrigger":
                    PostBathroomMusic.setParameterValue("GhostBellsIn", 1f);
                    break;
                case "postSecondPuzzleMetalsLayerTrigger":
                    PostBathroomMusic.setParameterValue("MetalsIn", 1f);
                    break;
                case "postSecondPuzzleVocalsLayerTrigger":
                    PostBathroomMusic.setParameterValue("VocalsIn", 1f);
                    
                    break;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        PostBathroomMusic.getPlaybackState(out PostBathroomMusicPlayback);
        isPostBathroomMusicPlaying = PostBathroomMusicPlayback != FMOD.Studio.PLAYBACK_STATE.STOPPED;

        if (isPostBathroomMusicPlaying)
        {
            switch (gameObject.name)
            {
                case "postSecondPuzzlePercLayerTrigger":
                    PostBathroomMusic.setParameterValue("PercIn", 0f);
                    break;
                case "postSecondPuzzleFluteLayerTrigger":
                    PostBathroomMusic.setParameterValue("FluteIn", 0f);
                    break;
                case "postSecondPuzzlePianoLayerTrigger":
                    PostBathroomMusic.setParameterValue("PianoIn", 0f);
                    break;
                case "postSecondPuzzleGhostBellsLayerTrigger":
                    PostBathroomMusic.setParameterValue("GhostBellsIn", 0f);
                    break;
                case "postSecondPuzzleMetalsLayerTrigger":
                    PostBathroomMusic.setParameterValue("MetalsIn", 0f);
                    break;
                case "postSecondPuzzleVocalsLayerTrigger":
                    PostBathroomMusic.setParameterValue("VocalsIn", 0f);
                    
                    break;
            }
        }
    }
}

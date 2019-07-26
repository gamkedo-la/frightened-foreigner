using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    static public FMOD.Studio.EventInstance TitleScreenMusic;
    static public FMOD.Studio.PLAYBACK_STATE TitleScreenMusicPlaybackState;

    public GameObject SceneManagerFromHierarchy;
    public GameObject AudioSettingsCanvasFromHierarchy;
    public GameObject AudioSettingsManagerFromHierarchy;

    public Animator animator;

    private float BackgroundMusicLayersFadeValue = 0f;
    private bool ShouldFadeInSopranos = false;
    

    private void Awake()
    {
        
        TitleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/titleScreenMusicV1");
    }

    // Start is called before the first frame update
    void Start()
    {       
            TitleScreenMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFadeInSopranos && BackgroundMusicLayersFadeValue < 1)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
    }

    public void LoadCemeteryLevelFromIntroCutscene()
    {
        SceneManager.LoadScene("Cemetery Level");
        TitleScreenMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
       
        
    }

    public void LoadIntroCutScene()
    {
        SceneManager.LoadScene("Intro CutScene");
        ShouldFadeInSopranos = true;
    }
}

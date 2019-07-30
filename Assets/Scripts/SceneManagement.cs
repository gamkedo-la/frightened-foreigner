using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

     public FMOD.Studio.EventInstance TitleScreenMusic;
    static public FMOD.Studio.PLAYBACK_STATE TitleScreenMusicPlaybackState;

    public GameObject SceneManagerFromHierarchy;
    public GameObject AudioSettingsCanvasFromHierarchy;
    public GameObject AudioSettingsManagerFromHierarchy;

    private Animator LevelChangerAnimator;

    public float BackgroundMusicLayersFadeValue = 0f;
    private bool ShouldFadeInTitleTrack = false;
    private bool ShouldFadeInSopranos = false;
    private bool ShouldFadeInGlock = false;
    private bool ShouldTransitionToBassoonPart = false;

    private GameObject BlackFade;

    private GameObject Part1Text;
    private TriggerLayerChange TriggerLayerChangeScript;
    

    private void Awake()
    {
        ShouldFadeInTitleTrack = true;
        TitleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/titleScreenMusicV1");
        LevelChangerAnimator = GameObject.Find("LevelChanger").GetComponent<Animator>();
        BlackFade = GameObject.Find("BlackFade");

        
    }

    // Start is called before the first frame update
    void Start()
    {       
            TitleScreenMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFadeInTitleTrack && BackgroundMusicLayersFadeValue < 1)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
        if (ShouldFadeInSopranos && BackgroundMusicLayersFadeValue < 2)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
        if (ShouldFadeInGlock && BackgroundMusicLayersFadeValue < 3)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
        if (TriggerLayerChangeScript.ShouldTransitionToBassoonPart && BackgroundMusicLayersFadeValue < 4)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
    }

    public void LoadCemeteryLevelFromIntroCutscene()
    {
        Part1Text = GameObject.Find("Part1 Text");
        TriggerLayerChangeScript = Part1Text.GetComponent<TriggerLayerChange>();
        SceneManager.LoadScene("Cemetery Level");
        if (!TriggerLayerChangeScript.ShouldTransitionToBassoonPart)
        {
            TriggerLayerChangeScript.ShouldTransitionToBassoonPart = true;
        }
    }

    public void LoadIntroCutScene()
    {
        SceneManager.LoadScene("Intro CutScene");
        ShouldFadeInGlock = true;
        BlackFade.SetActive(false);
        Debug.Log("Black Fade should be inactive");
    }

    public void TransitionToIntroCutscene()
    {
        LevelChangerAnimator.Play("FadeOut");
        ShouldFadeInSopranos = true;       
    }

    
}

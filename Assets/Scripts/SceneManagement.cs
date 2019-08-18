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

    public FMOD.Studio.EventInstance PostFirstPuzzleMusic;
    static public FMOD.Studio.PLAYBACK_STATE PostFirstPuzzleMusicPlaybackState;

    public float PostFirstPuzzleLayersFadeValue = 0f;
    public bool ShouldFadeInPostFirstLevelTrack = false;

    private Scene CurrentScene;

    private void Awake()
    {
        ShouldFadeInTitleTrack = true;
        TitleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/titleScreenMusicV1");
        LevelChangerAnimator = GameObject.Find("LevelChanger").GetComponent<Animator>();
        BlackFade = GameObject.Find("BlackFade");

        PostFirstPuzzleMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/firstPuzzleMusicBaseLayer");

        TriggerLayerChangeScript = gameObject.GetComponent<TriggerLayerChange>();
    }

    // Start is called before the first frame update
    void Start()
    {       
            TitleScreenMusic.start();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentScene = SceneManager.GetActiveScene();


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
        if (ShouldFadeInPostFirstLevelTrack && PostFirstPuzzleLayersFadeValue < 1)
        {
            Debug.Log(PostFirstPuzzleLayersFadeValue);
            PostFirstPuzzleMusic.getPlaybackState(out PostFirstPuzzleMusicPlaybackState);
            Debug.Log(PostFirstPuzzleMusicPlaybackState);
            if (PostFirstPuzzleMusicPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                PostFirstPuzzleMusic.start();
            }
            PostFirstPuzzleLayersFadeValue += 0.01f;
            PostFirstPuzzleMusic.setParameterValue("PostFirstPuzzleLayersFadeValue", PostFirstPuzzleLayersFadeValue);
        }
        if (ShouldFadeInPostFirstLevelTrack && BackgroundMusicLayersFadeValue < 5)
        {
            BackgroundMusicLayersFadeValue += 0.01f;
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
    }

    public void TransitionsForCemeteryLevelFromIntroCutscene()
    {
        Part1Text = GameObject.Find("Part1 Text");
        TriggerLayerChangeScript = Part1Text.GetComponent<TriggerLayerChange>();

        BlackFade.SetActive(true);
        
        LevelChangerAnimator.Play("FadeOut", 0, 0.25f);

        if (!TriggerLayerChangeScript.ShouldTransitionToBassoonPart)
        {
            TriggerLayerChangeScript.ShouldTransitionToBassoonPart = true;
        }
    }

    public void LoadCemeteryLevelFromIntroCutscene()
    {
        if (CurrentScene.name == "Intro CutScene")
        {
            SceneManager.LoadScene("Cemetery Level");
        }
    }

    public void LoadIntroCutScene()
    {
        if (CurrentScene.name == "Main Menu")
        {
            SceneManager.LoadScene("Intro CutScene");
            ShouldFadeInGlock = true;
            //BlackFade.SetActive(false);
        }
    }

    public void TransitionToIntroCutscene()
    {
        LevelChangerAnimator.Play("FadeOut");
        ShouldFadeInSopranos = true;       
    }

    
}

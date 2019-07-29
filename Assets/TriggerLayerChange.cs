using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLayerChange : MonoBehaviour
{

    private SceneManagement SceneManagementScript;
    private FMOD.Studio.EventInstance TitleScreenMusic;
    private float BackgroundMusicLayersFadeValue;
    private bool ShouldTransitionToBassoonPart = false;

    private void Awake()
    {
        SceneManagementScript = GameObject.Find("LevelChanger").GetComponent<SceneManagement>();
        TitleScreenMusic = SceneManagementScript.TitleScreenMusic;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ShouldTransitionToBassoonPart && BackgroundMusicLayersFadeValue < 4)
        {
            BackgroundMusicLayersFadeValue += 0.005f;
            
            TitleScreenMusic.setParameterValue("BackgroundMusicLayerManageValue", BackgroundMusicLayersFadeValue);
        }
    }

    public void ChangeLayersSecondPartIntroText()
    {
        ShouldTransitionToBassoonPart = true;
        
        BackgroundMusicLayersFadeValue = SceneManagementScript.BackgroundMusicLayersFadeValue;
    }
}
